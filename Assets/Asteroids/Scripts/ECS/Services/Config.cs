using Asteroids.Scripts.ECS.UnityComponents;
using UnityEngine;

namespace Asteroids.Scripts.ECS.Services
{
    [CreateAssetMenu(fileName = "Config", menuName = "Game/Config")]
    public class Config : ScriptableObject, IConfig
    {
        [SerializeField] private StarshipView starshipView;
        
        public StarshipView Starship => starshipView;
    }
}