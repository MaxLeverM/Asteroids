using System;
using System.Collections.Generic;
using Asteroids.Scripts.Core;
using Asteroids.Scripts.ECS.UnityComponents;
using UnityEngine;

namespace Asteroids.Scripts.ECS.Services
{
    [Serializable]
    public class AsteroidSpawnerConfig
    {
        [SerializeField] private AsteroidView prefabForSpawn;
        [SerializeField] private int startCapacity = 20;
        [SerializeField] private int maxCapacity = 30;
        [SerializeField] private Vector2 spawnInterval;
        [SerializeField] private List<AsteroidSpawnData> asteroidSpawnData;
        
        public AsteroidView PrefabForSpawn => prefabForSpawn;
        public int StartCapacity => startCapacity;
        public int MaxCapacity => maxCapacity;
        public Vector2 SpawnInterval => spawnInterval;
        public List<AsteroidSpawnData> AsteroidSpawnData => asteroidSpawnData;
    }
}