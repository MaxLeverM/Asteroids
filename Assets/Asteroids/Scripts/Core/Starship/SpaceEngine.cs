using System;
using UnityEngine;

namespace Asteroids.Scripts.Core.Starship
{
    [Serializable]
    public class SpaceEngine
    {
        [SerializeField] private float force = 0.5f;
        [SerializeField] private float rotationOffset = 90f;
        private MovableSpaceObject movableSpaceObject;
        private Transform transform;
        private Vector3 pointPosition;

        public void Init(MovableSpaceObject movableObject, Transform objectTransform)
        {
            movableSpaceObject = movableObject;
            transform = objectTransform;
        }

        public void Move(Vector3 direction)
        {
            var temp = direction * (Time.deltaTime * force);
            movableSpaceObject.Velocity += new Vector2(temp.x, temp.y);
        }

        public void LookAtPoint()
        {
            var diff = pointPosition - transform.position;
            diff.Normalize();
            float z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, z - rotationOffset);
        }

        public void UpdatePointPosition(Vector2 position)
        {
            pointPosition = position;
        }
    }
}