using System.Linq;
using UnityEngine;
using UnityEngine.UI;


namespace Modules.UI.Window.LoadingScreen
{
   public class UiLoadBar : MonoBehaviour
   {
      [SerializeField] private Image _progressBar;
      
      [SerializeField] private int _currentLoaded;
      [SerializeField] private int _targetLoaded;

      public void Init()
      {
         _progressBar.sprite = CreateBarSprite(Color.red);
         _progressBar.type = Image.Type.Filled;
         _progressBar.fillMethod = Image.FillMethod.Horizontal;
         _progressBar.fillAmount = 0;

      }

      public Sprite CreateBarSprite(Color color, int width = 1, int height = 1)
      {
         Texture2D texture = new Texture2D(width, height);
         Color[] pixels = Enumerable.Repeat(color, width * height).ToArray();
         texture.SetPixels(pixels);
         texture.Apply();

         Rect rect = new Rect(0, 0, width, height);
         return Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
      }

      
      public void UpdateBar()
      {
         _currentLoaded++;
         _progressBar.fillAmount = (float)_currentLoaded / _targetLoaded;
      }

      public void SetTarget(int targetLoaded)
      {
         _targetLoaded = targetLoaded;
      }
   }
}
