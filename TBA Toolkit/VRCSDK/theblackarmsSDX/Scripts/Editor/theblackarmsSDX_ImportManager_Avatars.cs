using UnityEngine;
using System.IO;
using System.Net;
using System;
using System.ComponentModel;
using theblackarmsSDX;
using UnityEditor;

namespace theblackarmsSDX
{
    public class theblackarmsSDX_ImportManager_Avatars
    {
        private const string V = "https://trigon.systems/all-sdk/assets/avatars/";
        public static string configName = "importConfig_avatars.json";
        public static string serverUrl = V;
        public static string internalServerUrl = V;

        public static void downloadAndImportAssetFromServer(string assetName)
        {
            if (File.Exists(theblackarmsSDX_Settings.getAssetPath() + assetName))
            {
                Chill_zoneLog(assetName + " exists. Importing it..");
                importDownloadedAsset(assetName);
            }
            else
            {
                Chill_zoneLog(assetName + " does not exist. Starting download..");
                downloadFile(assetName);
            }
        }

        private static void downloadFile(string assetName)
        {
            WebClient w = new WebClient();
            w.Headers.Set(HttpRequestHeader.UserAgent, "Webkit Gecko wHTTPS (Keep Alive 55)");
            w.QueryString.Add("assetName", assetName);
            w.DownloadFileCompleted += fileDownloadCompleted;
            w.DownloadProgressChanged += fileDownloadProgress;
            string url = serverUrl + assetName;
            w.DownloadFileAsync(new Uri(url), theblackarmsSDX_Settings.getAssetPath() + assetName);
        }

        public static void deleteAsset(string assetName)
        {
            File.Delete(theblackarmsSDX_Settings.getAssetPath() + assetName);
        }

        public static void updateConfig()
        {
            WebClient w = new WebClient();
            w.Headers.Set(HttpRequestHeader.UserAgent, "Webkit Gecko wHTTPS (Keep Alive 55)");
            w.DownloadFileCompleted += configDownloadCompleted;
            w.DownloadProgressChanged += fileDownloadProgress;
            string url = internalServerUrl + configName;
            w.DownloadFileAsync(new Uri(url), theblackarmsSDX_Settings.projectConfigPath + "update_" + configName);
        }

        private static void configDownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                //var updateFile = File.ReadAllText(theblackarmsSDX_Settings.projectConfigPath + "update_" + configName);
                File.Delete(theblackarmsSDX_Settings.projectConfigPath + configName);
                File.Move(theblackarmsSDX_Settings.projectConfigPath + "update_" + configName,
                    theblackarmsSDX_Settings.projectConfigPath + configName);
                theblackarmsSDX_ImportPanel.LoadJson();

                EditorPrefs.SetInt("theblackarmsSDX_configImportLastUpdated", (int) DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                Chill_zoneLog("Import Config has been updated!");
            }
            else
            {
                Chill_zoneLog("Import Config could not be updated!");
            }
        }

        private static void fileDownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            string assetName = ((WebClient) sender).QueryString["assetName"];
            if (e.Error == null)
            {
                Chill_zoneLog("Download of file " + assetName + " completed!");
            }
            else
            {
                deleteAsset(assetName);
                Chill_zoneLog("Download of file " + assetName + " failed!");
            }
        }

        private static void fileDownloadProgress(object sender, DownloadProgressChangedEventArgs e)
        {
            var progress = e.ProgressPercentage;
            var assetName = ((WebClient) sender).QueryString["assetName"];
            if (progress < 0) return;
            if (progress >= 100)
            {
                EditorUtility.ClearProgressBar();
            }
            else
            {
                EditorUtility.DisplayProgressBar("Download of " + assetName,
                    "Downloading " + assetName + ". Currently at: " + progress + "%",
                    (progress / 100F));
            }
        }

        public static void checkForConfigUpdate()
        {
            if (EditorPrefs.HasKey("theblackarmsSDX_configImportLastUpdated"))
            {
                var lastUpdated = EditorPrefs.GetInt("theblackarmsSDX_configImportLastUpdated");
                var currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

                if (currentTime - lastUpdated < 3600)
                {
                    Debug.Log("Not updating config: " + (currentTime - lastUpdated));
                    return;
                }
            }
            Chill_zoneLog("Updating import config");
            updateConfig();
        }

        private static void Chill_zoneLog(string message)
        {
            Debug.Log("[theblackarmsSDX] AssetDownloadManager: " + message);
        }

        public static void importDownloadedAsset(string assetName)
        {
            AssetDatabase.ImportPackage(theblackarmsSDX_Settings.getAssetPath() + assetName, true);
        }
    }
}