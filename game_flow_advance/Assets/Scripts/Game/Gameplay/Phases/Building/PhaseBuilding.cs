using RossoGames.Buildings.DataEntities;
using RossoGames.Gameplay.DataBehaviour;
using UnityEngine;

namespace RossoGames.Gameplay.Phases.Building
{
    public class PhaseBuilding : GameplayBasePhase
    {
        private BuildingDataEntity _dataEntity;

        public PhaseBuilding(PhaseBuildingDataBehaviour dataBehaviour) : base(dataBehaviour)
        {
        }

        public void Initialize(BuildingDataEntity dataEntity)
        {
            _dataEntity = dataEntity;
        }

        public override void Enter()
        {
            base.Enter();
            _ = InstanceBuilding();
        }

        private async Awaitable InstanceBuilding()
        {
            _levelService.InstanceBuilding(_dataEntity);
            await Awaitable.WaitForSecondsAsync(1);

            await _gameplayService.TransitionToPhaseDiceRoll();
        }
    }
}
