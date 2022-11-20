using Asteroids.Scripts.Core;
using UnityEngine;

namespace Asteroids.Scripts.Gameplay.GameField
{
    public class GameField : MonoBehaviour
    {
        [SerializeField] private Starship starship;
        [SerializeField] private Asteroid asteroid;
        private FieldBorder fieldBorder;

        private void Start()
        {
            var fieldBound = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            fieldBorder = new FieldBorder(fieldBound);
            fieldBorder.Add(starship.MovableSpaceObject);
            fieldBorder.Add(asteroid.SpaceObject);
        }

        private void FixedUpdate()
        {
            fieldBorder.CheckBorderIntersection();
        }
    }
}