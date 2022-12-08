using System;
using Asteroids.Scripts.Core;
using Asteroids.Scripts.Core.Interfaces;
using UnityEngine;

namespace Asteroids.Scripts.Gameplay
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private ScoreHandler scoreHandler;
        [SerializeField] private Starship starship;
        
        public ScoreHandler ScoreHandler => scoreHandler;

        public event Action OnGameOver;

        private void Awake()
        {
            if (starship is IScorer scorer)
            {
                scorer.OnPointsAwarded += scoreHandler.AddScore;
            }

            if (starship is IDestroyable destroyableStarship)
            {
                destroyableStarship.DestroyCalled += StarshipDestroyed;
            }
        }

        private void StarshipDestroyed(GameObject obj)
        {
            OnGameOver?.Invoke();
        }
    }
}