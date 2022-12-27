using UnityEngine;

namespace Asteroids.Scripts.ECS.UnityComponents
{
    public class BulletView : BaseView
    {
        [SerializeField] private CircleCollider2D bulletCollider;

        public CircleCollider2D BulletCollider => bulletCollider;
    }
}