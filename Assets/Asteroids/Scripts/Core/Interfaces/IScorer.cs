using System;

namespace Asteroids.Scripts.Core.Interfaces
{
    public interface IScorer
    {
        public event Action<int> OnPointsAwarded;
    }
}