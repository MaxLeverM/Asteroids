using System;
using Asteroids.Scripts.Core.Interfaces;
using UnityEngine;

namespace Asteroids.Scripts.Core
{
    [Serializable]
    public class MovableSpaceObject : IFieldObject
    {
        [SerializeField] private Vector2 position;
        [SerializeField] private Vector2 velocity;
        [SerializeField] private float damping = 1f;
        [SerializeField] private float maxSpeed = 0.1f;
        private Transform transform;

        public Vector2 Position
        {
            get => position;
            set
            {
                position = value;
                if (transform != null)
                    transform.position = position;
            }
        }

        public Vector2 Velocity
        {
            get => velocity;
            set => velocity = value;
        }

        public void PhysicUpdate()
        {
            Position += velocity;

            if (velocity.magnitude > maxSpeed)
            {
                velocity.Normalize();
                velocity *= maxSpeed;
            }

            velocity *= damping;
        }

        public void BindTransformPosition(Transform transformToBind)
        {
            transform = transformToBind;
        }
    }
}