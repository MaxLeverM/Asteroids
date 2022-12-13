using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids.Scripts.ECS.Services
{
    public class SceneContext : MonoBehaviour, ISceneContext
    {
        [SerializeField] private Camera camera;
        [SerializeField] private PlayerInput playerInput;
        
        public Camera Camera => camera;
        public PlayerInput PlayerInput => playerInput;
    }
}