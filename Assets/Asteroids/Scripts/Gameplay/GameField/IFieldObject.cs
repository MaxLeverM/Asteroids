using UnityEngine;

namespace Asteroids.Scripts.Gameplay.GameField
{
    public interface IFieldObject
    {
        public Vector2 Position { get; set; }
    }
}