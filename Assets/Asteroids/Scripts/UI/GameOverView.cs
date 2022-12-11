using System;
using Asteroids.Scripts.Core;
using Asteroids.Scripts.Core.Utilities;
using Asteroids.Scripts.Gameplay;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Asteroids.Scripts.UI
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private TextPatternDrawer finalScoreDrawer;
        [SerializeField] private Button againButton;
        [SerializeField] private CanvasGroup canvasGroup;

        private Player player;
        
        public void Init(Player player)
        {
            this.player = player;
            
            player.OnGameOver += ShowGameOverScreen;
            againButton.onClick.AddListener(ReloadScene);
        }

        private void ShowGameOverScreen()
        {
            finalScoreDrawer.Draw(player.ScoreHandler.Score);
            canvasGroup.Active(true);
        }

        private void ReloadScene() //TODO: temporary solution, make SceneLoader service
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}