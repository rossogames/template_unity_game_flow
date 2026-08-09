using RossoGames.Gameplay.Events;
using RossoGames.Tokens.Components;
using UnityEngine;

namespace RossoGames.Level.Service
{
    public class LevelHandlerTokens : LevelHandlerBase
    {
        private readonly Transform _root;
        private int _currentTileIndex = 0;
        private Token _token;

        public LevelHandlerTokens(LevelServiceData serviceData, Transform root) : base(serviceData)
        {
            _root = root;
        }

        public override void Initialize()
        {
            Instantiate();
        }
        public async Awaitable MoveToken(int steps)
        {
            var tilesCount = _levelService.GetTilesCount();
            int nextIndex = 0;

            var _nextPositions = new Vector3[steps];
            for (int i = 0; i < steps; i++)
            {
                nextIndex = (_currentTileIndex + i + 1) % tilesCount;
                _nextPositions[i] = _levelService.GetTokenAnchorPosition(nextIndex);
            }

            await _token.Move(_nextPositions);
            _currentTileIndex = nextIndex;

            _eventService.Raise(new GameplayTokenLandedEvent(_currentTileIndex));
        }
        public int GeTokenTileIndex()
        {
            return _currentTileIndex;
        }

        private void Instantiate()
        {
            var obj = GameObject.Instantiate(_serviceData.PlayerTokenAssetReference, _root);
            obj.transform.rotation = Quaternion.identity;

            var w = (float)_serviceData.CurrentLevelDataAsset.Size.Width;
            obj.transform.position = _levelService.GetTokenAnchorPosition(_currentTileIndex);

            _token = obj.GetComponent<Token>();
        }
    }
}
