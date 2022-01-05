using UnityEngine;
using UnityEditor;

namespace theblackarmsSDX
{
    [InitializeOnLoad]
    public class theblackarmsSDX_Info : EditorWindow
    {
        private const string Url = "https://github.com/TheBlackArms/TheBlackArmsSDX/";
        private const string Url1 = "https://trigon.systems/";
        private const string Link = "all-sdk/discord/";
        private const string Link1 = "home/";
        static theblackarmsSDX_Info()
        {
            EditorApplication.update -= DoSplashScreen;
            EditorApplication.update += DoSplashScreen;
        }

        private static void DoSplashScreen()
        {
            EditorApplication.update -= DoSplashScreen;
            if (!EditorPrefs.HasKey("theblackarmsSDX_ShowInfoPanel"))
            {
                EditorPrefs.SetBool("theblackarmsSDX_ShowInfoPanel", true);
            }
            if (EditorPrefs.GetBool("theblackarmsSDX_ShowInfoPanel"))
                OpenSplashScreen();
        }

        private static Vector2 changeLogScroll;
        private static GUIStyle vrcSdkHeader;
        private static GUIStyle theblackarmsSDXBottomHeader;
        private static GUIStyle chill_zoneHeaderLearnMoreButton;
        private static GUIStyle chill_zoneBottomHeaderLearnMoreButton;
        [MenuItem("theblackarmsSDX/Info", false, 500)]
        public static void OpenSplashScreen()
        {
            GetWindow<theblackarmsSDX_Info>(true);
        }
        
        public static void Open()
        {
            OpenSplashScreen();
        }

        public void OnEnable()
        {
            titleContent = new GUIContent("theblackarmsSDX Info");
            
            minSize = new Vector2(400, 700);;
            theblackarmsSDXBottomHeader = new GUIStyle();
            vrcSdkHeader = new GUIStyle
            {
                normal =
                    {
                       background = Resources.Load("theblackarmsSDXHeader") as Texture2D,
                       textColor = Color.white
                    },
                fixedHeight = 200
            };
        }

        public void OnGUI()
        {
            GUILayout.Box("", vrcSdkHeader);
            chill_zoneHeaderLearnMoreButton = EditorStyles.miniButton;
            chill_zoneHeaderLearnMoreButton.normal.textColor = Color.black;
            chill_zoneHeaderLearnMoreButton.fontSize = 12;
            chill_zoneHeaderLearnMoreButton.border = new RectOffset(10, 10, 10, 10);
            Texture2D texture = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Texture2D>("UI/Skin/UISprite.psd");
            chill_zoneHeaderLearnMoreButton.normal.background = texture;
            chill_zoneHeaderLearnMoreButton.active.background = texture;
            GUILayout.Space(4);
            GUI.backgroundColor = new Color(
            UnityEditor.EditorPrefs.GetFloat("SDKColor_R"),
            UnityEditor.EditorPrefs.GetFloat("SDKColor_G"),
            UnityEditor.EditorPrefs.GetFloat("SDKColor_B"),
            UnityEditor.EditorPrefs.GetFloat("SDKColor_A")
            );
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("importer made by zombie2312"))
            {
                Application.OpenURL(Url);
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("theblackarmsSDX Discord"))
            {
                Application.OpenURL(Url1 + Link);
            }
            if (GUILayout.Button("theblackarmsSDX Website"))
            {
                Application.OpenURL(Url1);
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(4);
            //Update assets
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("login"))
            {
                Application.OpenURL(Url1 + Link1);
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(4);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("SDK Ver. 7.9.6"))
            {
                Application.OpenURL("https://trigon.systems/changelog/");
            }
            GUILayout.EndHorizontal();
            GUILayout.Label("theblackarmsSDX Version 7.9.6");
            GUILayout.Space(2);
            GUILayout.Label("Instructions for built in features and changelog");
            changeLogScroll = GUILayout.BeginScrollView(changeLogScroll, GUILayout.Width(390));

