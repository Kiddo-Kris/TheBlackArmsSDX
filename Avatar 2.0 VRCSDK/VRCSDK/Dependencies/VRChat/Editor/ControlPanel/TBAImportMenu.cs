using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using VRC.Core;

public partial class VRCSdkControlPanel : EditorWindow
{
    private static GUIStyle ImportImage;
    private static GUIStyle EditorTools;
    private static GUIStyle ADDImage;
    private static GUIStyle PluginsImage;
    private static GUIStyle PrefabsImage;
    private static GUIStyle ShaderImage;
    private static GUIStyle AvatarsImage;
    private static GUIStyle OthersImages;

    Vector2 importsScroll;
    void ShowImports()
    {
        //sdk images
        ImportImage = new GUIStyle
        {
            normal =
            {
             background = Resources.Load("ImportsImage") as Texture2D,
            },
            fixedHeight = 100
        };
        EditorTools = new GUIStyle
        {
            normal =
            {
             background = Resources.Load("EditorTools") as Texture2D,
            },
            fixedHeight = 100
        };
        PluginsImage = new GUIStyle
        {
            normal =
            {
             background = Resources.Load("PluginsImage") as Texture2D,
            },
            fixedHeight = 100
        };
        PrefabsImage = new GUIStyle
        {
            normal =
            {
             background = Resources.Load("PrefabsImage") as Texture2D,
            },
            fixedHeight = 100
        };
        ShaderImage = new GUIStyle
        {
            normal =
            {
             background = Resources.Load("ShaderImage") as Texture2D,
            },
            fixedHeight = 100
        };
        AvatarsImage = new GUIStyle
        {
            normal =
            {
             background = Resources.Load("AvatarsImage") as Texture2D,
            },
            fixedHeight = 100
        };
        OthersImages = new GUIStyle
        {
            normal =
            {
             background = Resources.Load("OthersImages") as Texture2D,
            },
            fixedHeight = 100
        };
        ADDImage = new GUIStyle
        {
            normal =
            {
             background = Resources.Load("ADDImage") as Texture2D,
            },
            fixedHeight = 100
        };
        GUI.backgroundColor = Color.white;
        GUILayout.Box("", ImportImage);
        GUI.backgroundColor = new Color(
UnityEditor.EditorPrefs.GetFloat("SDKColor_R"),
UnityEditor.EditorPrefs.GetFloat("SDKColor_G"),
UnityEditor.EditorPrefs.GetFloat("SDKColor_B"),
UnityEditor.EditorPrefs.GetFloat("SDKColor_A")
);
        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("The Black Arms Website"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/");
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("Latest SDX Release Page"))
        {
            Application.OpenURL("https://www.github.com/TheBlackArms/TheBlackArmsSDX/releases/latest");
        }
        if (GUILayout.Button("SDX Support Server"))
        {
            Application.OpenURL("https://discord.gg/A9dca3N");
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        EditorGUILayout.LabelField("Click the asset to download EXTERNALLY", EditorStyles.boldLabel);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        EditorGUILayout.LabelField("This requires importing from downloads", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        EditorGUILayout.EndHorizontal();

        importsScroll = EditorGUILayout.BeginScrollView(  importsScroll, GUILayout.Width(SdkWindowWidth) );

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        EditorGUILayout.LabelField("VRC Layers", EditorStyles.boldLabel);
		EditorGUILayout.LabelField("               (Adds VRC's collision layers)");
		if (GUILayout.Button("Add", GUILayout.Width(45)))
                {
                    UpdateLayers.SetupEditorLayers();
                }
        EditorGUILayout.EndHorizontal();

        GUI.backgroundColor = Color.white;
        GUILayout.Box("", ADDImage);
        GUI.backgroundColor = new Color(
UnityEditor.EditorPrefs.GetFloat("SDKColor_R"),
UnityEditor.EditorPrefs.GetFloat("SDKColor_G"),
UnityEditor.EditorPrefs.GetFloat("SDKColor_B"),
UnityEditor.EditorPrefs.GetFloat("SDKColor_A")
);
        
        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("My Hero Academia Pack"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Addons/my_hero_academia_Pack.unitypackage");
        }
        if (GUILayout.Button("Old Stuff"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Addons/OldStuff.unitypackage");
        }
        if (GUILayout.Button("Basic Dark Theme"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Addons/Basic.Dark.TBA.SDX.Theme.Pack.unitypackage");
        }
        if (GUILayout.Button("SOA Theme"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Addons/Sons.of.Anarchy.TBA.SDX.Theme.Pack.unitypackage");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("Trash Gang Theme"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Addons/Trash.Gang.TBA.SDX.Theme.Pack.unitypackage");
        }
        if (GUILayout.Button("Classic Theme"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Addons/The.Black.Arms.SDX.Classic.Theme.Pack.unitypackage");
        }
        if (GUILayout.Button("Silence Theme"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Addons/Silence.TBA.SDX.Theme.Pack.unitypackage");
        }
        if (GUILayout.Button("Ministry Theme"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Addons/Ministry.TBA.SDX.Theme.Pack.unitypackage");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("Citizen Hack Theme"))
        {
            Application.OpenURL("https://github.com/TheBlackArms/TheBlackArmsSDX/releases/download/1.10.5/Citizen.Hack.TBA.SDX.Theme.Pack.unitypackage");
        }
        EditorGUILayout.EndHorizontal();

        GUI.backgroundColor = Color.white;
        GUILayout.Box("", EditorTools);
        GUI.backgroundColor = new Color(
UnityEditor.EditorPrefs.GetFloat("SDKColor_R"),
UnityEditor.EditorPrefs.GetFloat("SDKColor_G"),
UnityEditor.EditorPrefs.GetFloat("SDKColor_B"),
UnityEditor.EditorPrefs.GetFloat("SDKColor_A")
);

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("Unity Dark Mode"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/EditorTools/UnityDarkSkin.App.zip");
        }
        if (GUILayout.Button("Sentinel Importer"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/EditorTools/SentinelsImporter.unitypackage");
        }
        if (GUILayout.Button("BitAnimator"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/EditorTools/BitAnimator.unitypackage");
        }
        if (GUILayout.Button("ReroEditorScripts"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/EditorTools/ReroEditorScripts.unitypackage");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("QHierarchy"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/EditorTools/QHierarchy.unitypackage");
        }
        if (GUILayout.Button("PlayModeSaver"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/EditorTools/PlayModeSaver.unitypackage");
        }
        if (GUILayout.Button("MuscleAnimator"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/EditorTools/MuscleAnimator.unitypackage");
        }
        if (GUILayout.Button("Pumkins Avatar Tools v0.8.1b"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/EditorTools/PumkinsAvatarTools_v0.8.1b.unitypackage");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("Unity FBXExporter"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/EditorTools/Unity_FBXExporter.unitypackage");
        }
        if (GUILayout.Button("VRC Avatar Editor"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/EditorTools/VRCAvatarEditor_beta_v0.3.0.1.unitypackage");
        }
        if (GUILayout.Button("VRChat Developer Tool"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/EditorTools/VRCDeveloperTool_20200621.unitypackage");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("VRChat Avatar 3.0 Tools"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/EditorTools/VRCAvatars3Tools_20200809.unitypackage");
        }
        if (GUILayout.Button("Mesh Delete With Texture"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/EditorTools/MeshDeleterWithTexture_v0.6.1b.zip");
        }
        if (GUILayout.Button("Model Bone Deleter"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/EditorTools/ModelBoneDeleter_v1.1.3.zip");
        }
        EditorGUILayout.EndHorizontal();
        GUI.backgroundColor = Color.white;
        GUILayout.Box("", PluginsImage);
        GUI.backgroundColor = new Color(
UnityEditor.EditorPrefs.GetFloat("SDKColor_R"),
UnityEditor.EditorPrefs.GetFloat("SDKColor_G"),
UnityEditor.EditorPrefs.GetFloat("SDKColor_B"),
UnityEditor.EditorPrefs.GetFloat("SDKColor_A")
);

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("DynamicBones 1.2.1"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Plugins/DynamicBone_1.2.1.unitypackage");
        }
        if (GUILayout.Button("Final IK v1.9"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Plugins/Final_IK_v1.9.unitypackage");
        }
        if (GUILayout.Button("Post Processing Stuff"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Plugins/Post_ProcessingStack_v2.unitypackage");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("Inventory System"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Plugins/VRChat-InventorySystem-0.1.0c.zip");
        }
        if (GUILayout.Button("Button Inventory"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Plugins/button_inventory.unitypackage");
        }
        EditorGUILayout.EndHorizontal();

        GUI.backgroundColor = Color.white;
        GUILayout.Box("", PrefabsImage);
        GUI.backgroundColor = new Color(
UnityEditor.EditorPrefs.GetFloat("SDKColor_R"),
UnityEditor.EditorPrefs.GetFloat("SDKColor_G"),
UnityEditor.EditorPrefs.GetFloat("SDKColor_B"),
UnityEditor.EditorPrefs.GetFloat("SDKColor_A")
);

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("LC Particle Overenderer"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Prefabs/LC_S_OVERENDER.unitypackage");
        }
        if (GUILayout.Button("LC Particle"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Prefabs/LCPARTICLE.unitypackage");
        }
        if (GUILayout.Button("Spring Joint"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Prefabs/4112_Spring_joint_thing.unitypackage");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("NanoSDK Particle Shader Sphere"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Prefabs/nanoSDK_ParticleShaderSpherePrefab.unitypackage");
        }
        if (GUILayout.Button("NanoSDK World Audio"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Prefabs/nanoSDK_WorldAudioPrefab.unitypackage");
        }
        if (GUILayout.Button("Armband"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Prefabs/Armband_G_V1.2.unitypackage");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("SDK_PREFABS"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Prefabs/SDK_PREFABS.unitypackage");
        }
        if (GUILayout.Button("WorldFixedIK"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Prefabs/WorldFixedIK.unitypackage");
        }
        if (GUILayout.Button("Inventory System Scripts"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Prefabs/InventorySysPrefabEdited.unitypackage");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("Dynamic Bones Prefabs"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Prefabs/DBP.unitypackage");
        }
        if (GUILayout.Button("Distraction"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Prefabs/7488%20VRChat%20Screenspace%20Prefab%20-%20Henry%20Distracted%20You.unitypackage");
        }
        EditorGUILayout.EndHorizontal();

        GUI.backgroundColor = Color.white;
        GUILayout.Box("", ShaderImage);
        GUI.backgroundColor = new Color(
UnityEditor.EditorPrefs.GetFloat("SDKColor_R"),
UnityEditor.EditorPrefs.GetFloat("SDKColor_G"),
UnityEditor.EditorPrefs.GetFloat("SDKColor_B"),
UnityEditor.EditorPrefs.GetFloat("SDKColor_A")
);

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("PhoenixAceVFX Shader Pack !Very Old!"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Shaders/PhoenixAceVFX_Shader_Pack.unitypackage");
        }
        if (GUILayout.Button("ReroStandard"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Shaders/ReroStandard.unitypackage");
        }
        if (GUILayout.Button("RealToon"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Shaders/RealToon_5.2.1.unitypackage");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("3D Parallax Eye"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Shaders/3D_Parallax_eye_Shader_Fixed.unitypackage");
        }
        if (GUILayout.Button("Doppelganger MetallicFX 5.11"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Shaders/Doppelganger_Distortion_wave_3D.unitypackage");
        }
        if (GUILayout.Button("Poiyomi Toon Latest"))
        {
            Application.OpenURL("https://github.com/poiyomi/PoiyomiToonShader/releases/latest");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("Mochie's Uber Shader 1.4"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Shaders/Mochies.Uber.Shader.v1.4.unitypackage");
        }
        if (GUILayout.Button("Mochie's Nameplate 1.1"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Shaders/Mochies_Nameplates_v1.1.unitypackage");
        }
        if (GUILayout.Button("Mochie's Screen FX"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Shaders/Mochies_Screen_FX_v1.5.unitypackage");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("YourXelf Overlay"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Shaders/YourXelf_Overlay.unitypackage");
        }
        if (GUILayout.Button("ZalgoBLYAT"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Shaders/ZalgoBLYAT.unitypackage");
        }
        if (GUILayout.Button("Start Sphere V5"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Shaders/StartSphereV5.shader");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("Another Shader Pack"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Shaders/All%20Shaders.zip");
        }
        if (GUILayout.Button("Riot Fur"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Shaders/Riots_Furshader.rar");
        }
        if (GUILayout.Button("Corpse Laser"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Shaders/Corpse_Lazer_Shader_v1.5.unitypackage");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("Doppelganger Distortion wave 3D"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Shaders/Doppelganger_Distortion_wave_3D.unitypackage");
        }
        if (GUILayout.Button("Equation 90HZ"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Shaders/7481%2090Hz%20Equation%20Shader%20v11.unitypackage");
        }
        if (GUILayout.Button("Dissolve Glasses 90HZ"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Shaders/7385%2090Hz%20Dissolve%20Glasses%20shader%20v14.unitypackage");
        }
        EditorGUILayout.EndHorizontal();

        GUI.backgroundColor = Color.white;
        GUILayout.Box("", AvatarsImage);
        GUI.backgroundColor = new Color(
UnityEditor.EditorPrefs.GetFloat("SDKColor_R"),
UnityEditor.EditorPrefs.GetFloat("SDKColor_G"),
UnityEditor.EditorPrefs.GetFloat("SDKColor_B"),
UnityEditor.EditorPrefs.GetFloat("SDKColor_A")
);

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("Gecko"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Avatars/Gecko.unitypackage");
        }
        if (GUILayout.Button("YBot Chain"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Avatars/YBot_Chain.unitypackage");
        }
        if (GUILayout.Button("Loading Avatar"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Avatars/Loading_Avatar_Update.unitypackage");
        }
        if (GUILayout.Button("Lewd Avatars"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Avatars/lewd_avatars_by_oddest.rar");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("Zed bot"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Avatars/Zed_bot.FBX");
        }
        if (GUILayout.Button("X-Bot Fixed"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Avatars/X_Bot.fbx");
        }
        if (GUILayout.Button("VBot V2"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Avatars/VBOT_V2_base.fbx");
        }
        if (GUILayout.Button("Corpse loading avatar"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Avatars/Corpse_Loading_Avatar_V7.unitypackage");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("YBot With Shapekeys"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Avatars/YBot_Visemes_-_Eyetracking_-_Blinking.unitypackage");
        }
        if (GUILayout.Button("YBot"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Avatars/YBot.unitypackage");
        }
        if (GUILayout.Button("XBot"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Avatars/X_Bot.fbx");
        }
        if (GUILayout.Button("Nikei"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Avatars/Nikei.unitypackage");
        }
        if (GUILayout.Button("Kyle Base"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Avatars/Kyle_Base.unitypackage");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("YBot Base"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Avatars/YBotBase.fbx");
        }
        if (GUILayout.Button("VRPill"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Avatars/VRPill.unitypackage");
        }
        if (GUILayout.Button("[REDACTED] Bot"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Avatars/Nigger_Bot.unitypackage");
        }
        if (GUILayout.Button("V-bot V3.0"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Avatars/7092_Corpse_V-Bot_30_Public.unitypackage");
        }
        if (GUILayout.Button("Asuka Langley"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Avatars/6608%20Asuka%20Langley.unitypackage");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("Clown Girl"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Avatars/7430%20Clown%20Girl%20Honoka.unitypackage");
        }
        if (GUILayout.Button("Warlock Miku & Luka"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Avatars/7402%20Warlock%20-%20miku%20and%20luka.unitypackage");
        }
        if (GUILayout.Button("Kemono Fox"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Avatars/7377%20Kemono%20Fox.unitypackage");
        }
        if (GUILayout.Button("TDA Miku Pack"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Avatars/7327%20TDA%20Miku%20Pack%20Part%202%20Converted%20MMD%20Models.unitypackage");
        }
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("Matt"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Avatars/7501%20Matt%20from%20wii%20sports%20rigged.unitypackage");
        }
        if (GUILayout.Button("Kiryu"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Avatars/7440%20Kiryu%20From%20Yakuza%200%20Happy%20Gesture%20Baka%20Mitai%20Singing%20Gesture.unitypackage");
        }
        if (GUILayout.Button("Basic EBoy"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Avatars/7407%20Basic%20EBoy%20%20PsychoUpdates.unitypackage");
        }
        if (GUILayout.Button("Hylotl Starbound"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Avatars/7363%20Hylotl%20Starbound%20-%20Facerig.unitypackage");
        }
        EditorGUILayout.EndHorizontal();

        GUI.backgroundColor = Color.white;
        GUILayout.Box("", OthersImages);
        GUI.backgroundColor = new Color(
UnityEditor.EditorPrefs.GetFloat("SDKColor_R"),
UnityEditor.EditorPrefs.GetFloat("SDKColor_G"),
UnityEditor.EditorPrefs.GetFloat("SDKColor_B"),
UnityEditor.EditorPrefs.GetFloat("SDKColor_A")
);

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("Roblox Mad"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Extras/Roblox_Mad.unitypackage");
        }
        if (GUILayout.Button("Thicc Putin"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Extras/PUTIN.unitypackage");
        }
        if (GUILayout.Button("Aftermath"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Extras/7487%20Aftermath%20VIDEO%20PREFAB.unitypackage");
        }
        if (GUILayout.Button("Anime"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Extras/7519%20Anime%20Video%20Prefab.unitypackage");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("Distraction Dance"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Extras/7489%20Henry%20Stickmin%20Distraction%20Animation.unitypackage");
        }
        if (GUILayout.Button("Car Shearer Dance"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Extras/7470%20Car%20shearer%20Dance%20Animation.unitypackage");
        }
        if (GUILayout.Button("Goopie Dance"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Extras/7479%20Goopies%20Dance%20Animation.unitypackage");
        }
        if (GUILayout.Button("Stick Bugg"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/all-sdk/assets/Extras/7447%20Stick%20Bugg.unitypackage");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("Animation Pack"))
        {
            Application.OpenURL("https://vrcmods.com/item/1029");
        }
        if (GUILayout.Button("Stick Boned"))
        {
            Application.OpenURL("https://vrcmods.com/item/7462");
        }
        if (GUILayout.Button("RSL Anim Pack"))
        {
            Application.OpenURL("https://vrcmods.com/item/7405");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("Persona 4 Specialist"))
        {
            Application.OpenURL("https://vrcmods.com/item/7445");
        }
        if (GUILayout.Button("Galaxy Skybox"))
        {
            Application.OpenURL("https://vrcmods.com/item/7381");
        }
        if (GUILayout.Button("CSGO Knives"))
        {
            Application.OpenURL("https://vrcmods.com/item/7279");
        }
        if (GUILayout.Button("ThunderGun"))
        {
            Application.OpenURL("https://vrcmods.com/item/7252");
        }
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Reaching The Stars"))
        {
            Application.OpenURL("https://www.mediafire.com/file/co2tq75vj5vc5bl/Reaching_The_Stars_Prefab.unitypackage/file");
        }
        if (GUILayout.Button("Mortals"))
        {
            Application.OpenURL("https://vrcmods.com/item/7852");
        }
        if (GUILayout.Button("Hijacked"))
        {
            Application.OpenURL("https://vrcmods.com/item/7664");
        }
        if (GUILayout.Button("Senko HBFS"))
        {
            Application.OpenURL("http://www.mediafire.com/file/6rwa87ioy0siywp/Senko+Anim+Video+Prefab.unitypackage/file");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndScrollView();
		GUILayout.FlexibleSpace();

            GUILayout.BeginVertical(boxGuiStyle, GUILayout.Height(20));
            GUILayout.FlexibleSpace();
			EditorGUILayout.LabelField("Import Menu Made By TheGamingBram", EditorStyles.boldLabel);

            GUILayout.EndVertical();
    }
}
