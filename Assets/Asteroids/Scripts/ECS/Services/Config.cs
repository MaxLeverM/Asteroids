using Asteroids.Scripts.ECS.UnityComponents;
using UnityEngine;

namespace Asteroids.Scripts.ECS.Services
{
    [CreateAssetMenu(fileName = "Config", menuName = "Game/Config")]
    public class Config : ScriptableObject, IConfig
    {
        [SerializeField] private StarshipView starshipView;
        [SerializeField] private BulletView bulletView;
        
        public StarshipView Starship => starshipView;
        public BulletView Bullet => bulletView;
    }
}