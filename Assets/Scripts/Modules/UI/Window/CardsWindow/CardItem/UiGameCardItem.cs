using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.UI.Window.CardsWindow.CardItem
{
    public class UiGameCardItem : PoolableItem
    {
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Image _frontSprite;
        [SerializeField] private Image _backSprite;

        [SerializeField] private Button _button;
        
        private CardData _data;
        private bool _isInteractive;

        public CardData Data => _data;
        public bool IsInteractive => _isInteractive;

        public event Action<UiGameCardItem> OnCardSelected; 

        public void Init(CardData cardData)
        {
            _data = cardData;
            ButtonSubscribe();
        }

        public void SetImage(Sprite frontSprite)
        {
            _frontSprite.sprite = frontSprite;
        }

        public void SetInteractive(bool state)
        {
            _button.interactable = state;
            _isInteractive = state;
        }

        public void Flip(float delay = 0)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DOScaleX(0, 0.25f));
            sequence.AppendCallback(() =>
            {
                _frontSprite.enabled = !_frontSprite.enabled;
                _backSprite.enabled = !_backSprite.enabled;
            });
            sequence.Append(transform.DOScaleX(1, 0.25f));

            sequence.Play().SetDelay(delay);
        }

        public void Show(bool value) {
            gameObject.SetActive(value);
        }

        public void Fade(float delay = 0)
        {
            _backgroundImage.DOFade(0,0.5f).SetDelay(delay);
            _frontSprite.DOFade(0,0.5f).SetDelay(delay);
            _backSprite.DOFade(0,0.5f).SetDelay(delay);
        }

        private void ButtonUnsubscribe()
        {
            _button.onClick.RemoveListener(OnButtonClickHandler);
        }

        private void ButtonSubscribe()
        {
            _button.onClick.AddListener(OnButtonClickHandler);
        }

        private void OnButtonClickHandler()
        {
            OnCardSelected?.Invoke(this);
            Flip();
        }

        private void ResetFade()
        {
            Color c = _frontSprite.color;
            _frontSprite.color = new Color(c.r, c.g, c.b, 1f);
            c = _backSprite.color;
            _backSprite.color =  new Color(c.r, c.g, c.b, 1f);
            c = _backgroundImage.color;
            _backgroundImage.color = new Color(c.r, c.g, c.b, 1f);
        }

        private void ResetImage()
        {
            _frontSprite.sprite = null;
            _backSprite.sprite = null;
        }

        public override void Free()
        {
            ButtonUnsubscribe();
            DOTween.Kill(gameObject);
            ResetFade();
            ResetImage();
            base.Free();
        }
        
    }
}
