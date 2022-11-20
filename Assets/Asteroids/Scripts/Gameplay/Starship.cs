using Asteroids.Scripts.Core.Interfaces;
using Asteroids.Scripts.Gameplay.GameField;
using UnityEngine;

namespace Asteroids.Scripts.Gameplay
{
    public class Starship : MonoBehaviour, IFieldObject
    {
        private bool isMovePressed = false;
        private Vector3 pointerPosition;

        [SerializeField] private Vector3 velocity;

        [SerializeField] private float damping = 0.99f;
        [SerializeField] private float force = 0.5f;
        [SerializeField] private float maxSpeed = 0.1f;
        [SerializeField] private float rotationOffset = 90f;

        public void MovePressed(bool isPressed)
        {
            isMovePressed = isPressed;
        }

        public void PointerPositionChanged(Vector3 lookAtPosition)
        {
            pointerPosition = lookAtPosition;
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
                Move();

            Rotate();
        }

        private void FixedUpdate()
        {
            transform.position += velocity;

            if (velocity.magnitude > maxSpeed)
            {
                velocity.Normalize();
                velocity *= maxSpeed;
            }

            velocity *= damping;
        }

        private void Move()
        {
            velocity += transform.up * (Time.deltaTime * force);
        }

        private void Rotate()
        {
            var diff = pointerPosition - transform.position;
            diff.Normalize();
            float z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, z - rotationOffset);
        }

        public Vector2 Position
        {
            get => transform.position;
            set => transform.position = value;
        }
    }
}