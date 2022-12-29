using System;
using UnityEngine;

namespace Asteroids.Scripts.Core
{
    [Serializable]
    public class AsteroidSpawnData
    {
        [SerializeField] private int id;
        [SerializeField] private Vector3 scale;
        [SerializeField] private int partsCount;
        [SerializeField] private Vector2 velocityRange;
        [SerializeField] private int scoreForDestroy;
        [SerializeField] private float hp;

        public int Id => id;
        public Vector3 Scale => scale;
        public int PartsCount => partsCount;
        public Vector2 VelocityRange => velocityRange;
        public int ScoreForDestroy => scoreForDestroy;
        public float Hp => hp;
    }
}