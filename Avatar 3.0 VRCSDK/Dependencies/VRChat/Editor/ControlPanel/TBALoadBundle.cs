using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using VRC.Core;


public partial class VRCSdkControlPanel : EditorWindow
{

    private static GUIStyle LoadImage;


    public string Avatar_AssetURL = "Place Asset URL here and press LOAD to preview model (Can't save from this)";

    void Showload()
    {
        LoadImage = new GUIStyle
        {
            normal =
            {
             background = Resources.Load("Settingsload") as Texture2D,
            },
            fixedHeight = 100
        };
        GUI.backgroundColor = Color.white;
        GUILayout.Box("", LoadImage);
        GUI.backgroundColor = new Color(
UnityEditor.EditorPrefs.GetFloat("SDKColor_R"),
UnityEditor.EditorPrefs.GetFloat("SDKColor_G"),
UnityEditor.EditorPrefs.GetFloat("SDKColor_B"),
UnityEditor.EditorPrefs.GetFloat("SDKColor_A")
);
        importsScroll = EditorGUILayout.BeginScrollView(importsScroll, GUILayout.Width(SdkWindowWidth));
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
        EditorGUILayout.LabelField("Avatar", EditorStyles.boldLabel);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(30));
        Avatar_AssetURL = GUI.TextField(new Rect(5, 50, 590, 30), Avatar_AssetURL);
        EditorGUILayout.Space();
        EditorGUILayout.EndHorizontal();

        //main part of the script
        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("Load Avatar"))
        {
            Debug.Log("Loading");
#pragma warning disable CS0618 // Type or member is obsolete
            WWW Avatar = new WWW(Avatar_AssetURL);
#pragma warning restore CS0618 // Type or member is obsolete
            while (!Avatar.isDone) ;
            Debug.Log(Avatar.progress);
            AssetBundle avatar = Avatar.assetBundle;
            GameObject ava = avatar.LoadAsset("_CustomAvatar") as GameObject;
            Instantiate(ava);
            AssetBundle.UnloadAllAssetBundles(false);
            Avatar.Dispose();
            Debug.Log("Load Sucsess");
        }

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        EditorGUILayout.LabelField("YOU CANT UPLOAD VRCA'S WITH THIS SCRIPT!!", EditorStyles.boldLabel);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndScrollView();
        GUILayout.FlexibleSpace();
        GUILayout.BeginVertical(boxGuiStyle, GUILayout.Height(20));
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField("LoadBundle script by Plague", EditorStyles.boldLabel);
        GUILayout.EndVertical();
    }
}

