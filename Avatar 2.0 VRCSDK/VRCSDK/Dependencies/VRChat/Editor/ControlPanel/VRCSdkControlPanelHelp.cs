using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.SDKBase;

public partial class VRCSdkControlPanel : EditorWindow
{
    [MenuItem("Phoenix/SDK/FAQ", false, 13)]
    public static void ShowDeveloperFAQ()
    {
        if (!RemoteConfig.IsInitialized())
        {
            RemoteConfig.Init(() => ShowDeveloperFAQ());
            return;
        }

        Application.OpenURL(RemoteConfig.GetString("sdkDeveloperFaqUrl"));
    }

    [MenuItem("Phoenix/SDK/VRC Discord", false, 13)]
    public static void ShowVRChatDiscord()
    {
        if (!RemoteConfig.IsInitialized())
        {
            RemoteConfig.Init(() => ShowVRChatDiscord());
            return;
        }

        Application.OpenURL(RemoteConfig.GetString("sdkDiscordUrl"));
    }

    [MenuItem("Phoenix/SDK/Optimizing", false, 13)]
    public static void ShowAvatarOptimizationTips()
    {
        if (!RemoteConfig.IsInitialized())
        {
            RemoteConfig.Init(() => ShowAvatarOptimizationTips());
            return;
        }

        Application.OpenURL(kAvatarOptimizationTipsURL);
    }

    [MenuItem("Phoenix/SDK/Rigging", false, 13)]
    public static void ShowAvatarRigRequirements()
    {
        if (!RemoteConfig.IsInitialized())
        {
            RemoteConfig.Init(() => ShowAvatarRigRequirements());
            return;
        }

        Application.OpenURL(kAvatarRigRequirementsURL);
    }
}
