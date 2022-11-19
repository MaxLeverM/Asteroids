using UnityEngine;

namespace Asteroids.Scripts.Gameplay.GameField
{
    public class GameField : MonoBehaviour
    {
        [SerializeField] private Starship starship;
        private FieldBorder fieldBorder;

        private void Start()
        {
            var fieldBound = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            fieldBorder = new FieldBorder(fieldBound);
            fieldBorder.Add(starship);
        }

        private void FixedUpdate()
        {
            fieldBorder.CheckBorderIntersection();
        }
    }
}