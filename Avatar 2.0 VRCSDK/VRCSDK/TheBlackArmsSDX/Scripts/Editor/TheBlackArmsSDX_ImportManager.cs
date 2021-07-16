using UnityEngine;
using System.IO;
using System.Net;
using System;
using System.ComponentModel;
using TheBlackArmsSDX;
using UnityEngine;
using UnityEditor;

namespace TheBlackArmsSDX
{
    public class TheBlackArmsSDX_ImportManager
    {
        public static string configName = "importConfig.json";
        public static string serverUrl = "https://chillzone.live/theblackarms/assets/";
        public static string internalServerUrl = "https://chillzone.live/theblackarms/assets/";

        public static void downloadAndImportAssetFromServer(string assetName)
        {
            if (File.Exists(TheBlackArmsSDX_Settings.getAssetPath() + assetName))
            {
                TbaLog(assetName + " exists. Importing it..");
                importDownloadedAsset(assetName);
            }
            else
            {
                TbaLog(assetName + " does not exist. Starting download..");
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
            w.DownloadFileAsync(new Uri(url), TheBlackArmsSDX_Settings.getAssetPath() + assetName);
        }

        public static void deleteAsset(string assetName)
        {
            File.Delete(TheBlackArmsSDX_Settings.getAssetPath() + assetName);
        }

        public static void updateConfig()
        {
            WebClient w = new WebClient();
            w.Headers.Set(HttpRequestHeader.UserAgent, "Webkit Gecko wHTTPS (Keep Alive 55)");
            w.DownloadFileCompleted += configDownloadCompleted;
            w.DownloadProgressChanged += fileDownloadProgress;
            string url = internalServerUrl + configName;
            w.DownloadFileAsync(new Uri(url), TheBlackArmsSDX_Settings.projectConfigPath + "update_" + configName);
        }

        private static void configDownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                //var updateFile = File.ReadAllText(TheBlackArmsSDX_Settings.projectConfigPath + "update_" + configName);
                File.Delete(TheBlackArmsSDX_Settings.projectConfigPath + configName);
                File.Move(TheBlackArmsSDX_Settings.projectConfigPath + "update_" + configName,
                    TheBlackArmsSDX_Settings.projectConfigPath + configName);
                TheBlackArmsSDX_ImportPanel.LoadJson();

                EditorPrefs.SetInt("TheBlackArmsSDX_configImportLastUpdated", (int) DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                TbaLog("Import Config has been updated!");
            }
            else
            {
                TbaLog("Import Config could not be updated!");
            }
        }

        private static void fileDownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            string assetName = ((WebClient) sender).QueryString["assetName"];
            if (e.Error == null)
            {
                TbaLog("Download of file " + assetName + " completed!");
            }
            else
            {
                deleteAsset(assetName);
                TbaLog("Download of file " + assetName + " failed!");
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
            if (EditorPrefs.HasKey("TheBlackArmsSDX_configImportLastUpdated"))
            {
                var lastUpdated = EditorPrefs.GetInt("TheBlackArmsSDX_configImportLastUpdated");
                var currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

                if (currentTime - lastUpdated < 3600)
                {
                    Debug.Log("Not updating config: " + (currentTime - lastUpdated));
                    return;
                }
            }
            TbaLog("Updating import config");
            updateConfig();
        }

        private static void TbaLog(string message)
        {
            Debug.Log("[TheBlackArmsSDX] AssetDownloadManager: " + message);
        }

        public static void importDownloadedAsset(string assetName)
        {
            AssetDatabase.ImportPackage(TheBlackArmsSDX_Settings.getAssetPath() + assetName, true);
        }
    }
}