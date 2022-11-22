using Asteroids.Scripts.Core;
using Asteroids.Scripts.Core.Interfaces;
using Asteroids.Scripts.Core.Starship;
using UnityEngine;

namespace Asteroids.Scripts.Gameplay
{
    public class Starship : MonoBehaviour
    {
        [SerializeField] private MovableSpaceObject movableSpaceObject;
        [SerializeField] private SpaceEngine spaceEngine;
        [SerializeField] private BulletGun mainGun;
        [SerializeField] private LaserGun additionalGun;

        public IGun MainGun => mainGun;
        public LaserGun AdditionalGun => additionalGun;

        private bool isMovePressed = false;

        public MovableSpaceObject MovableSpaceObject => movableSpaceObject;

        private void Start()
        {
            movableSpaceObject.BindTransformPosition(transform);
            spaceEngine = new SpaceEngine(movableSpaceObject, transform); //TODO Inspector data rewrited, fix this
            mainGun.Init(transform);
            additionalGun.Init(transform);
        }

        private void Update()
        {
            if (isMovePressed)
                spaceEngine.Move(transform.up);

            spaceEngine.LookAtPoint();
            
            mainGun.Update();
        }

        private void FixedUpdate()
        {
            movableSpaceObject.PhysicUpdate();
            additionalGun.FixedUpdate();
        }

        public void MovePressed(bool isPressed) => isMovePressed = isPressed;

        public void PointerPositionChanged(Vector3 lookAtPosition) => spaceEngine.UpdatePointPosition(lookAtPosition);

        public void Fire(bool isActive) => mainGun.Fire(isActive);

        public void AdditionalFire(bool isActive) => additionalGun.Fire(isActive);
    }
}