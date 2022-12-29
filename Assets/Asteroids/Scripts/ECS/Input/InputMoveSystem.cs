using Asteroids.Scripts.ECS.Components;
using Asteroids.Scripts.ECS.Services;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids.Scripts.ECS.Systems.Input
{
    public class InputMoveSystem : IEcsInitSystem, IEcsRunSystem
    {
        private ISceneContext sceneContext;
        private PlayerInput playerInput;
        private InputAction moveAction;
        private bool isMovePressed;
        
        private EcsFilter<PlayerComponent, MoveEngineComponent, TransformComponent> playerEngineFilter;

        public void Init()
        {
            playerInput = sceneContext.PlayerInput;
            sceneContext.PlayerInput.onActionTriggered += ReadAction;
            
            moveAction = playerInput.currentActionMap.FindAction(InputConstants.Move);
        }

        private void ReadAction(InputAction.CallbackContext context)
        {
            if (context.action == moveAction)
            {
                Move(context);
            }
        }
        
        public void Run()
        {
            Move();
        }

        public void Move(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    isMovePressed = true;
                    break;

                case InputActionPhase.Canceled:
                    isMovePressed = false;
                    break;
            }
        }

        private void Move()
        {
            foreach (var i in playerEngineFilter)
            {
                ref var engine = ref playerEngineFilter.Get2(i);
                ref var transformComponent = ref playerEngineFilter.Get3(i);
                engine.moveDirection = isMovePressed ? transformComponent.transform.up: Vector2.zero;
            }
        }
    }
}