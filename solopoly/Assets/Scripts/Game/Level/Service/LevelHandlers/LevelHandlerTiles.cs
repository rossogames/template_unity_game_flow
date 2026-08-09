using RossoGames.Tiles.Components;
using RossoGames.Tiles.DataAssets;
using RossoGames.Tiles.DataBehaviour;
using System.Collections.Generic;
using UnityEngine;

namespace RossoGames.Level.Service
{
    public class LevelHandlerTiles : LevelHandlerBase
    {
        private readonly Transform _root;
        private List<Tile> _tiles;

        public LevelHandlerTiles(LevelServiceData serviceData, Transform root) : base(serviceData)
        {
            _root = root;
        }

        public override void Initialize()
        {
            var tiles = GetTilesData();
            Instantiate(tiles);
        }
        public Vector3 GetTokenAnchorPosition(int index)
        {
            return _tiles[index].TokenAnchor.position;
        }
        public int GetTilesCount()
        {
            return _tiles.Count;
        }
        public bool IsTileBuildable(int tileIndex)
        {
            var tile = _tiles[tileIndex];
            return tile.DataBehaviour is TileBuildableDataBehaviour;
        }
        public bool IsTileTransport(int tileIndex)
        {
            var tile = _tiles[tileIndex];
            return tile.DataBehaviour is TileTransportDataBehaviour;
        }
        public Tile GetTile(int tileIndex)
        {
            return _tiles[tileIndex];
        }

        private List<TileDataAsset> GetTilesData()
        {
            var w = _serviceData.CurrentLevelDataAsset.Size.Width;
            var h = _serviceData.CurrentLevelDataAsset.Size.Height;

            var tiles = new List<TileDataAsset>();
            var buildeableTilesCount = (w - 1) * 2 + (h - 1) * 2;

            for (var i = 0; i < buildeableTilesCount; i++)
            {
                TileDataAsset randomTile = null;
                if (i == 0 || i == w - 1 || i == w + h - 2 || i == w + w + h - 3) // corners    
                    randomTile = _serviceData.CurrentLevelDataAsset.TransportTiles.GetRandomItem();
                else
                    randomTile = _serviceData.CurrentLevelDataAsset.BuildableTiles.GetRandomItem();

                tiles.Add(randomTile);
            }

            return tiles;
        }
        private void Instantiate(List<TileDataAsset> tiles)
        {
            _tiles = new();

            var x = 0;
            var z = 0;
            var w = _serviceData.CurrentLevelDataAsset.Size.Width;
            var h = _serviceData.CurrentLevelDataAsset.Size.Height;

            int _previousSide = -1;
            for (int i = 0; i < tiles.Count; i++)
            {
                int currentSide =
                    i < w - 1 ? 0 :
                    i < w + h - 2 ? 1 :
                    i < w + w + h - 3 ? 2 :
                    3;

                if (currentSide == 0)
                {
                    if (currentSide > _previousSide)
                    {
                        x = w + 1;
                        z = 0;
                    }

                    x--;
                }
                else if (currentSide == 1)
                {
                    if (currentSide > _previousSide)
                    {
                        x = 0;
                        z = 1;
                    }

                    z++;
                }
                else if (currentSide == 2)
                {
                    if (currentSide > _previousSide)
                    {
                        x = 1;
                        z = h + 2;
                    }

                    x++;
                }
                else if (currentSide == 3)
                {
                    if (currentSide > _previousSide)
                    {
                        x = w + 2;
                        z = h + 1;
                    }

                    z--;
                }

                _previousSide = currentSide;

                var location = new Vector3(x, 0, z);
                var rotation = currentSide * 90f;

                Instantiate(tiles[i], location, rotation);
            }
        }
        private void Instantiate(TileDataAsset tileData, Vector3 position, float rotation)
        {
            var obj = GameObject.Instantiate(tileData.AssetReference, _root);
            var tile = obj.GetComponent<Tile>();
            tile.transform.rotation = Quaternion.Euler(0f, rotation, 0f);
            tile.transform.position = position;
            tile.Initialize(tileData.DataBehaviour);

            _tiles.Add(tile);
        }
    }
}
