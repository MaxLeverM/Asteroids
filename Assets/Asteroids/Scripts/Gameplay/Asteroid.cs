using System;
using Asteroids.Scripts.Core;
using Asteroids.Scripts.Core.Interfaces;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

namespace Asteroids.Scripts.Gameplay
{
    public class Asteroid : MonoBehaviour, IMovableObjectHolder, IDestroyable
    {
        [SerializeField] private MovableSpaceObject spaceObject;

        public Action<GameObject> DestroyCalled { get; set; }

        public MovableSpaceObject MovableObject => spaceObject;


        private void Start()
        {
            spaceObject.BindTransform(transform);
        }

        private void FixedUpdate()
        {
            spaceObject.PhysicUpdate();
        }

        public void CallDestroy()
        {
            DestroyCalled?.Invoke(gameObject);
        }
    }
}