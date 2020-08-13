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
        if (GUILayout.Button("The Black Arms Discord"))
        {
            Application.OpenURL("https://discord.gg/Jbf5AAR");
        }
        if (GUILayout.Button("TGE VRC Asset Server"))
        {
            Application.OpenURL("https://discord.gg/cbDhUZW");
        }
        if (GUILayout.Button("DEDSEC"))
        {
            Application.OpenURL("https://discord.gg/r7RcJCv");
        }
        if (GUILayout.Button("VRChat Discord"))
        {
            Application.OpenURL("http://discord.gg/VRChat");
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
            Application.OpenURL("https://cdn.discordapp.com/attachments/715621075735806004/715622863758950430/my_hero_academia_Pack.unitypackage");
        }
        if (GUILayout.Button("Old Stuff"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715621075735806004/715622940418244709/OldStuff.unitypackage");
        }
        if (GUILayout.Button("Basic Dark Theme"))
        {
            Application.OpenURL("https://github.com/TheBlackArms/TheBlackArmsSDX/releases/download/1.9.2.2-1R/Basic.Dark.TBA.SDX.Theme.Pack.unitypackage");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("Trash Gang Theme"))
        {
            Application.OpenURL("https://github.com/TheBlackArms/TheBlackArmsSDX/releases/download/1.9.2.2R/Trash.Gang.TBA.SDX.Theme.Pack.unitypackage");
        }
        if (GUILayout.Button("Classic Theme"))
        {
            Application.OpenURL("https://github.com/TheBlackArms/TheBlackArmsSDX/releases/download/1.9.2.2R/The.Black.Arms.SDX.Classic.Theme.Pack.unitypackage");
        }
        if (GUILayout.Button("Silence Theme"))
        {
            Application.OpenURL("https://github.com/TheBlackArms/TheBlackArmsSDX/releases/download/1.9.2.2R/Silence.TBA.SDX.Theme.Pack.unitypackage");
        }
        if (GUILayout.Button("Ministry Theme"))
        {
            Application.OpenURL("https://github.com/TheBlackArms/TheBlackArmsSDX/releases/download/1.9.2.2-3R/Ministry.TBA.SDX.Theme.Pack.unitypackage");
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
            Application.OpenURL("https://cdn.discordapp.com/attachments/685658143950372926/695284833320173678/UnityDarkSkin.App.zip");
        }
        if (GUILayout.Button("BitAnimator"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309685665955881/715311556329603112/BitAnimator.unitypackage");
        }
        if (GUILayout.Button("ReroEditorScripts"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309685665955881/715312372289634415/ReroEditorScripts.unitypackage");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("QHierarchy"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309685665955881/715312372729905204/QHierarchy.unitypackage");
        }
        if (GUILayout.Button("PlayModeSaver"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309685665955881/715312376374624296/PlayModeSaver.unitypackage");
        }
        if (GUILayout.Button("MuscleAnimator"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309685665955881/715312377804881940/MuscleAnimator.unitypackage");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("Pumkins Avatar Tools v0.8.1b"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309685665955881/715312393458155660/PumkinsAvatarTools_v0.8.1b.unitypackage");
        }
        if (GUILayout.Button("Unity FBXExporter"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309685665955881/715312468531871844/Unity_FBXExporter.unitypackage");
        }
        if (GUILayout.Button("VRC Avatar Editor"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715851292916056065/716732375748444200/VRCAvatarEditor_beta_v0.3.0.1.unitypackage");
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
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309704443723957/715314279917682699/DynamicBone_1.2.1.unitypackage");
        }
        if (GUILayout.Button("Final IK v1.9"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309704443723957/715314749776330833/Final_IK_v1.9.unitypackage");
        }
        if (GUILayout.Button("Post Processing Stuff"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/685659591807467528/714070949951176764/Post_ProcessingStack_v2.unitypackage");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("Inventory System"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/685659591807467528/730906283443748984/VRChat-InventorySystem-0.1.0c.zip");
        }
        if (GUILayout.Button("Button Inventory"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/685659591807467528/720294538966466590/button_inventory.unitypackage");
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
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309731438395424/715316977870307369/LC_S_OVERENDER.unitypackage");
        }
        if (GUILayout.Button("LC Particle"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309731438395424/715316980214792252/LCPARTICLE.unitypackage");
        }
        if (GUILayout.Button("Spring Joint"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/685659510441902119/710416568034525224/4112_Spring_joint_thing.unitypackage");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("NanoSDK Particle Shader Sphere"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309731438395424/715316980722303036/nanoSDK_ParticleShaderSpherePrefab.unitypackage");
        }
        if (GUILayout.Button("NanoSDK World Audio"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309731438395424/715316981590393004/nanoSDK_WorldAudioPrefab.unitypackage");
        }
        if (GUILayout.Button("Armband"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/685659510441902119/730836220581773412/Armband_G_V1.2.unitypackage");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("SDK_PREFABS"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309731438395424/715316982597156945/SDK_PREFABS.unitypackage");
        }
        if (GUILayout.Button("WorldFixedIK"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309731438395424/715316983566172219/WorldFixedIK.unitypackage");
        }
        if (GUILayout.Button("Inventory System Scripts"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309731438395424/715316985852067970/InventorySysPrefabEdited.unitypackage");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("Dynamic Bones Prefabs"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309731438395424/715316984396644479/DBP.unitypackage");
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
            Application.OpenURL("https://cdn.discordapp.com/attachments/711755163647344672/711759641192955934/PhoenixAceVFX_Shader_Pack.unitypackage");
        }
        if (GUILayout.Button("ReroStandard"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309758667554886/715319033662275664/ReroStandard.unitypackage");
        }
        if (GUILayout.Button("RealToon"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309758667554886/715500217553649745/RealToon_5.2.1.unitypackage");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("3D Parallax Eye"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/685659550816141314/730855112247083018/3D_Parallax_eye_Shader_Fixed.unitypackage");
        }
        if (GUILayout.Button("Doppelganger MetallicFX 5.11"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309758667554886/715321638845153380/Doppelganger_MetallicFX_5.11.unitypackage");
        }
        if (GUILayout.Button("PoiyomiToonV5.5.5"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309758667554886/715320210676252733/PoiyomiToonV5.5.5.unitypackage");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("Mochie/s Uber Shader 1.4"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309758667554886/715499843522658334/Mochies.Uber.Shader.v1.4.unitypackage");
        }
        if (GUILayout.Button("Mochie's Nameplate 1.1"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309758667554886/715499857955258438/Mochies_Nameplates_v1.1.unitypackage");
        }
        if (GUILayout.Button("Mochie's Screen FX"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309758667554886/715499924518731946/Mochies_Screen_FX_v1.5.unitypackage");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("YourXelf Overlay"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309758667554886/715319259584004176/YourXelf_Overlay.unitypackage");
        }
        if (GUILayout.Button("ZalgoBLYAT"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309758667554886/715319099395407882/ZalgoBLYAT.unitypackage");
        }
        if (GUILayout.Button("Start Sphere V5"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/685659550816141314/731626459944452096/StartSphereV5.shader");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("Another Shader Pack"))
        {
            Application.OpenURL("https://www.mediafire.com/download_status.php?quickkey=en9zkbs48fkxcld&origin=download");
        }
        if (GUILayout.Button("Riot Fur"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/685659550816141314/728061993675653160/Riots_Furshader.rar");
        }
        if (GUILayout.Button("Corpse Laser"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/685659550816141314/730822299258716170/Corpse_Lazer_Shader_v1.5.unitypackage");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("Doppelganger Distortion wave 3D"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309758667554886/715319035109179452/Doppelganger_Distortion_wave_3D.unitypackage");
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
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309796349444148/715322764449415248/Gecko.unitypackage");
        }
        if (GUILayout.Button("YBot Chain"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309796349444148/715324311132569670/YBot_Chain.unitypackage");
        }
        if (GUILayout.Button("Loading Avatar"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309796349444148/715324328882995230/Loading_Avatar_Update.unitypackage");
        }
        if (GUILayout.Button("Lewd Avatars"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/685659510441902119/685907672188584011/lewd_avatars_by_oddest.rar");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("Zed bot"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309796349444148/715324328735932446/Zed_bot.FBX");
        }
        if (GUILayout.Button("X-Bot Fixed"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309796349444148/715324442762281111/4279_X-Bot_Fixed.unitypackage");
        }
        if (GUILayout.Button("VBot V2"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309796349444148/715324445212016671/VBOT_V2_base.fbx");
        }
        if (GUILayout.Button("Corpse loading avatar"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309796349444148/715325813091991581/Corpse_Loading_Avatar_V7.unitypackage");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("YBot With Shapekeys"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309796349444148/715324463914418176/YBot_Visemes_-_Eyetracking_-_Blinking.unitypackage");
        }
        if (GUILayout.Button("YBot"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309796349444148/715324475331182612/YBot.unitypackage");
        }
        if (GUILayout.Button("XBot"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309796349444148/715324481618575390/X_Bot.fbx");
        }
        if (GUILayout.Button("Nikei"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309796349444148/715325701859049522/Nikei.unitypackage");
        }
        if (GUILayout.Button("Kyle Base"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309796349444148/715324772879302686/Kyle_Base.unitypackage");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("YBot Base"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309796349444148/715324486379110510/YBotBase.fbx");
        }
        if (GUILayout.Button("VRPill"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309796349444148/715324621989085234/VRPill.unitypackage");
        }
        if (GUILayout.Button("[REDACTED] Bot"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309796349444148/715324723281657856/Nigger_Bot.unitypackage");
        }
        if (GUILayout.Button("V-bot V3.0"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/715309796349444148/715325526641999872/7092_Corpse_V-Bot_30_Public.unitypackage");
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
            Application.OpenURL("https://cdn.discordapp.com/attachments/688440858189627490/736265633964228639/Roblox_Mad.unitypackage");
        }
        if (GUILayout.Button("Thicc Putin"))
        {
            Application.OpenURL("https://cdn.discordapp.com/attachments/688440858189627490/736265646773370991/PUTIN.unitypackage");
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
