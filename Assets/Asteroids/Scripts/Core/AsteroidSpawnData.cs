using System;
using UnityEngine;

namespace Asteroids.Scripts.Core
{
    [Serializable]
    public class AsteroidSpawnData
    {
        [SerializeField] private Vector3 scale;
        [SerializeField] private int partsCount;
        [SerializeField] private Vector2 velocityRange;

        public Vector3 Scale => scale;
        public int PartsCount => partsCount;
        public Vector2 VelocityRange => velocityRange;
    }
}