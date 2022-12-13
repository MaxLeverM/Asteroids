using Asteroids.Scripts.ECS.Components;
using Asteroids.Scripts.ECS.Services;
using Asteroids.Scripts.Gameplay;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids.Scripts.ECS.UnityComponents
{
    public class InputSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private Camera mainCamera;
        private ISceneContext sceneContext;
        private PlayerInput playerInput;

        private InputAction fireAction;
        private InputAction moveAction;
        private InputAction rotateAction;
        private InputAction additionalFireAction;

        private EcsFilter<PlayerComponent, SpaceEngineComponent, MovableComponent> playerEngineFilter;

        public void Init()
        {
            mainCamera = sceneContext.Camera;
            playerInput = sceneContext.PlayerInput;
            sceneContext.PlayerInput.onActionTriggered += ReadAction;

            fireAction = playerInput.currentActionMap.FindAction("Fire");
            moveAction = playerInput.currentActionMap.FindAction("Move");
            rotateAction = playerInput.currentActionMap.FindAction("Look");
            additionalFireAction = playerInput.currentActionMap.FindAction("AdditionalFire");
        }

        private void ReadAction(InputAction.CallbackContext context)
        {
            if (context.action == fireAction)
            {
                Fire(context);
            }

            if (context.action == moveAction)
            {
                Move(context);
            }

            if (context.action == rotateAction)
            {
                Rotate(context);
            }

            if (context.action == additionalFireAction)
            {
                AdditionalFire(context);
            }
        }


        private bool isMovePressed;

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

        public void Rotate(InputAction.CallbackContext context)
        {
            foreach (var i in playerEngineFilter)
            {
                ref var engine = ref playerEngineFilter.Get2(i);

                engine.lookAtPosition = mainCamera.ScreenToWorldPoint(context.ReadValue<Vector2>());
            }
        }

        public void Fire(InputAction.CallbackContext context)
        {
            /*    switch (context.phase)
                {
                    case InputActionPhase.Started:
                        starship.Fire(true);
                        break;
                    case InputActionPhase.Canceled:
                        starship.Fire(false);
                        break;
                }*/
        }

        public void AdditionalFire(InputAction.CallbackContext context)
        {
            /* switch (context.phase)
             {
                 case InputActionPhase.Started:
                     starship.AdditionalFire(true);
                     break;
                 case InputActionPhase.Canceled:
                     starship.AdditionalFire(false);
                     break;
             }*/
        }

        public void Run()
        {
            Move();
        }

        private void Move()
        {
            foreach (var i in playerEngineFilter)
            {
                ref var engine = ref playerEngineFilter.Get2(i);
                ref var movable = ref playerEngineFilter.Get3(i);
                engine.moveDirection = isMovePressed?movable.transform.up: Vector2.zero;
            }
        }
    }
}