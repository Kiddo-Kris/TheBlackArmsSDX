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
        if (GUILayout.Button("The Black Arms Discord"))
        {
            Application.OpenURL("https://discord.gg/m6UfHkY");
        }
        if (GUILayout.Button("TGE VRC Asset Server"))
        {
            Application.OpenURL("https://discord.gg/cbDhUZW");
        }
        if (GUILayout.Button("DEDSEC"))
        {
            Application.OpenURL("https://discord.gg/hEV4yKZ");
        }
        if (GUILayout.Button("TBA Guilded"))
        {
            Application.OpenURL("https://www.guilded.gg/i/Kk57LVQE");
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
        EditorGUILayout.LabelField("The Black Arms Discord: https://discord.gg/m6UfHkY", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("DEDSEC Discord: https://discord.gg/r7RcJCv", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("The Black Arms SDX Dev: PhoenixAceVFX", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("The Black Arms SDX Contributors", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Wolfie (Huge help with legal and github!)", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("PoH (taught me how to mod upload panels)", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("TheGamingBram (awesome script work)", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("ODDS (SDX is based in ODDS)", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Plague (LoadBundle Script Creator)", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Controser (For the AntiYoink!)", EditorStyles.boldLabel);
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
