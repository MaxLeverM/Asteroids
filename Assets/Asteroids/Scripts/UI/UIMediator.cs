using System;
using Asteroids.Scripts.Gameplay;
using UnityEngine;

namespace Asteroids.Scripts.UI
{
    public class UIMediator : MonoBehaviour
    {
        [SerializeField] private GameOverView gameOverView;
        [SerializeField] private GameStatisticView gameStatisticView;

        [SerializeField] private Player player;
        [SerializeField] private Starship starship;

        public GameOverView GameOverView => gameOverView;
        public GameStatisticView GameStatisticView => gameStatisticView;

        private void Start()
        {
         //   gameStatisticView.Init(player, starship);
         //   gameOverView.Init(player);
        }
    }
}