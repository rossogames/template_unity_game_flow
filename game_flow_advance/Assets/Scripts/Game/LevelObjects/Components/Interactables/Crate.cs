using DG.Tweening;
using Game.Common.Extensions;
using Rossoforge.Services.Locator;
using Rossoforge.TimeFlow.Service;
using Rossogames.ContextActions.DataEntities;
using Rossogames.Items.DataEntities;
using Rossogames.LevelObjects.DataBehaviour;
using Rossogames.LevelObjects.DataStates;
using Rossogames.PopupFlow.Service;
using Rossogames.Progression.Components;
using UnityEngine;

namespace Rossogames.LevelObjects.Components
{
    public class Crate : PersistableState<CrateDataState>,
        IContextActionListener<ContextActionOpenDataEntity>,
        IContextActionListener<ContextActionCloseDataEntity>,
        IContextActionListener<ContextActionInspectDataEntity>
    {
        [SerializeField]
        private CrateDataBehaviour _dataBehaviour;

        [SerializeField]
        private ContainerDataEntity _containerDataEntity;

        [SerializeField]
        private GameObject _lid;

        [SerializeField]
        private AudioSource _audioOpen;

        [SerializeField]
        private AudioSource _audioClose;

        private IPopupFlowService _popupFlowService;
        private ITimeFlowService _timeFlowService;

        private Vector3 _closedRotation;
        private Vector3 _openRotation;

        protected override void Awake()
        {
            base.Awake();

            _popupFlowService = ServiceLocator.Get<IPopupFlowService>();
            _timeFlowService = ServiceLocator.Get<ITimeFlowService>();

            _closedRotation = _lid.transform.localRotation.eulerAngles;
            _openRotation = _closedRotation + Vector3.right * _dataBehaviour.OpenRotation;
        }
        override protected void OnDataStateLoaded()
        {
            base.OnDataStateLoaded();
            _lid.transform.localRotation = Quaternion.Euler(DataState.IsOpened ? _openRotation : _closedRotation);
        }

        public bool IsContextActionEnabled(ContextActionOpenDataEntity contextActionDataEntity)
        {
            return !DataState.IsOpened;
        }
        public bool IsContextActionEnabled(ContextActionCloseDataEntity contextActionDataEntity)
        {
            return DataState.IsOpened;
        }
        public bool IsContextActionEnabled(ContextActionInspectDataEntity contextActionDataEntity)
        {
            return DataState.IsOpened;
        }
        public async Awaitable OnContextActionInvoke(ContextActionOpenDataEntity contextActionDataEntity)
        {
            DataState.IsOpened = true;
            _lid.transform.DOKill();
            _audioOpen.Play();


            await _lid.transform
                .DOLocalRotate(_openRotation, _dataBehaviour.Duration)
                .SetEase(_dataBehaviour.EaseCurve)
                .Await();

            await OpenContainerPopup();
        }
        public async Awaitable OnContextActionInvoke(ContextActionCloseDataEntity contextActionDataEntity)
        {
            DataState.IsOpened = false;
            _lid.transform.DOKill();
            _audioClose.Play();

            await _lid.transform
                .DOLocalRotate(_closedRotation, _dataBehaviour.Duration)
                .SetEase(_dataBehaviour.EaseCurve)
                .Await();
        }
        public async Awaitable OnContextActionInvoke(ContextActionInspectDataEntity contextActionDataEntity)
        {
            await OpenContainerPopup();
        }

        private async Awaitable OpenContainerPopup()
        {
            _timeFlowService.PauseTimeFlow();
            await _popupFlowService.OpenPopupContainer(_containerDataEntity);
            _timeFlowService.ResumeTimeFlow();
        }
    }
}
