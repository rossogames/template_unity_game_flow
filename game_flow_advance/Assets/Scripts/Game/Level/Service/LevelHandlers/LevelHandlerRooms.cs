using Rossoforge.Common.DataStructures;
using Rossoforge.Events.Bus;
using Rossoforge.Pool.DataConfig;
using Rossoforge.Utils.Logger;
using Rossogames.Common;
using Rossogames.LevelObjects.Components;
using Rossogames.LevelObjects.DataAssets;
using Rossogames.LevelObjects.Events;
using Sirenix.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace Rossogames.Level.Service
{
    public class LevelHandlerRooms : LevelHandlerBase, IEventListener<RoomEnterEvent>
    {
        private const int _depth = 3;

        private readonly Transform _root;
        private List<Node<LevelRoomDataAsset>> _nodes = new();
        private HashSet<LevelRoomDataAsset> _activeRooms = new();
        private HashSet<LevelRoomDataAsset> _toRemoveRooms = new();
        private Dictionary<LevelRoomDataAsset, LevelRoom> _instancedRooms = new();
        private Node<LevelRoomDataAsset> _currentNode;

        public LevelHandlerRooms(LevelDataService serviceData, Transform root) : base(serviceData)
        {
            _root = root;
        }

        public override void Initialize()
        {
            _eventService.RegisterListener<RoomEnterEvent>(this);

            LoadNodes();
            PopulateRooms();

            _currentNode = _nodes[0];
            Instantiate(_currentNode, depth: _depth);
        }
        public override void Dispose()
        {
            base.Dispose();
            _eventService.UnregisterListener<RoomEnterEvent>(this);
        }

        private void LoadNodes()
        {
            var map = new Dictionary<LevelRoomDataAsset, Node<LevelRoomDataAsset>>();

            // 1. Crear todos los padres
            foreach (var room in _serviceData.LevelDataAsset.Rooms)
            {
                var node = new Node<LevelRoomDataAsset>(room);
                _nodes.Add(node);
                map.Add(room, node);
            }

            // 2. conectar los nodos hijos
            foreach (var node in _nodes)
            {
                foreach (var nextRoom in node.Value.NextRoom)
                {
                    if (map.TryGetValue(nextRoom, out var nextNode))
                        node.AddChild(nextNode);
                }
            }
        }
        private void PopulateRooms()
        {
            var populatedRooms = new HashSet<PooledGameobjectDataConfig>();
            foreach (var room in _serviceData.LevelDataAsset.Rooms)
            {
                if (populatedRooms.Add(room.AssetReference))
                    _poolService.Populate(room.AssetReference, PoolCategories.Gameplay);
            }
        }
        private void Instantiate(Node<LevelRoomDataAsset> node, int depth, int currentDepth = 1)
        {
            if (currentDepth > depth || node == null)
                return;

            _toRemoveRooms.Remove(node.Value);

            if (_activeRooms.Add(node.Value))
            {
                var roomDataAsset = node.Value;
                var obj = _poolService.Get(roomDataAsset.AssetReference, _root, roomDataAsset.Position, Space.World, PoolCategories.Gameplay);
                obj.gameObject.name = roomDataAsset.name;
                obj.transform.rotation = Quaternion.Euler(roomDataAsset.Rotation);

                var levelRoom = obj.gameObject.GetComponent<LevelRoom>();
                levelRoom.Initialize(roomDataAsset);

                _instancedRooms.Add(node.Value, levelRoom);
            }

            foreach (var childNode in node.Children)
                Instantiate(childNode, depth, currentDepth + 1);

        }
        private void TryChangeCurrentNode(LevelRoomDataAsset roomDataAsset)
        {
            if (_currentNode.Value.Equals(roomDataAsset))
                return;

            _toRemoveRooms.AddRange(_activeRooms);
            foreach (var node in _currentNode.Children)
            {
                if (!node.Value.Equals(roomDataAsset))
                    continue;

                _currentNode = node;
                Instantiate(_currentNode, depth: _depth);
                DespawnFarRooms();
                return;
            }

            RossoLogger.Error($"It has not been possible to find room {roomDataAsset.name} next to the current room {_currentNode.Children}");
        }
        private void DespawnFarRooms()
        {
            foreach (var room in _toRemoveRooms)
            {
                if (_instancedRooms.TryGetValue(room, out var levelRoom))
                {
                    levelRoom.ReturnToPool();
                    _instancedRooms.Remove(room);
                }
                _activeRooms.Remove(room);
            }
        }

#if UNITY_EDITOR
        public override void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            foreach (var n in _nodes)
                foreach (var child in n.Children)
                    Gizmos.DrawLine(n.Value.Position, child.Value.Position);
        }
#endif

        public void OnEventInvoked(RoomEnterEvent eventArg)
        {
            TryChangeCurrentNode(eventArg.LevelRoomDataAsset);
        }
    }
}