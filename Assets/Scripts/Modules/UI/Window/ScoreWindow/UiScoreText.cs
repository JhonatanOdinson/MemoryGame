using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Modules.UI.Window.ScoreWindow
{
    public class UiScoreText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        private Sequence _sequence;
        
        public void Init()
        {
            _sequence  = DOTween.Sequence();
            _sequence.SetAutoKill(false);
            CreateSequence();
        }

        public void UpdateScore(int score)
        {
            Animate();
            string textFormatting = string.Format("Score: {0}", score);
            _text.text = textFormatting;
            
        }

        private void CreateSequence()
        {
            _sequence.Append(transform.DOScale(Vector3.one * 2, 0.5f));
            _sequence.Append(transform.DOScale(Vector3.one, 0.5f));
        }

        private void Animate()
        {
            transform.localScale = Vector3.one;
            _sequence.Restart();
        }

        public void Destruct()
        {
            DOTween.Kill(gameObject);
        }
    }
}
