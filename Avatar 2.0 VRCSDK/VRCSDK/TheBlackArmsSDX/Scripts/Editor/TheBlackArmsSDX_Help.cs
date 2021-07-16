using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TheBlackArmsSDX
{
    public class TheBlackArmsSDX_Help
    {

        [MenuItem("Phoenix/Help/Discord", false, 1049)]
        public static void OpenDiscordLink()
        {
            Application.OpenURL("https://chillzone.live/all-sdk/discord");
        }
        
        [MenuItem("Phoenix/Help/How to upload: Avatar", false, 1050)]
        public static void OpenAvatarLink()
        {
            Application.OpenURL("https://chillzone.live/all-sdk/tutorialAvatar/");
        }
        
        [MenuItem("Phoenix/Help/How to upload: World", false, 1051)]
        public static void OpenWorldLink()
        {
            Application.OpenURL("https://chillzone.live/all-sdk/tutorialWorld");
        }
        
        [MenuItem("Phoenix/Help/How to update: SDK", false, 1052)]
        public static void OpenSDKUpdateLink()
        {
            Application.OpenURL("https://chillzone.live/all-sdk/updateSDK");
        }
        
        [MenuItem("Phoenix/Help/How to update: Assets", false, 1053)]
        public static void OpenAssetUpdateLink()
        {
            Application.OpenURL("https://chillzone.live/all-sdk/updateAssets");
        }
        
        [MenuItem("Phoenix/Utilities/Update configs", false, 1000)]
        public static void ForceUpdateConfigs()
        {
            TheBlackArmsSDX_ImportManager.updateConfig();
        }

    }
}