using System;
using Asteroids.Scripts.Core;
using UnityEngine;

namespace Asteroids.Scripts.Gameplay
{
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] private MovableSpaceObject spaceObject;

        public MovableSpaceObject SpaceObject => spaceObject;
        
        private void Start()
        {
            spaceObject.BindTransformPosition(transform);
            spaceObject.Velocity = new Vector2(1, 1);
        }

        private void FixedUpdate()
        {
            spaceObject.PhysicUpdate();
        }
    }
}