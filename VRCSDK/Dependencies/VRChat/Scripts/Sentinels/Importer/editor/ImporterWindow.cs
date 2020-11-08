using System.Collections;
using UnityEngine;
using UnityEditor;
using Sentinels;
using System;
using System.IO;



public class SentinelsImporter : EditorWindow
{

    Texture2D headerSectionTexture;
    Texture2D bodySectionTexture;
    Texture2D toolLogo;
    Texture2D info;

    GUIContent infocon;

    Color headerSectionColor = new Color32(35, 35, 35, 255);

    Rect headerSection;
    Rect bodySection;

    public string[] options = { "VRCSDK/Avatar 3.0", "VRCSDK/Avatar 2.0", "VRCSDK/Udon World", "Tools/Dynamic Bones", "Tools/Muscle Animator", "Tools/Pumpkin's Avatar Tools", "Tools/Discord RPC", "Shaders/Poiyomi Toon", "Shaders/XS Toon", "Shaders/Mochi Uber", "Prefabs/Sentinels Community Floater", "Prefabs/Yelby Measuring Tool", "Prefabs/Dynamic Bone Settings", "Prefabs/Holy Gecko", "Prefabs/Starfield Skybox", "Against TOS/Avatar Limit Remover", "Against TOS/Anti Yoink", "Against TOS/Nano Thumbail Selector", "Against TOS/Splash Screen Remover" };
    public int index = 0;
    //18 total

    private static string localDownloadPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Unity/Sentinels/";
    string fileName = "";

    [MenuItem("Sentinels/Importer")]
    static void OpenWindow()
    {
        SentinelsImporter window = (SentinelsImporter)GetWindow(typeof(SentinelsImporter));
        Texture icon = AssetDatabase.LoadAssetAtPath<Texture>("Assets/VRCSDK/Sentinels/Importer/resources/icons/icon.png");
        window.minSize = new Vector2(200, 240);
        window.titleContent = new GUIContent("Importer", icon);
        window.Show();
    }

    void OnEnable()
    {
        InitTextures();
    }

    void InitTextures()
    {
        headerSectionTexture = new Texture2D(1, 1);
        headerSectionTexture.SetPixel(0, 0, headerSectionColor);
        headerSectionTexture.Apply();

        bodySectionTexture = Resources.Load<Texture2D>("icons/blue");
        info = Resources.Load<Texture2D>("icons/info");

        infocon = new GUIContent(info);
    }

    void OnGUI()
    {
        DrawLayouts();
        DrawHeader();
        DrawBody();
    }

    void DrawLayouts()
    {
        headerSection.x = 0;
        headerSection.y = 0;
        headerSection.width = Screen.width;
        headerSection.height = 20;

        bodySection.x = 0;
        bodySection.y = 20;
        bodySection.width = Screen.width;
        bodySection.height = Screen.height - 20;

        GUI.DrawTexture(headerSection, headerSectionTexture);
        GUI.DrawTexture(bodySection, bodySectionTexture);
    }

    void DrawHeader()
    {
        GUILayout.BeginArea(headerSection);

        GUILayout.Label("Made By Controser#8888", EditorStyles.centeredGreyMiniLabel);

        GUILayout.EndArea();
    }

    void DrawBody()
    {
        GUILayout.BeginArea(bodySection);
        GUILayout.BeginArea(new Rect(Screen.width - 26, 2, 24, 24));
        if (GUILayout.Button(infocon, GUILayout.Width(24), GUILayout.Height(24)))
        {
            SentinelsInfo.OpenInfo();
        }
        GUILayout.EndArea();
        GUILayout.BeginArea(new Rect((Screen.width / 2) - 70, 0, 140, 140));
        if (toolLogo == null)
            toolLogo = AssetDatabase.LoadAssetAtPath("Assets/VRCSDK/Sentinels/Importer/resources/icons/logo.png", typeof(Texture2D)) as Texture2D;
        GUILayout.Label(toolLogo);
        GUILayout.EndArea();
        GUILayout.BeginArea(new Rect(10, 140, Screen.width - 20, 80));
        GUILayout.Label("Choose Asset:");
        index = EditorGUILayout.Popup(index, options);
        SelectedAsset();
        ImportButton();
        GUILayout.EndArea();
        GUILayout.EndArea();
    }

