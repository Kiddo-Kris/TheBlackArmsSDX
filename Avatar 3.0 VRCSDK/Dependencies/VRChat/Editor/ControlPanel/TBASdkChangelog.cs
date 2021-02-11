using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using VRC.Core;

public partial class VRCSdkControlPanel : EditorWindow
{
    private static GUIStyle ChangeImage;
    Vector2 changesScroll;
	
    void ShowChanges()
    {

        ChangeImage = new GUIStyle
        {
            normal =
            {
             background = Resources.Load("SettingsChanges") as Texture2D,
            },
            fixedHeight = 100
        };
        GUI.backgroundColor = Color.white;
        GUILayout.Box("", ChangeImage);
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
        EditorGUILayout.BeginVertical(boxGuiStyle);
        EditorGUILayout.LabelField("The Black Arms Discord: theblackarms.cf", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("The Black Arms SDX Dev: PhoenixAceVFX", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("The Black Arms SDX Contributors", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Wolfie (Huge help with legal and github!)", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("PoH (taught me how to mod upload panels)", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("TheGamingBram (awesome script work)", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("ODDS (SDX is based in ODDS)", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Controser (AntiYoink[In 3.0] and SENTINEL IMPORTER)", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Bunny (For their Utilities!)", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Zombie2312 (Total VPS Hosting)", EditorStyles.boldLabel);
        EditorGUILayout.EndVertical();

        changesScroll = EditorGUILayout.BeginScrollView(changesScroll, GUILayout.Width(SdkWindowWidth));
		EditorGUILayout.BeginVertical();
		GUILayout.Label(
        @"You are fully free to contribute to the SDX
All edits still remain under my copyright ownership
This is done via CLA

Note: If your going to ask me to make you a SDK/SDX
I will only make a THEME PACK for my SDX as it is
Much easier to do than to make an entire remake
This is completely free of charge as I refuse to make
Any money or profit from my SDX and I will not rebrand
My SDX, this decision is final and wont be changed.

This Branch is ENTIRELY SEPERATED from the main branch
Therefore this will have its own changelog

The Black Arms Avatar 3.0 SDX 2.3
Replaced Links on control panel to all go to website now
New discord as the old got just got false banned again

The Black Arms Avatar 3.0 SDX 2.2.1
Updated Links for Import Panel (webserver url change)

The Black Arms Avatar 3.0 SDX 2.2
Migrated EVERYTHING to a dedicated host!
-Huge thanks to Zombie2312 for the hosting!

The Black Arms Avatar 3.0 SDX 2.1.1
Added a few extra assets

The Black Arms Avatar 3.0 SDX 2.1
Added several utilities (thanks to bunny!)
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

The Black Arms Avatar 3.0 SDX 2.0.3
TBA got banned again so I updated links
SDX Support server is now SDX community server
New Main Servers (Invite via TBA Discord & Lonely Souls Buttons)

The Black Arms Avatar 3.0 SDX 2.0.2
Integrated Sentinels Importer (No Errors)

The Black Arms Avatar 3.0 SDX 2.0.1
Improved some performance issues
Invisible control pane buttons should be fixed

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

The Black Arms Avatar 3.0 SDX 1.2.1 Release
File Structure Changes

The Black Arms Avatar 3.0 SDX 1.2 Release
Upload method edited
Updated File Appends
Added AV 3.0 Emulator

The Black Arms Avatar 3.0 SDX 1.2 Beta
Testing AV3 Emulator script
Testing Upload method edit

The Black Arms Avatar 3.0 SDX 1.1.2
Added more Editor Tools to Assets

The Black Arms Avatar 3.0 SDX 1.1.1
Moved SENTINEL IMPORTER from pre-imported to external
-This is in the assets panel until further notice
-Be sure to remove it when uploading or errors occur

The Black Arms Avatar 3.0 SDX 1.1
Added SENTINEL IMPORTER (The Free One)

The Black Arms Avatar 3.0 SDX 1.0.7
AntiYoink! (Huge thanks to controser!)

The Black Arms Avatar 3.0 SDX 1.0.6
Slight expansion to Imports Pane

The Black Arms Avatar 3.0 SDX 1.0.5
Added Support Server Links

The Black Arms Avatar 3.0 SDX 1.0.4
Updated Control Panel Layouts

The Black Arms Avatar 3.0 SDX 1.0.3
Fixed DEDSEC Invite Links

The Black Arms Avatar 3.0 SDX 1.0.2
Repaired Discord Links
Added Guilded.gg Links

The Black Arms Avatar 3.0 SDX 1.0.1
Updated Discord Links

The Black Arms Avatar 3.0 SDX 1.0
This is the beginning of what is to come
Almost everything came over from main branch
SDK Coloring didn't but I am working on it.
");
		EditorGUILayout.EndVertical();
		EditorGUILayout.EndScrollView();
    }
}
