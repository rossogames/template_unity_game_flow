using Rossoforge.Core.Services;

namespace RossoGames.Inputs.Service
{
    public interface IInputsService : IService, IInitializable
    {
        float HorizontalAxis { get; }
    }
}