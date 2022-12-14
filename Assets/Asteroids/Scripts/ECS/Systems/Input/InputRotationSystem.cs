using Asteroids.Scripts.ECS.Components;
using Asteroids.Scripts.ECS.Services;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids.Scripts.ECS.Systems.Input
{
    public class InputRotationSystem : IEcsInitSystem
    {
        private Camera mainCamera;
        private ISceneContext sceneContext;
        private PlayerInput playerInput;
        private InputAction rotateAction;
        
        private EcsFilter<PlayerComponent, RotationEngineComponent> playerEngineFilter;
        
        public void Init()
        {
            mainCamera = sceneContext.Camera;
            playerInput = sceneContext.PlayerInput;
            sceneContext.PlayerInput.onActionTriggered += ReadAction;
            rotateAction = playerInput.currentActionMap.FindAction(InputConstants.Look);
        }
        
        private void ReadAction(InputAction.CallbackContext context)
        {
            if (context.action == rotateAction)
            {
                Rotate(context);
            }
        }

        private void Rotate(InputAction.CallbackContext context)
        {
            foreach (var i in playerEngineFilter)
            {
                ref var engine = ref playerEngineFilter.Get2(i);

                engine.lookAtPosition = mainCamera.ScreenToWorldPoint(context.ReadValue<Vector2>());
            }
        }
    }
}