﻿using Asteroids.Scripts.ECS.Components;
using Asteroids.Scripts.ECS.Services;
using Leopotam.Ecs;
using UnityEngine.InputSystem;

namespace Asteroids.Scripts.ECS.Systems.Input
{
    public class InputAdditionalFireSystem : IEcsInitSystem
    {
        private ISceneContext sceneContext;
        private PlayerInput playerInput;
        private InputAction additionalFireAction;
        
        private EcsFilter<InputAdditionalAttackListener> inputAttackListener;
        
        public void Init()
        {
            playerInput = sceneContext.PlayerInput;
            sceneContext.PlayerInput.onActionTriggered += ReadAction;
            additionalFireAction = playerInput.currentActionMap.FindAction(InputConstants.AdditionalFire);
        }

        private void ReadAction(InputAction.CallbackContext context)
        {
            if (context.action == additionalFireAction)
            {
                AdditionalFire(context);
            }
        }

        private void AdditionalFire(InputAction.CallbackContext context)
        {
            foreach (var i in inputAttackListener)
            {
                var entity = inputAttackListener.GetEntity(i);
                entity.Get<FireEvent>();
            }
        }
    }
}