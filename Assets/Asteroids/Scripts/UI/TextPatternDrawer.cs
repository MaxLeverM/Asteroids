using TMPro;
using UnityEngine;

namespace Asteroids.Scripts.UI
{
    public class TextPatternDrawer : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private string pattern;
        
        public void Draw(params object[] values)
        {
            text.text = string.Format(pattern, values);
        }
    }
}