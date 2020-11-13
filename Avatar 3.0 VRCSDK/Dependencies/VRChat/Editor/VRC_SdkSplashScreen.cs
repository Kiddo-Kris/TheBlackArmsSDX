#define COMMUNITY_LABS_SDK

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

        static VRC_SdkSplashScreen()
        {
            EditorApplication.update -= DoSplashScreen;
            EditorApplication.update += DoSplashScreen;
        }

        private static void DoSplashScreen()
        {
            EditorApplication.update -= DoSplashScreen;
            if (EditorApplication.isPlaying)
                return;

            #if UDON
                if (!EditorPrefs.GetBool("VRCSDK_ShowedSplashScreenFirstTime", false))
                {
                    OpenSplashScreen();
                    EditorPrefs.SetBool("VRCSDK_ShowedSplashScreenFirstTime", true);
                }
                else
            #endif
                if (EditorPrefs.GetBool("VRCSDK_ShowSplashScreen", true))
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
            titleContent = new GUIContent("The Black Arms Avatar 3.0 SDX");

            maxSize = new Vector2(400, 600);
            minSize = maxSize;

            vrcSdkHeader = new GUIStyle
            {
                normal =
                    {
#if UDON
                            background = Resources.Load("vrcSdkSplashUdon1") as Texture2D,
#elif COMMUNITY_LABS_SDK
                            background = Resources.Load("vrcSdkHeaderWithCommunityLabs") as Texture2D,
#else
                            background = Resources.Load("vrcSdkHeader") as Texture2D,
#endif
                        textColor = Color.white
                    },
                fixedHeight = 200
            };

            vrcSdkBottomHeader = new GUIStyle
            {
                normal =
                {
#if UDON
                        background = Resources.Load("vrcSdkSplashUdon2") as Texture2D,
#else
                        background = Resources.Load("vrcSdkBottomHeader") as Texture2D,
#endif

                    textColor = Color.white
                },
                fixedHeight = 100
            };

        }

        public void OnGUI()
        {
            GUILayout.Box("", vrcSdkHeader);

                vrcHeaderLearnMoreButton = EditorStyles.miniButton;
                vrcHeaderLearnMoreButton.normal.textColor = Color.black;
                vrcHeaderLearnMoreButton.fontSize = 12;
                vrcHeaderLearnMoreButton.border = new RectOffset(10, 10, 10, 10);
                Texture2D texture = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Texture2D>("UI/Skin/UISprite.psd");
                vrcHeaderLearnMoreButton.normal.background = texture;
                vrcHeaderLearnMoreButton.active.background = texture;
#if UDON
            if (GUI.Button(new Rect(20, 160, 185, 25), "Get Started with Udon", vrcHeaderLearnMoreButton))
                    Application.OpenURL("https://ask.vrchat.com/t/getting-started-with-udon/80");
#elif COMMUNITY_LABS_SDK
            if (GUI.Button(new Rect(20, 140, 180, 40), "Please Read", vrcHeaderLearnMoreButton))
                    Application.OpenURL(CommunityLabsConstants.COMMUNITY_LABS_DOCUMENTATION_URL);
#endif


            GUILayout.Space(4);
            GUILayout.BeginHorizontal();
            GUI.backgroundColor = Color.gray;
            if (GUILayout.Button("The Black Arms Discord"))
        {
            Application.OpenURL("https://discord.gg/m6UfHkY");
        }
        if (GUILayout.Button("TBA Assets"))
        {
            Application.OpenURL("https://discord.gg/cbDhUZW");
        }
        if (GUILayout.Button("DEDSEC"))
        {
            Application.OpenURL("https://discord.gg/r7RcJCv");
        }
        if (GUILayout.Button("TBA Guilded"))
        {
            Application.OpenURL("https://www.guilded.gg/i/Kk57LVQE");
        }
            GUI.backgroundColor = Color.white;
            GUILayout.EndHorizontal();
            GUILayout.Space(4);
            GUILayout.BeginHorizontal();
            GUI.backgroundColor = Color.gray;
            if (GUILayout.Button("VRC Avatar 3.0 Tutorials"))
            {
                Application.OpenURL("https://www.youtube.com/playlist?list=PLYeEcl-XxYVmXyXtQ0c33jXB54nklug2n");
            }
            if (GUILayout.Button("Latest SDX Release Page"))
            {
                Application.OpenURL("https://www.github.com/TheBlackArms/TheBlackArmsSDX/releases/latest");
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("SDX Support Server"))
            {
                Application.OpenURL("https://discord.gg/A9dca3N");
            }
            GUI.backgroundColor = Color.white;
            GUILayout.EndHorizontal();
            GUILayout.Space(2);

            changeLogScroll = GUILayout.BeginScrollView(changeLogScroll, false, false, GUIStyle.none, GUI.skin.verticalScrollbar, GUILayout.Width(395));

            GUILayout.Label(
    @"Welcome to The Black Arms SDX Avatar 3.0"
            );
            GUILayout.EndScrollView();

            GUILayout.Space(4);

            GUILayout.Box("", vrcSdkBottomHeader);
            vrcBottomHeaderLearnMoreButton = EditorStyles.miniButton;
            vrcBottomHeaderLearnMoreButton.normal.textColor = Color.black;
            vrcBottomHeaderLearnMoreButton.fontSize = 10;
            vrcBottomHeaderLearnMoreButton.border = new RectOffset(10, 10, 10, 10);
            vrcBottomHeaderLearnMoreButton.normal.background = texture;
            vrcBottomHeaderLearnMoreButton.active.background = texture;

#if UDON
            if (GUI.Button(new Rect(110, 520, 200, 25), "More Info and Examples", vrcBottomHeaderLearnMoreButton))
                Application.OpenURL("https://ask.vrchat.com/c/udon/5 ");
#else
            if (GUI.Button(new Rect(110, 525, 180, 42), "Click Here to see great\nassets for VRChat creation", vrcBottomHeaderLearnMoreButton))
                Application.OpenURL("https://assetstore.unity.com/lists/vrchat-picks-125734?aid=1101l7yuQ");
#endif

            //if (GUI.Button(new Rect(80, 540, 240, 30), "Learn how to create for VRChat Quest!", vrcBottomHeaderLearnMoreButton))
            //{
            //    Application.OpenURL("https://docs.vrchat.com/docs/creating-content-for-the-oculus-quest");
            //}

            GUILayout.FlexibleSpace();

            GUILayout.BeginHorizontal();

            GUILayout.FlexibleSpace();
            EditorPrefs.SetBool("VRCSDK_ShowSplashScreen", GUILayout.Toggle(EditorPrefs.GetBool("VRCSDK_ShowSplashScreen"), "Show at Startup"));

            GUILayout.EndHorizontal();
        }

    }
}