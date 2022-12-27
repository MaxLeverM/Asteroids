using Asteroids.Scripts.ECS.UnityComponents;
using UnityEngine;

namespace Asteroids.Scripts.ECS.Services
{
    [CreateAssetMenu(fileName = "Config", menuName = "Game/Config")]
    public class Config : ScriptableObject, IConfig
    {
        [SerializeField] private StarshipView starshipView;
        [SerializeField] private BulletView bulletView;
        [SerializeField] private AsteroidSpawnerConfig asteroidSpawnerConfig;
        
        public StarshipView Starship => starshipView;
        public BulletView Bullet => bulletView;
        public AsteroidSpawnerConfig AsteroidSpawnerConfig => asteroidSpawnerConfig;
    }
}