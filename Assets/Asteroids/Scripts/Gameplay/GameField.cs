using Asteroids.Scripts.Core;
using UnityEngine;

namespace Asteroids.Scripts.Gameplay
{
    public class GameField : MonoBehaviour
    {
        [SerializeField] private Starship starship;
        [SerializeField] private FieldBound fieldBound;
        [SerializeField] private Spawner asteroidSpawner;
        private FieldBorder fieldBorder;

        private void Start()
        {
            fieldBorder = new FieldBorder(fieldBound.ExtremePoint);
            fieldBorder.Add(starship.MovableSpaceObject);
            asteroidSpawner.Init(this, fieldBound.ExtremePoint, fieldBorder.Add, fieldBorder.Remove);
        }

        private void FixedUpdate()
        {
            fieldBorder.CheckBorderIntersection();
        }
    }
}