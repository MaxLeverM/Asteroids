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

        private void Start()
        {
            againButton.onClick.AddListener(ReloadScene);
        }

        public void ShowGameOverScreen(int score)
        {
            finalScoreDrawer.Draw(score);
            canvasGroup.Active(true);
        }

        private void ReloadScene() //TODO: temporary solution, make SceneLoader service
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}