using Rossoforge.Services.Service;
using UnityEngine;

namespace Rossogames.Cameras.Service
{
    public interface ICameraService : IService
    {
        Camera Camera { get; }

        void SetupCamera(Camera camera);
    }
}