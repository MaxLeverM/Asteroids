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
        [SerializeField] private Quaternion rotation;
        [SerializeField] private float damping = 1f;
        [SerializeField] private float maxSpeed = 0.1f;
        private Transform transform;

        public Action<Vector2> OnPositionUpdate;
        public Action<Quaternion> OnRotationUpdate;
        public Action<Vector2> OnVelocityUpdate;

        public Vector2 Position
        {
            get => position;
            set
            {
                position = value;
                OnPositionUpdate?.Invoke(position);
                if (transform != null)
                    transform.position = position;
            }
        }

        public Quaternion Rotation
        {
            get => rotation;
            set
            {
                rotation = value;
                OnRotationUpdate?.Invoke(rotation);
                if(transform!=null)
                    transform.rotation = rotation;
            }
        }

        public Vector2 Velocity
        {
            get => velocity;
            set
            {
                velocity = value;
                OnVelocityUpdate?.Invoke(velocity);
            }
        }

        public void PhysicUpdate()
        {
            Position += velocity;

            if (velocity.magnitude > maxSpeed)
            {
                velocity.Normalize();
                velocity *= maxSpeed;
            }

            Velocity *= damping;
        }

        public void BindTransform(Transform transformToBind)
        {
            transform = transformToBind;
        }
    }
}