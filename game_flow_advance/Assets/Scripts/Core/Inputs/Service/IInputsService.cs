using Rossoforge.Services.Service;

namespace RossoGames.Inputs.Service
{
    public interface IInputsService : IService, IInitializable
    {
        float HorizontalAxis { get; }
    }
}