using theblackarmsSDX;
using System.IO;
using System.Net.Http;
using UnityEditor;
using UnityEngine;

public class theblackarmsSDX_updateCheck : MonoBehaviour
{
    [InitializeOnLoad]
    public class Startup
    {
        public static string versionURL = "https://trigon.systems/all-sdk/updateSDK/version.txt";
        public static string currentVersion = File.ReadAllText("Assets/VRCSDK/theblackarmsSDX/tbaversions.txt");
        static Startup()
        {
            Check();
        }
        public async static void Check()
        {
            HttpClient httpClient = new HttpClient();
            var result = await httpClient.GetAsync(versionURL);
            var strServerVersion = await result.Content.ReadAsStringAsync();
            var serverVersion = strServerVersion;

            var thisVersion = currentVersion;
            
            if (serverVersion != thisVersion)
            {
                theblackarmsSDX_AutomaticUpdateAndInstall.AutomaticSDKInstaller();
            }
        }
    }
}
