using System;
using System.IO;
using Cysharp.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using UnityEngine;
using UnityEngine.Networking;

namespace Modules.Tools.GoogleDrive
{
    public class GoogleDriveHelper : MonoBehaviour
    {
        public static DriveService GetDriveService()
        {
            
            var credential = LoadCredentialAsset();

            return new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "UnityDriveEditor"
            });

        }

        private static GoogleCredential LoadCredentialAsset()
        {
            TextAsset credentialAsset = Resources.Load<TextAsset>("DriveAuth/credentials");
            string json = credentialAsset.text;
            return CreateFileFromStream(json);
        }

        private static GoogleCredential CreateFileFromStream(string json)
        {
            var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json));
            var credential = GoogleCredential.FromStream(stream)
                .CreateScoped(DriveService.Scope.Drive);
            return credential;
        }

        private static string GetLinkById(string fileId)
        {
            string url = $"https://drive.google.com/uc?export=download&id={fileId}";
            return url;
        }

        public static async UniTask<string> DownloadJson(string fileId)
        {
            
            using (UnityWebRequest request = UnityWebRequest.Get(GetLinkById(fileId)))
            {
                await request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError("Error while downloading!: " + request.error);
                    return null;
                }

                return request.downloadHandler.text;
            }
        }

        public static void UploadJson(string fileId, string jsonContent)
        {
            var service = GetDriveService();
            var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(jsonContent));
            stream.Position = 0;
          
            var updateRequest = service.Files.Update(null, fileId, stream, "application/json");
            updateRequest.Upload();
            
            Debug.Log("Json successfully Update!!! ");
        }
        
        public static async UniTask<Texture2D> DownloadImage(string fileId)
        {
            using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(GetLinkById(fileId)))
            {
                await request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"Error while downloading image!: {request.error}");
                    return null;
                }

                return DownloadHandlerTexture.GetContent(request);
            }
            
        }


    }
}
