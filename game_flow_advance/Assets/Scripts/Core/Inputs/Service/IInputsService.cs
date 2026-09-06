using Rossoforge.Services.Service;

namespace Rossogames.Inputs.Service
{
    public interface IInputsService : IService
    {
        CharacterInputs Character { get; }
        CameraInputs Camera { get; }
        InputType CurrentInputType { get; }
    }
}