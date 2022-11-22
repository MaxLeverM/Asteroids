using Asteroids.Scripts.Core.Interfaces;
using Asteroids.Scripts.Core.Starship;
using UnityEngine;

namespace Asteroids.Scripts.Core
{
    public class UfoEnemyAI : IEnemyAI
    {
        private Transform ufo;
        private Transform target;
        private SpaceEngine spaceEngine;

        public UfoEnemyAI(Transform origin, SpaceEngine spaceEngine)
        {
            ufo = origin;
            this.spaceEngine = spaceEngine;
        }

        public void Update()
        {
            spaceEngine.Move((target.position - ufo.position).normalized);
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}