    void ImportButton()
    {
        EditorGUILayout.BeginHorizontal();
        if (File.Exists(localDownloadPath + fileName))
        {
            if (GUILayout.Button("Import", GUILayout.Height(40), GUILayout.Width((Screen.width / 2) - 12)))
            {
                Directory.CreateDirectory(localDownloadPath);
                Sentinels_ImportManager.ImportAsset(fileName);
            }
            if (GUILayout.Button("Download", GUILayout.Height(40), GUILayout.Width((Screen.width / 2) - 12)))
            {
                Directory.CreateDirectory(localDownloadPath);
                Sentinels_ImportManager.DownloadAsset(fileName);
            }
            EditorGUILayout.EndHorizontal();
        }
        else
        {
            GUI.enabled = false;
            if (GUILayout.Button("Import", GUILayout.Height(40), GUILayout.Width((Screen.width / 2) - 12)))
            {
                Directory.CreateDirectory(localDownloadPath);
                Sentinels_ImportManager.ImportAsset(fileName);
            }
            GUI.enabled = true;
            if (GUILayout.Button("Download", GUILayout.Height(40), GUILayout.Width((Screen.width / 2) - 12)))
            {
                Directory.CreateDirectory(localDownloadPath);
                Sentinels_ImportManager.DownloadAsset(fileName);
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    void SelectedAsset()
    {
        switch (index)
        {
            case 0:
                //VRCSDK Avatar 3.0
                fileName = "avatar3.unitypackage";
                break;
            case 1:
                //VRCSDK Avatar 2.0
                fileName = "avatar2.unitypackage";
                break;
            case 2:
                //VRCSDK Udon
                fileName = "udon.unitypackage";
                break;
            case 3:
                //Dynamic Bones
                fileName = "dynamicbones.unitypackage";
                break;
            case 4:
                //Muscle Animator
                fileName = "muscleanimator.unitypackage";
                break;
            case 5:
                //Pumpkin's Avatar Tools
                fileName = "pumpkin.unitypackage";
                break;
            case 6:
                //Discord RPC
                fileName = "discordrpc.unitypackage";
                break;
            case 7:
                //Poiyomi Toon
                fileName = "poiyomitoon.unitypackage";
                break;
            case 8:
                //XS Toon
                fileName = "xstoon.unitypackage";
                break;
            case 9:
                //Mochi's Uber
                fileName = "mochi.unitypackage";
                break;
            case 10:
                //Sentinels Community Logo
                fileName = "communityrep.unitypackage";
                break;
            case 11:
                //Yelby's Measuring Tool
                fileName = "yelby.unitypackage";
                break;
            case 12:
                //Dynamic Bone Prefab
                fileName = "dynamicsettings.unitypackage";
                break;
            case 13:
                //The Holy GECKO
                fileName = "gecko.unitypackage";
                break;
            case 14:
                //Starfield Skybox
                fileName = "starskybox.unitypackage";
                break;
            case 15:
                //Avatar Limit Remover
                fileName = "nolimit.unitypackage";
                break;
            case 16:
                //Anti Yoink
                fileName = "antiyoink.unitypackage";
                break;
            case 17:
                //Nano Thumbnail Selector
                fileName = "imagechooser.unitypackage";
                break;
            case 18:
                //Splash Screen Remover
                fileName = "nosplash.unitypackage";
                break;
        }
    }

}

public class SentinelsInfo : EditorWindow
{
    Vector2 Scroll;
    Texture2D footer;
    Color footerColor = new Color32(35, 35, 35, 255);
    public static void OpenInfo()
    {
        SentinelsInfo window = (SentinelsInfo)GetWindow(typeof(SentinelsInfo));
        window.minSize = new Vector2(350, 500);
        window.titleContent = new GUIContent("Importer Info");
        window.Show();
    }

    public void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height - 70));
        Scroll = GUILayout.BeginScrollView(Scroll, GUILayout.Width(Screen.width), GUILayout.Height(Screen.height - 70));
        EditorGUILayout.LabelField(
@"Want more imports? Supporting this tool on patreon unlocks over 20 more! 

How To Use:

There is a dropdown menu full of assets. The first thing to do is select the asset of your choice. Afterwards click 'download' to ensure the file exists on your computer. If the file is downloaded then 'import' will be selectable. Clicking 'import' will then add the package to your project. 'Download' stays available so that if an update is pushed out or the file gets corrupted you can replace it.


Changelog:

Sentinels Importer

• Custom SDK discontinued
    - To keep users from breaking TOS
• Tool now named Sentinels Importer
• Completely built from scratch
• Added popup for selecting asset
• Download button works like before
• Import button now always visible but disabled if the file does not exist
• Info button added to view changelog and customization
• More imports added
• Added categories 'VRCSDK' and 'Against TOS'
• Added custom textures and layouts

Sentinels Avatar SDK

• SDK updated for Avatar 3.0
• Customization settings removed due to conflict

Sentinels SDK v6.1

• Imports made global (can be accessed across projects)
• More imports added
• Future proof added
• Download bar added
• Separate buttons for import and download added
• Download button now replaces old files
• New graphics

Sentinels SDK v6

• Discord RPC added
• More imports added
• Customizations options on Settings tab added
• Future proof removed
• Button color can now be changed
• Compatibility added for Unity Dark Mode
• New graphics

Sentinels SDK v5

• More imports added
• 'Discord Server' button added
• 'Sentinels Website' button added
• About tab on Control Panel added
• Renamed 'Authentication' to 'Login'
• Buttons given color
• Made compatible with Unity 2018 and 2017
• New graphics and layout

Sentinels SDK v4

• Imports tab on Control Panel added
• All imports moved to new tab
• More imports added
• Imports can now be downloaded before imported to save on file size
• Toggle for splash screen added
• Renamed 'Content Manager' to 'My Content'
• New graphics and layout

Sentinels SDK v3

• Control Panel added
• 'How To: Quest Content' button added
• 'Support Creator' button added
• More imports added
• Splash Screen removed
• Future Proof changed to inform of giving rippers full access
• New graphics

Sentinels SDK v2

• Anti Yoink added
• More imports added
• Changed 'Upload Avatar' to 'Upload'
• New graphics and layout

Sentinels SDK

• Auto fill visemes added
• Scripts/event handler added
• Import buttons added to settings panel
• Removed annoying warnings
• Removed avatar limitations
• Renamed 'Build & Publish' to 'Upload Avatar'
• Custom changelog and splash screen",
            EditorStyles.wordWrappedLabel);
        GUILayout.EndScrollView();
        GUILayout.EndArea();
        GUILayout.BeginArea(new Rect(0, Screen.height - 70, Screen.width, 70));
        footer = new Texture2D(1, 1);
        footer.SetPixel(0, 0, footerColor);
        footer.Apply();
        GUILayout.BeginVertical(GUILayout.Height(20));
        GUILayout.FlexibleSpace();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Need help? Want to support this tool? Use the buttons below!", EditorStyles.centeredGreyMiniLabel);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Discord"))
        {
            Application.OpenURL("http://discord.gg/ejKpC6A");
        }
        if (GUILayout.Button("Patreon"))
        {
            Application.OpenURL("https://www.patreon.com/sentinelsimporter");
        }
        EditorGUILayout.EndHorizontal();
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}