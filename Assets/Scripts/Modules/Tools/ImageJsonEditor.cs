using System.Collections.Generic;
using EditorScripts.GoogleDrive;
using Modules.UI.Window.CardsWindow.CardItem;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace Modules.Tools
{
    public class ImageJsonEditor : EditorWindow
    {
        private List<CardData> entries = new List<CardData>();
        private string driveJsonId = "1blPnqwKdtrjbA_eOA7vAsPpm5lWnvbRS";


        [MenuItem("Tools/Image JSON Editor")]
        public static void ShowWindow()
        {
            GetWindow<ImageJsonEditor>("Image JSON Editor");
        }

        private void OnGUI()
        {
            GUILayout.Label("Image Entries", EditorStyles.boldLabel);

            if (GUILayout.Button("Load JSON"))
            {
                LoadJsonFromDrive();

            }

            if (GUILayout.Button("Save JSON"))
            {
                SaveJsonToDrive();
            }

            if (GUILayout.Button("Add New Entry"))
            {
                entries.Add(new CardData());
            }

            if (entries is null) return;

            for (int i = 0; i < entries.Count; i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("ID", GUILayout.Width(40));
                entries[i].CardId = GUILayout.TextField(entries[i].CardId, GUILayout.Width(100));
                GUILayout.Label("URL", GUILayout.Width(40));
                entries[i].Url = GUILayout.TextField(entries[i].Url, GUILayout.ExpandWidth(true));

                if (GUILayout.Button("X", GUILayout.Width(20)))
                {
                    entries.RemoveAt(i);
                }
                GUILayout.EndHorizontal();
            }
        }

        private void LoadJsonFromDrive()
        {
            string json = GoogleDriveHelper.DownloadJson(driveJsonId);
            entries = JsonConvert.DeserializeObject<List<CardData>>(json);
        }
        
        private void SaveJsonToDrive()
        {
            string json = JsonConvert.SerializeObject(entries, Formatting.Indented);
            GoogleDriveHelper.UploadJson(driveJsonId, json);
        }

    }
}