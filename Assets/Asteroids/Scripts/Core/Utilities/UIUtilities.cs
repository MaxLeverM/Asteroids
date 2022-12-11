using UnityEngine;

namespace Asteroids.Scripts.Core.Utilities
{
    public static class UIUtilities
    {
        public static void Active(this CanvasGroup canvasGroup, bool status)
        {
            canvasGroup.alpha = status ? 1f : 0f;
            canvasGroup.interactable = status;
            canvasGroup.blocksRaycasts = status;
        }
    }
}