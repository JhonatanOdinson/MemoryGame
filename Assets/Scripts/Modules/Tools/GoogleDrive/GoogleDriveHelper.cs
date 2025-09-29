using System;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using UnityEngine;

namespace Modules.Tools.GoogleDrive
{
    public class GoogleDriveHelper : MonoBehaviour
    {
        public static DriveService GetDriveService()
        {
            var credential = GoogleCredential.FromFile("Assets/ScriptableData/DriveAuth/credentials.json")
                .CreateScoped(DriveService.Scope.Drive);

            return new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "UnityDriveEditor"
            });

        }

        public static string DownloadJson(string fileId)
        {
            try
            {
                var service = GetDriveService();

                var request = service.Files.Get(fileId);
                var stream = new MemoryStream();
                request.Download(stream);

                string json = System.Text.Encoding.UTF8.GetString(stream.ToArray());
                
                return json;
            }
            catch (Exception ex)
            {
                Debug.LogError("Error while downloading!: " + ex);
                return null;
            }
        }

        public static void UploadJson(string fileId, string jsonContent)
        {
            var service = GetDriveService();
            var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(jsonContent));
            stream.Position = 0;
            
            var file = service.Files.Get(fileId).Execute();
            Debug.Log("MIME: " + file.MimeType);


            var updateRequest = service.Files.Update(null, fileId, stream, "application/json");
            updateRequest.Upload();



            Debug.Log("Json successfully Update!!! ");
        }
        
        public static Texture2D DownloadImage(string fileId)
        {
            try
            {
                var service = GetDriveService();
                var request = service.Files.Get(fileId);
                var stream = new MemoryStream();
                request.Download(stream);

                byte[] imageData = stream.ToArray();
                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(imageData);
                
                return texture;
            }
            catch (Exception ex)
            {
                Debug.LogError("Error while downloading image: " + ex.Message);
                return null;
            }
        }


    }
}
