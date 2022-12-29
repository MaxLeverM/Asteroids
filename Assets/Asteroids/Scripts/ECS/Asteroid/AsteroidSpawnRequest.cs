using Asteroids.Scripts.Core;
using Asteroids.Scripts.ECS.UnityComponents;
using UnityEngine;
using UnityEngine.Pool;

namespace Asteroids.Scripts.ECS.Systems
{
    public struct AsteroidSpawnRequest
    {
        public ObjectPool<BaseView> asteroidPool;
        public Vector2 spawnPoint;
        public Vector2 velocity;
        public AsteroidSpawnData spawnData;
    }
}