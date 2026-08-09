using Rossoforge.Core.DataStructures;
using Rossoforge.Services;
using RossoGames.Buildings.DataAssets;
using RossoGames.Buildings.DataEntities;
using RossoGames.Cards.Components;
using RossoGames.Common.Enums;
using RossoGames.Progression.Service;
using RossoGames.Tiles.Components;
using RossoGames.Tiles.DataBehaviour;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RossoGames.Level.Service
{
    public class LevelHandlerBuildings : LevelHandlerBase
    {
        private IProgressionService _progressionService;

        private readonly Transform _cardsRoot;
        private readonly Transform _buildingsRoot;
        private Dictionary<Rarities, List<BuildingDataAsset>> _buildingByRarity;
        private Dictionary<BuildingDataEntity, BuildingDataAsset> _buildingEntities;
        private Dictionary<int, GameObject> _activeBuildings = new();
        private List<BuildingCard> _activeCards = new();

        public LevelHandlerBuildings(LevelServiceData serviceData, Transform cardRoot, Transform buildingsRoot) : base(serviceData)
        {
            _progressionService = ServiceLocator.Get<IProgressionService>();
            _cardsRoot = cardRoot;
            _buildingsRoot = buildingsRoot;
        }

        public override void Initialize()
        {
            base.Initialize();
            LoadBuildingsByRarity();
            MapBuildingEntities();
        }

        public void ShowCards(int amount)
        {
            var currentTileIndex = _levelService.GeTokenTileIndex();
            var currentTile = _levelService.GetTile(currentTileIndex) as TileBuildable;

            for (int i = 0; i < amount; i++)
            {
                var rarity = currentTile.BuildingRarities.GetRandomItem();
                var buildings = _buildingByRarity[rarity];

                int randomIndex = Random.Range(0, buildings.Count);
                var randomBuilding = buildings[randomIndex];

                var card = _poolService.Get<BuildingCard>(_serviceData.BuildingCardAssetReference, _cardsRoot, Vector3.zero, Space.World);
                card.Initialize(randomBuilding.BuildingDataEntity);

                _activeCards.Add(card);
                buildings.RemoveAt(randomIndex);
            }
        }
        public void HideCards()
        {
            foreach (var card in _activeCards)
            {
                var dataAsset = _buildingEntities[card.DataEntity];

                _buildingByRarity[card.DataEntity.Rarity].Add(dataAsset);
                card.gameObject.SetActive(false); // this retorn the card to the pool
            }
            _activeCards.Clear();
        }
        public void InstanceBuilding(BuildingDataEntity buildingDataEntity)
        {
            var currentTileIndex = _levelService.GeTokenTileIndex();
            var currentTile = _levelService.GetTile(currentTileIndex) as TileBuildable;

            if (_activeBuildings.ContainsKey(currentTileIndex))
                GameObject.Destroy(_activeBuildings[currentTileIndex]);

            var dataAsset = _buildingEntities[buildingDataEntity];
            var building = GameObject.Instantiate(dataAsset.AssetReference, currentTile.BuildingAnchor.position, currentTile.BuildingAnchor.rotation, _buildingsRoot);
            _activeBuildings[currentTileIndex] = building;

            ChangeTileRarity(currentTile, buildingDataEntity.Rarity);
        }

        private void ChangeTileRarity(TileBuildable tile, Rarities newRarity)
        {
            var rarityToKeep = new WeightedList<Rarities>();

            foreach (var weightedItem in tile.BuildingRarities)
            {
                if (weightedItem.Item >= newRarity)
                    rarityToKeep.Add(weightedItem.Item, weightedItem.Weight);
            }

            tile.BuildingRarities = rarityToKeep;
        }

        private void LoadBuildingsByRarity()
        {
            var unlockedBuildings = _progressionService.GetUnlockedBuildings().ToHashSet();

            _buildingByRarity = _serviceData.BuildingCollectionDataAsset.BuildingDataAssets
                .Where(dataAsset => unlockedBuildings.Contains(dataAsset.BuildingDataEntity.Id))
                .GroupBy(dataAsset => dataAsset.BuildingDataEntity.Rarity)
                .ToDictionary(
                    group => group.Key,
                    group => group.ToList()
                );
        }
        private void MapBuildingEntities()
        {
            _buildingEntities = _serviceData.BuildingCollectionDataAsset.BuildingDataAssets
                .ToDictionary(
                    dataAsset => dataAsset.BuildingDataEntity,
                    dataAsset => dataAsset
                );
        }
    }
}
