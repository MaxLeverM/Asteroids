using UnityEngine;

namespace Asteroids.Scripts.ECS.UnityComponents
{
    public class AsteroidView : BaseView
    {
        [SerializeField] private CircleCollider2D asteroidCollider;

        public CircleCollider2D AsteroidCollider => asteroidCollider;
    }
}