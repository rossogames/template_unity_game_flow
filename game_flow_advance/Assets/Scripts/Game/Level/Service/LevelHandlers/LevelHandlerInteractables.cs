using Rossogames.Common;
using Rossogames.ContextActions.Components;
using Rossogames.ContextActions.DataEntities;
using Rossogames.ContextActions.Resolver;
using Rossogames.LevelObjects.Components;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rossogames.Level.Service
{
    public class LevelHandlerInteractables : LevelHandlerBase
    {
        private readonly Transform _root;
        private ContextActionsMenu _contextActions;

        private Dictionary<string, IContextActionResolver> _actionResolvers;

        public LevelHandlerInteractables(LevelDataService serviceData, Transform root) : base(serviceData)
        {
            _root = root;
        }

        public override void Initialize()
        {
            base.Initialize();
            _poolService.Populate(_serviceData.ContextActionsAssetReference, PoolCategories.Gameplay);

            _actionResolvers = new Dictionary<string, IContextActionResolver>();
        }

        public void TryShowContextActions(Interactable interactable)
        {
            if (!interactable.InZone)
                return;

            if (interactable.DataBehaviour.InteractableContextActions == null || interactable.DataBehaviour.InteractableContextActions.Length == 0)
                return;

            if (_contextActions == null)
            {
                _contextActions = _poolService.Get<ContextActionsMenu>(
                    _serviceData.ContextActionsAssetReference,
                    _root,
                    new Vector3(280, -100, 0),
                    Space.Self,
                    PoolCategories.Gameplay
                );
            }

            _contextActions.Initialize(interactable);
        }
        public void HideContextActions()
        {
            if (_contextActions != null)
            {
                _contextActions.gameObject.SetActive(false);
                _contextActions = null;
            }
        }
        public bool IsAvailable(ContextActionDataEntity contextActionDataEntity, Interactable interactable)
        {
            var resolver = GetResolver(contextActionDataEntity, interactable);
            return resolver.IsAvailable();
        }
        public async Awaitable InvokeAction(ContextActionDataEntity contextActionDataEntity, Interactable interactable)
        {
            var resolver = GetResolver(contextActionDataEntity, interactable);
            HideContextActions();
            await resolver.Invoke();
            TryShowContextActions(interactable);
        }

        private IContextActionResolver GetResolver(ContextActionDataEntity contextActionDataEntity, Interactable interactable)
        {
            if (!_actionResolvers.TryGetValue(contextActionDataEntity.name, out var resolver))
            {
                var contextActionType = contextActionDataEntity.GetType();
                var reolverType = typeof(ContextActionResolver<>).MakeGenericType(contextActionType);
                resolver = Activator.CreateInstance(reolverType) as IContextActionResolver;

                _actionResolvers.Add(contextActionDataEntity.name, resolver);
            }

            resolver.Initialize(contextActionDataEntity, interactable);
            return resolver;
        }
    }
}