using System;
using UnityEngine;

namespace Asteroids.Scripts.Core
{
    [Serializable]
    public class ScoreHandler
    {
        [SerializeField] private int score;

        public Action<int> OnScoreValueChanged;
        
        public int Score
        {
            get => score;
            set
            {
                if(value<0)
                    return;
                score = value;
                OnScoreValueChanged?.Invoke(score);
            }
        }

        public void AddScore(int scoreToAdd)
        {
            Score += scoreToAdd;
        }
    }
}