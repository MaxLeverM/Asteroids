using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids.Scripts.ECS.Services
{
    public interface ISceneContext
    {
        public Camera Camera { get; }
        public PlayerInput PlayerInput { get; }
    }
}