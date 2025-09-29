using System.Collections.Generic;
using Core.GameEvents;
using Cysharp.Threading.Tasks;
using Modules.Tools.GoogleDrive;
using Modules.UI.Window.CardsWindow.CardItem;
using Newtonsoft.Json;
using UnityEngine;

namespace Core
{
    public class ResourceController : MonoBehaviour
    {
        [SerializeField] private Dictionary<string, Texture2D> _imageCache = new();
        [SerializeField] private List<CardData> _jsonCardData = new();

        public List<CardData> JsonCardData => _jsonCardData;

        [SerializeField] private GameEvent OnCacheStart;
        [SerializeField] private GameEvent OnCacheUpdated;
        [SerializeField] private GameEvent OnCacheComplete;

        public async UniTask Init()
        {
            string jsonUrl = GameDirector.GetGameConfig.JsonUrl;
            _jsonCardData = await CommonComponents.ResourceController.ParseFromJson<CardData>(jsonUrl);
            OnCacheStart.Check(null,_jsonCardData.Count);
            await LoadAndCachedImage();
            OnCacheComplete.Check(null, null);
        }

        private async UniTask LoadAndCachedImage()
        {
            foreach (var cardData in _jsonCardData)
            {
                Texture2D texture2D =  await LoadImage(cardData.Url);
                _imageCache[cardData.CardId] = texture2D;
                OnCacheUpdated.Check(null,null);
            }
        }

        

        public async UniTask<List<T>> ParseFromJson<T>(string jsonId)
        {
            string content = await GoogleDriveHelper.DownloadJson(jsonId);
            List<T> resultList = JsonConvert.DeserializeObject<List<T>>(content);

            return resultList;
        }

        public Sprite GetImage(string fileId)
        {
            if (_imageCache.TryGetValue(fileId, out Texture2D cached))
                return TextureToSprite(cached);
            return null;
        }

        public Sprite TextureToSprite(Texture2D texture2D)
        {
            Sprite sprite = Sprite.Create(texture2D,
                new Rect(0, 0, texture2D.width, texture2D.height),
                new Vector2(0.5f, 0.5f)  );

            return sprite;
        }

        private async UniTask<Texture2D> LoadImage(string fileId)
        {
           return await GoogleDriveHelper.DownloadImage(fileId);
        }

        public void Free()
        {
            _imageCache.Clear();
        }
    }
}
