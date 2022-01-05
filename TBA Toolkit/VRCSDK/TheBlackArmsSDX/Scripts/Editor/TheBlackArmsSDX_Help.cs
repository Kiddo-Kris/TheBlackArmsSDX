using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace theblackarmsSDX
{
    public class theblackarmsSDX_Help
    {

        [MenuItem("theblackarmsSDX/Help/Discord", false, 1049)]
        public static void OpenDiscordLink()
        {
            Application.OpenURL("https://trigon.systems/all-sdk/discord");
        }
        
        [MenuItem("theblackarmsSDX/Help/How to upload: Avatar", false, 1050)]
        public static void OpenAvatarLink()
        {
            Application.OpenURL("https://trigon.systems/all-sdk/tutorialAvatar/");
        }
        
        [MenuItem("theblackarmsSDX/Help/How to upload: World", false, 1051)]
        public static void OpenWorldLink()
        {
            Application.OpenURL("https://trigon.systems/all-sdk/tutorialWorld");
        }
        
        [MenuItem("theblackarmsSDX/Help/How to update: SDK", false, 1052)]
        public static void OpenSDKUpdateLink()
        {
            Application.OpenURL("https://trigon.systems/all-sdk/updateSDK");
        }
        
        [MenuItem("theblackarmsSDX/Help/How to update: Assets", false, 1053)]
        public static void OpenAssetUpdateLink()
        {
            Application.OpenURL("https://trigon.systems/all-sdk/updateAssets");
        }
        
        [MenuItem("theblackarmsSDX/Utilities/Update configs", false, 1000)]
        public static void ForceUpdateConfigs()
        {
            theblackarmsSDX_ImportManager.updateConfig();
        }
        public static void UpdatesdkBtn()
        {

            theblackarmsSDX_AutomaticUpdateAndInstall.AutomaticSDKInstaller();
        }


    }
}