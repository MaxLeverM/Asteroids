using System;

namespace Asteroids.Scripts.Core.Interfaces
{
    public interface IScorer
    {
        public Action<int> OnPointsAwarded { get; set; }
    }
}