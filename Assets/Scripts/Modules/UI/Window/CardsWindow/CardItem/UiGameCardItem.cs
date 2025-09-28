using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.UI.Window.CardsWindow.CardItem
{
    public class UiGameCardItem : PoolableItem
    {
        [SerializeField] private Image _frontSprite;
        [SerializeField] private Image _backSprite;

        [SerializeField] private Button _button;
        
        private CardData _data;

        public CardData Data => _data;
        
        public void Init(CardData cardData)
        {
            _data = cardData;
            ButtonSubscribe();
        }

        public void SetImage(Sprite frontSprite)
        {
            _frontSprite.sprite = frontSprite;
        }
        
        public void Flip()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DOScaleX(0, 0.25f));
            sequence.AppendCallback(() =>
            {
                _frontSprite.enabled = !_frontSprite.enabled;
                _backSprite.enabled = !_backSprite.enabled;
            });
            sequence.Append(transform.DOScaleX(1, 0.25f));

            sequence.Play();
        }

        public void Show(bool value) {
            gameObject.SetActive(value);
        }

        private void Fade()
        {
            _frontSprite.DOFade(0,0.5f);
            _backSprite.DOFade(0,0.5f);
        }

        private void ButtonUnsubscribe()
        {
            _button.onClick.RemoveListener(Flip);
        }

        private void ButtonSubscribe()
        {
            _button.onClick.AddListener(Flip);
        }

        public override void Free()
        {
            ButtonUnsubscribe();
            DOTween.Kill(gameObject);
            _frontSprite = null;
            _backSprite = null;
            base.Free();
        }
        
    }
}