            GUILayout.Label(
@"
== V7.9.6 ==

Overall:
- Updated to newest theblackarms-devkit
- Removed useless Stuff
------------------------------------------------------------
♬♩♪♫ Added new features ♫♪♩♬
☼ hide_debug_logs_in_Editor
☼ custom_color_devkit 'theblackarms-devkit'
☼ add Recovery tools
------------------------------------------------------------
∞∞∞∞∞∞∞∞∞Fixed bugs∞∞∞∞∞∞∞∞∞
☼ Fixed: 'chillzone-devkit'
☼ unity_editor_crashing_on_startup
- Fixed an error for FS system not being able to find tbaversion.txt
- Fixed bug discord RPC erroring out
------------------------------------------------------------
∞∞∞∞∞∞∞∞∞Recovery tools∞∞∞∞∞∞∞∞∞
- ChangePropertiesOfObject
- CopyBones
- CopyComponents
- Duplicator
- FindMaterial
- FindWrongScripts
- avatar\Scene Loader
- Remove_Missing_Scripts
------------------------------------------------------------
∞∞∞∞∞∞∞∞∞Contributors to theblackarms-devkit∞∞∞∞∞∞∞∞∞
- Contributed by:[zombie2312]
===============================================================

∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX/DevKit 2.4
Yup its 2.4 now!
SDK2 is now Deprecated, 
You can still use it but support wont last.
New DevKit now supports all official VRCSDK3 builds!
Tested Avatar3 and UDON builds will 
be released regularly with DevKit Updates
DevKit is updated via online, 
It should remain up to date as long as your online
ChilloutVR CCK Supported

I will still push updates to the github for anyone who is 
unsure if they're updating correctly
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
New Importer!
update base SDX.
fix ui breaking on upload.
added new importer/updater for SDX.
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
Update base SDX.
Fix ui breaking on upload.
Added new importer/updater for SDX.
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX 2.3.1
Removed copyright fraud issue (Bunny had stolen the scripts, 
I met the real maker)
The creator also wishes to remain anonymous with their scripts
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms Avatar 3.0 SDX 2.3.1
Removed copyright fraud issue
(Bunny had stolen the scripts, I met the real maker)
The creator also wishes to remain anonymous with their scripts
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX 2.3
Replaced Links on control panel to all go to website now
New discord as the old got just got false banned again
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms Avatar 3.0 SDX 2.3
Replaced Links on control panel to all go to website now
New discord as the old got just got false banned again
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX 2.2.1
Updated Links for Import Panel (webserver url change)
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms Avatar 3.0 SDX 2.2.1
Updated Links for Import Panel (webserver url change)
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX 2.2
Migrated EVERYTHING to a dedicated host!
-Huge thanks to Zombie2312 for the hosting!
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms Avatar 3.0 SDX 2.2
Migrated EVERYTHING to a dedicated host!
-Huge thanks to Zombie2312 for the hosting!
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX 2.1.1
Added a few extra assets
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms Avatar 3.0 SDX 2.1.1
Added a few extra assets
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX 2.1
Added several utilities
-AddVRCAvatarPedestals
-ChangePropertiesOfProject
-CopyBones
-CopyComponents
-Duplicator
-FindMaterial
-FindWrongScripts
-GetPropertiesOfObject
-LoadBundle (Unreal Loader)
-MeshGenerator
-MeshToAsset
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms Avatar 3.0 SDX 2.1
Added several utilities
-AddVRCAvatarPedestals
-ChangePropertiesOfProject
-CopyBones
-CopyComponents
-Duplicator
-FindMaterial
-FindWrongScripts
-GetPropertiesOfObject
-LoadBundle (Unreal Loader)
-MeshGenerator
-MeshToAsset
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX 2.0.3
TBA got banned again so I updated links
SDX Support server is now SDX community server
New Main Servers (Invite via TBA Discord & Lonely Souls Buttons)
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms Avatar 3.0 SDX 2.0.3
TBA got banned again so I updated links
SDX Support server is now SDX community server
New Main Servers (Invite via TBA Discord & Lonely Souls Buttons)
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX 2.0.2
Integrated Sentinels Importer (No Errors)
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms Avatar 3.0 SDX 2.0.2
Integrated Sentinels Importer (No Errors)
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX 2.0.1
Improved some performance issues
Invisible control pane buttons should be fixed
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms Avatar 3.0 SDX 2.0.1
Improved some performance issues
Invisible control pane buttons should be fixed
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX 2.0
Thats right Version 2
We have updated many things in this update
Several scripts have been modified
Antiyoink is now in both revisions
Upload methods changed again (sorry)
Lots of bug fixes (too long of a list)
File structure changes completed (that wont happen for awhile now)
Now converting assets to import system (like sentinel importer)
Overhaul of some DLLs (many edits to those)

Added Night Raid Theme Pack (By Nep Nep)
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms Avatar 3.0 SDX 2.0
Thats right Version 2
We have updated many things in this update
Several scripts have been modified
Antiyoink is now in both revisions
Upload methods changed again (sorry)
Lots of bug fixes (too long of a list)
File structure changes completed (that wont happen for awhile now)
Now converting assets to import system (like sentinel importer)
Overhaul of some DLLs (many edits to those)
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX 1.12.5
File Structure Changes
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX 1.12.4
Updated file appends

DELETE OLD BUILD WHEN UPDATING OR ERRORS HAPPEN
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms Avatar 3.0 SDX 1.2.1
File Structure Changes
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms Avatar 3.0 Release 1.2
Upload Method Edited
Updated File Appends
Added AV 3.0 Emulator

DELETE OLD BUILD WHEN UPDATING OR ERRORS HAPPEN
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX 1.12.3
New upload method implemented
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX 1.12.2
Added more Editor Tools to Assets
Fixed bug with Control Panel Buttons
-They are no longer invisible on first import!
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms Avatar 3.0 SDX 1.1.2
Added more Editor Tools to Assets
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX 1.12.1
Moved SENTINEL IMPORTER from pre-imported to external
-This is in the assets panel until further notice
-Be sure to remove it when uploading or errors occur
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms Avatar 3.0 SDX 1.1.1
Moved SENTINEL IMPORTER from pre-imported to external
-This is in the assets panel until further notice
-Be sure to remove it when uploading or errors occur
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX 1.12
Added SENTINEL IMPORTER (The Free One)
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms Avatar 3.0 SDX 1.1
Added SENTINEL IMPORTER (The Free One)
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX 1.11.1
Fixed a compiling error (WHOOPS)
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black arms SDX 1.11
Rewritten a few things
Preparing for FULL REWRITE of the SDX
Added a few new functions
Adding AntiYoink to this version soon
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX Avatar 3.0 Release 1.0.7
AntiYoink (Thanks to Controser!)
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX 1.10.10
Slight expansion to Imports Pane
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms Avatar 3.0 SDX 1.0.6
Slight expansion to Imports Pane
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX 1.10.9
Fixed position of color selector in Control Panel Settings
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX 1.10.8
Added Support Server Links
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX Avatar 3.0 Release 1.0.5
Added Support Server LInks
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX 1.10.7
Updated Control Panel Layouts
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX Avatar 3.0 Release 1.0.4
Updated Control Panel Layouts
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX 1.10.6
Fixed DEDSEC Invite Links
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX Avatar 3.0 Release 1.0.3
Fixed DEDSEC Invite Links
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX 1.10.5
Repaired Discord Links
Added Guilded.gg Links
Sons of Anarchy TBA SDX Theme Pack is now available
Citizen Hack TBA SDX Theme Pack now available
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX 1.10.4
Updated Discord Links for The Black Arms
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX Avatar 3.0 Release 1.0.2
Repaired Discord Links
Added Guilded.gg Links
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX Avatar 3.0 Release 1.0.1
The Black Arms Avatar 3.0 SDX 1.0.1
Updated Discord Links
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX Avatar 3.0 Release 1.0
The Black Arms Avatar 3.0 SDX 1.0
This is the beginning of what is to come
Almost everything came over from main branch
SDK Coloring didn't but I am working on it.
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX 1.10.3
Fixed Overlapping Text on Upload Panels (whoops)
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX 1.10.2
Fixed RGB Text on Upload Panels (I cant believe it broke)
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX 1.10.1
Forgot to add the new Assets for 1.10 (lol)
Removed dead links (I think I got them all)
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX 1.10
Updated version scheme!
Added a new Loadbundle as a COMPONENT script
-Add it to an object to use it
Modified Changelog Pane
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX 1.9.2.3-1R
Updated a few minor things
Now under CLA instead of licensing

This new CLA applies to ALL REVISIONS of The Black Arms SDX
This includes ALL PAST and FUTURE REVISIONS
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX 1.9.2.3R
Now home in 2 Discords
DEDSEC and The Black Arms: UNLEASHED
Fixed a DLL issue
Fixed bug with ESR crashing unity
LICENSE Information added to Github
Thanks to Frostbite for allowing TBA SDX to be home
-In DEDSEC
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX 1.9.2.2-5R
Updated DiscordRPC
My discord just got false banned unfairly
We are merging with DEDSEC
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX 1.9.2.2-4R
OG Discord link is back!!!
https://discord.gg/assetsuntamed
Huge thanks to W0lfy for the 12 boosts!
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
1.9.2.2-3R
Just updated this one to reflect the new discord as the old one was shadow banned
https://discord.gg/mYn3jSz - New server perm link
Ministry TBA SDX Theme Pack by Blaze
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX 1.9.2.2-2 Release
Cleaned the ASSETS PANEL (For legal reasons)
-Compressed it too so its not as lengthy
-Added theme packs to ADDONS subsection
-All paid assets removed from ASSETS PANEL
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
The Black Arms SDX 1.9.2.2-1 RELEASE
Finalized the LICENSE
Fixed minor underlying bugs
Theme packs are now officially supported by the SDX
Theme packs are in the previous builds, I will not be reposing them with each update.
Added Basic Dark Theme Pack
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
1.9.2.2 RELEASE + Theme Packs
Ive done a bit of work to this one, and now THEME PACKS are coming!
So I have released the CLASSIC Theme Pack which houses the old banners to enable the old feel of the SDX
I have now added the Trash Gang TBA SDX Theme Pack!
Added Silence TBA SDX Theme Pack!
The Black Arms SDX 1.9.2.2 RELEASE
License is now official in full effect
Theme Packs are on the way!
-This will allow me to make custom looks to the SDX
-All without having to remake the SDX repeatedly!
All functions are tested and ready for RELEASE
Splash Screen modified
-Now points out that there is a license
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
== V1.9.2.2 ==
This Revision is not the latest, github seems to of bugged that
This update is optional
The Black Arms SDX 1.9.2.2 BETA
Updated Banners (planning to release theme packages)
Improved DLL optimizations (hard to notice lol)
The SDX made it to GITHUB!
Product is fully OPEN SOURCED
License added (Because I have to)

LICENSE
This product is given AS IS
You are allowed to FORK this repository
You are not allowed to RESELL ANYTHING within this repository
You are not allowed to CLAIM this repository as your own
You are allowed to MODIFY this Product
-As long as the original credit remains in tact
-failure to meet this will result in DMCA Takedown
You are allowed to SHARE this AS IS
If you redistribute your own version credit is required to be given
-Or it will be met with a DMCA Takedown
This project is OPEN SOURCED but is not allowed to be SOLD or used for PROFIT
This repository falls under a Dual LICENSE
-GNU AGPLv3
-CC-BY-NC-SA-4.0
-Don't worry all your work will be 100% credited to you
∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞
== V1.9.2 ==
This includes 1.9.2.1 RELEASE and 1.9.2 BETA
1.9.2.2 ALPHA will be soon

The Black Arms SDX 1.9.2.1 RELEASE
ONLY SUPPORTS UNITY 2018.4.20F1
SDX is found here in top bar
Phoenix/SDK/
Changelog
-This is a small update
Moved ALL menu items from SDX to Phoenix/
-Thanks to TheGamingBram for the DLL work!
-This also includes the TGE functions shared (Now in normal font!)
All 'The Gaming Evolution' funcions are now under Phoenix/TGE/
Fixed a minor crash issue
Added new ASSETS to the ASSETS Panel (Still external downloads)

The Black Arms SDX 1.9.2 BETA
ONLY SUPPORTS UNITY 2018.4.20F1
SDX is found here in top bar
Phoenix/SDK/
Changelog
Improving asset bundling
Updated to newest VRCSDK2 revision (2020.06.16.20.54)
Changed upload method, seeing if it is faster or slower
Think I fixed a bug relating to world uploads processing so slow
Reworked some weird code in hopes to fix crashing issues
This is not a TRUE RELEASE, this is a OPTIONAL BETA
");
            GUILayout.EndScrollView();
            GUILayout.Space(4);

            GUILayout.Box("", theblackarmsSDXBottomHeader);
            chill_zoneBottomHeaderLearnMoreButton = EditorStyles.miniButton;
            chill_zoneBottomHeaderLearnMoreButton.normal.textColor = Color.black;
            chill_zoneBottomHeaderLearnMoreButton.fontSize = 10;
            chill_zoneBottomHeaderLearnMoreButton.border = new RectOffset(10, 10, 10, 10);
            chill_zoneBottomHeaderLearnMoreButton.normal.background = texture;
            chill_zoneBottomHeaderLearnMoreButton.active.background = texture;

            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
            EditorPrefs.SetBool("theblackarmsSDX_ShowInfoPanel", GUILayout.Toggle(EditorPrefs.GetBool("theblackarmsSDX_ShowInfoPanel"), "Show at startup"));
            GUILayout.EndHorizontal();
        }

    }
}