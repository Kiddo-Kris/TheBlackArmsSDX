using UnityEngine;
using System.IO;
using System.Net;
using System;
using System.ComponentModel;
using UnityEditor;

namespace Sentinels
{
    public class Sentinels_ImportManager
    {
        private static string localPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Unity/Sentinels/";
        private static string localDownloadPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Unity/Sentinels/";
        private static string urlStart = "http://www.sentinels.xyz/uploads/2/0/9/0/20909832/";

        public static void DownloadAsset(string assetName)
        {
            if (File.Exists(localDownloadPath + assetName))
            {
                SentLog(assetName + " exists. Replacing it..");
                File.Delete(localDownloadPath + assetName);
                DownloadFile(assetName);
            }
            else
            {
                SentLog(assetName + " does not exist. Starting download..");
                DownloadFile(assetName);
            }
        }

        public static void ImportAsset(string assetName)
        {
            if (File.Exists(localDownloadPath + assetName))
            {
                SentLog(assetName + " exists. Importing it..");
                ImportDownloadedAsset(assetName);
            }
            else
            {
                SentLog(assetName + " does not exist. Please download first.");
            }
        }

        private static void DownloadFile(string assetName)
        {
            WebClient w = new WebClient();
            w.Headers.Set(HttpRequestHeader.UserAgent, "Webkit Gecko wHTTPS (Keep Alive 55)");
            w.QueryString.Add("assetName", assetName);
            w.DownloadFileCompleted += FileDownloadCompleted;
            w.DownloadProgressChanged += FileDownloadProgress;
            string url = urlStart + assetName;
            w.DownloadFileAsync(new Uri(url), localDownloadPath + assetName);
        }

        private static void FileDownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            string assetName = ((WebClient)(sender)).QueryString["assetName"];
            SentLog("Download of file " + assetName + " completed!");
        }

        private static void FileDownloadProgress(object sender, DownloadProgressChangedEventArgs e)
        {
            var progress = e.ProgressPercentage;
            var assetName = ((WebClient)sender).QueryString["assetName"];
            if (progress < 0) return;
            if (progress >= 100)
            {
                #if UNITY_EDITOR
                EditorUtility.ClearProgressBar();
                #endif
            }
            else
            {
                #if UNITY_EDITOR
                EditorUtility.DisplayProgressBar("Download of " + assetName,
                    "Downloading " + assetName + ". Currently at: " + progress + "%",
                    (progress / 100F));
                #endif
            }
        }

        private static void SentLog(string message)
        {
            Debug.Log("[Sentinels] AssetDownloader: " + message);
        }

        public static void importAsset(string assetName)
        {
            #if UNITY_ENGINE
            AssetDatabase.ImportPackage(localPath + assetName, true);
            #endif
        }

        public static void ImportDownloadedAsset(string assetName)
        {
            #if UNITY_ENGINE
            AssetDatabase.ImportPackage(localDownloadPath + assetName, true);
            #endif
        }
    }
}