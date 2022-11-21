using System;
using UnityEngine;

namespace Asteroids.Scripts.Core
{
    [Serializable]
    public class FieldBound
    {
        [SerializeField] private Vector2 offset;
        private Vector2 extremePoint;

        public Vector2 ExtremePoint
        {
            get
            {
                if (extremePoint == default && Camera.main != null)
                {
                    extremePoint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
                    extremePoint += offset;
                }
                return extremePoint;
            }
        }
    }
}