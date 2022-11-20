using Asteroids.Scripts.Core;
using UnityEngine;

namespace Asteroids.Scripts.Gameplay
{
    public class Starship : MonoBehaviour
    {
        [SerializeField] private MovableSpaceObject movableSpaceObject;
        [SerializeField] private SpaceEngine spaceEngine;

        private bool isMovePressed = false;

        public MovableSpaceObject MovableSpaceObject => movableSpaceObject;

        public void MovePressed(bool isPressed)
        {
            isMovePressed = isPressed;
        }

        public void PointerPositionChanged(Vector3 lookAtPosition)
        {
            spaceEngine.UpdatePointPosition(lookAtPosition);
        }

        private void Start()
        {
            movableSpaceObject.BindTransformPosition(transform);
            spaceEngine = new SpaceEngine(movableSpaceObject, transform);
        }

        public void Fire()
        {
        }

        public void LaserFire()
        {
        }

        private void Update()
        {
            if (isMovePressed)
                spaceEngine.Move(transform.up);

            spaceEngine.LookAtPoint();
        }

        private void FixedUpdate()
        {
            movableSpaceObject.PhysicUpdate();
        }
    }
}