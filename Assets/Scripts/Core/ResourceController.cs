using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Modules.Tools.GoogleDrive;
using Newtonsoft.Json;
using UnityEngine;

namespace Core
{
    public class ResourceController : MonoBehaviour
    {
        private Dictionary<string, Texture2D> _imageCache = new();

        public void Init()
        {
            
        }

        public List<T> ParseFromJson<T>(string jsonId)
        {
            string content = GoogleDriveHelper.DownloadJson(jsonId);
            List<T> resultList = JsonConvert.DeserializeObject<List<T>>(content);

            return resultList;
        }

        public async UniTask<Sprite> GetImage(string fileId, string fileUrl)
        {
            if (_imageCache.TryGetValue(fileId, out Texture2D cached))
                return TextureToSprite(cached);

            Texture2D texture2D = await LoadImage(fileUrl);
            _imageCache[fileId] = texture2D;
            return TextureToSprite(texture2D);

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
           return GoogleDriveHelper.DownloadImage(fileId);
        }

        public void Free()
        {
            _imageCache.Clear();
        }
    }
}
