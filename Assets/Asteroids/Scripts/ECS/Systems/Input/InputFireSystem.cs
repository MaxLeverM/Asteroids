using Asteroids.Scripts.ECS.Components;
using Asteroids.Scripts.ECS.Services;
using Leopotam.Ecs;
using UnityEngine.InputSystem;

namespace Asteroids.Scripts.ECS.Systems.Input
{
    public class InputFireSystem : IEcsInitSystem
    {
        private ISceneContext sceneContext;
        private PlayerInput playerInput;
        private InputAction fireAction;
        
        private EcsFilter<InputMainAttackListener> inputAttackListener;
        
        public void Init()
        {
            playerInput = sceneContext.PlayerInput;
            sceneContext.PlayerInput.onActionTriggered += ReadAction;
            fireAction = playerInput.currentActionMap.FindAction(InputConstants.Fire);
        }

        private void ReadAction(InputAction.CallbackContext context)
        {
            if (context.action == fireAction)
            {
                Fire(context);
            }
        }

        private void Fire(InputAction.CallbackContext context)
        {
            foreach (var i in inputAttackListener)
            {
                var entity = inputAttackListener.GetEntity(i);
                entity.Get<FireEvent>();
            }
        }
    }
}