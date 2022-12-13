using Asteroids.Scripts.ECS.UnityComponents;
using UnityEngine;

namespace Asteroids.Scripts.ECS.Services
{
    public interface IConfig
    {
        public StarshipView Starship { get; }
    }
}