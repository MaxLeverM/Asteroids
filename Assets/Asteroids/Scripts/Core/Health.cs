using System;
using Asteroids.Scripts.Core.Utilities;
using UnityEngine;

namespace Asteroids.Scripts.Core
{
    [Serializable]
    public class Health
    {
        [SerializeField] private float hp;
        [SerializeField] private float maxHp;

        public float HP
        {
            get => hp;
            set
            {
                var tempHp = Math.Clamp(value, 0, maxHp);
                if (ProjectMath.NearlyEqual(hp, tempHp))
                    return;
                hp = tempHp;
                OnHealthChanged?.Invoke(hp);
                if (hp <= 0)
                    OnHealthEnded?.Invoke();
            }
        }

        public float MaxHp
        {
            get => maxHp;
            set => maxHp = value;
        }

        public event Action<float> OnHealthChanged;
        public event Action OnHealthEnded;

        public void Damage(float damage)
        {
            HP -= damage;
        }

        public void Restore(float restorePoint)
        {
            HP += restorePoint;
        }
    }
}