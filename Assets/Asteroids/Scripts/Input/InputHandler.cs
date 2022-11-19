using System;
using Asteroids.Scripts.Gameplay;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids.Scripts.Input
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private Starship starship;
        
        private Camera mainCamera;
        
        private void Awake()
        {
            mainCamera = Camera.main;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    starship.MovePressed(true);
                    break;

                case InputActionPhase.Canceled:
                    starship.MovePressed(false);
                    break;
            }
        }
        public void OnFire(InputAction.CallbackContext context)
        {
            Debug.Log("Fire!");
        }
        public void OnLaserFire(InputAction.CallbackContext context)
        {
            Debug.Log("OnLaserFire!");
        }
        public void OnRotate(InputAction.CallbackContext context)
        {
            starship.PointerPositionChanged(mainCamera.ScreenToWorldPoint(context.ReadValue<Vector2>()));
        }
    }
}