using DG.Tweening;
using Library.Scripts.Core;
using Modules.UI.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.UI.Window.IntroTipsWindow
{
    public class UiIntroTipsWindow : WindowBase
    {
        [SerializeField] private UiIntroTipsProvider _provider;
        [SerializeField] private UiIntroTipsHandler _handler;
        [SerializeField] private Image _bg;
        [SerializeField] private AimTips _aimTips;
        public override void Init()
        {
            _provider.Init(this);
            _handler.Init(this);
        }

        public void OnConfirmClickHandler()
        {
            _handler.ConfirmClickHandler();
        }

        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public void ShowBg(bool state)
        {
            if(state) _aimTips.PlayTips();
            else _aimTips.StopTips();
            
            _bg.DOFade(state ? 0.5f : 0f, 0.5f).SetId("ShowBg");
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        public void KillTweens()
        {
            DOTween.Kill("ShowBg",true);
        }

        public override IWindowProvider GetProvider()
        {
            return _provider;
        }

        public override void FreeWindow()
        {
         
        }
        
        public override void Destruct()
        {
            KillTweens();
        }
    }
}
