﻿#define COMMUNITY_LABS_SDK
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace VRCSDK2
{
    [InitializeOnLoad]
    public class VRC_SdkSplashScreen : EditorWindow
    {
        Color SDKColor = Color.gray;
        static VRC_SdkSplashScreen()
        {
            EditorApplication.update -= DoSplashScreen;
            EditorApplication.update += DoSplashScreen;
        }

        private static void DoSplashScreen()
        {
            EditorApplication.update -= DoSplashScreen;
            if (!EditorPrefs.HasKey("VRCSDK_ShowSplashScreen"))
            {
                EditorPrefs.SetBool("VRCSDK_ShowSplashScreen", true);
            }
            if (EditorPrefs.GetBool("VRCSDK_ShowSplashScreen"))
                OpenSplashScreen();
        }

        private static GUIStyle vrcSdkHeader;
        private static GUIStyle vrcSdkBottomHeader;
        private static GUIStyle vrcHeaderLearnMoreButton;
        private static GUIStyle vrcBottomHeaderLearnMoreButton;
        private static Vector2 changeLogScroll;
        [MenuItem("Phoenix/SDK/Info", false, 1)]
        public static void OpenSplashScreen()
        {
            GetWindow<VRC_SdkSplashScreen>(true);
        }
        
        public static void Open()
        {
            OpenSplashScreen();
        }

        public void OnEnable()
        {
            titleContent = new GUIContent("The Black Arms");

            maxSize = new Vector2(400, 270);
            minSize = maxSize;

            vrcSdkHeader = new GUIStyle
            {
                normal =
                    {
#if COMMUNITY_LABS_SDK
                        background = Resources.Load("vrcSdkHeader") as Texture2D,
#else
                        background = Resources.Load("vrcSdkHeader") as Texture2D,
#endif
                        textColor = Color.white
                    },
                fixedHeight = 200
            };

        }

        public void OnGUI()
        {
            GUILayout.Box("", vrcSdkHeader);


#if COMMUNITY_LABS_SDK
            vrcHeaderLearnMoreButton = EditorStyles.miniButton;
            vrcHeaderLearnMoreButton.normal.textColor = Color.black;
            vrcHeaderLearnMoreButton.fontSize = 12;
            vrcHeaderLearnMoreButton.border = new RectOffset(10, 10, 10, 10);
            //Texture2D texture = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Texture2D>("UI/Skin/UISprite.psd");
            //vrcHeaderLearnMoreButton.normal.background = texture;
            //vrcHeaderLearnMoreButton.active.background = texture;
            //if (GUI.Button(new Rect(20, 140, 180, 40), "Please Read", vrcHeaderLearnMoreButton))
            //{
            //    Application.OpenURL(CommunityLabsConstants.COMMUNITY_LABS_DOCUMENTATION_URL);
            //}
#endif
            GUILayout.Space(4);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Make sure to ABIDE by the LICENSE in Changelog");
            GUILayout.EndHorizontal();
            GUILayout.Space(4);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Click here to join the discord"))
            {
                Application.OpenURL("https://discord.gg/assetsuntamed");
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(4);

            GUILayout.FlexibleSpace();

            GUILayout.BeginHorizontal();

            GUILayout.FlexibleSpace();
            GUILayout.Label("Forget The Limits");
            GUI.backgroundColor = Color.white;
            EditorPrefs.SetBool("VRCSDK_ShowSplashScreen", GUILayout.Toggle(EditorPrefs.GetBool("VRCSDK_ShowSplashScreen"), "Show at Startup"));
             GUILayout.EndHorizontal();
        }

    }
}