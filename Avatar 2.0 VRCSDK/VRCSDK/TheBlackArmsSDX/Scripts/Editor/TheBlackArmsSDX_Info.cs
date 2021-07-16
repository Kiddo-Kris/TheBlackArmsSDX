using UnityEngine;
using UnityEditor;

namespace TheBlackArmsSDX
{
    [InitializeOnLoad]
    public class TheBlackArmsSDX_Info : EditorWindow
    {

        static TheBlackArmsSDX_Info()
        {
            EditorApplication.update -= DoSplashScreen;
            EditorApplication.update += DoSplashScreen;
        }

        private static void DoSplashScreen()
        {
            EditorApplication.update -= DoSplashScreen;
            if (!EditorPrefs.HasKey("TheBlackArmsSDX_ShowInfoPanel"))
            {
                EditorPrefs.SetBool("TheBlackArmsSDX_ShowInfoPanel", true);
            }
            if (EditorPrefs.GetBool("TheBlackArmsSDX_ShowInfoPanel"))
                OpenSplashScreen();
        }

        private static Vector2 changeLogScroll;
        private static GUIStyle vrcSdkHeader;
        [MenuItem("Phoenix/Info", false, 500)]
        public static void OpenSplashScreen()
        {
            GetWindow<TheBlackArmsSDX_Info>(true);
        }
        
        public static void Open()
        {
            OpenSplashScreen();
        }

        public void OnEnable()
        {
            titleContent = new GUIContent("TheBlackArmsSDX Info");
            
            minSize = new Vector2(400, 700);;

            vrcSdkHeader = new GUIStyle
            {
                normal =
                    {
                       background = Resources.Load("TheBlackArmsSDXHeader") as Texture2D,
                       textColor = Color.white
                    },
                fixedHeight = 200
            };
        }

        public void OnGUI()
        {
            GUILayout.Box("", vrcSdkHeader);
            GUILayout.Space(4);
            GUI.backgroundColor = new Color(
            UnityEditor.EditorPrefs.GetFloat("SDKColor_R"),
            UnityEditor.EditorPrefs.GetFloat("SDKColor_G"),
            UnityEditor.EditorPrefs.GetFloat("SDKColor_B"),
            UnityEditor.EditorPrefs.GetFloat("SDKColor_A")
        );
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("SDK made by PhoenixAceVFX/zombie2312"))
            {
                Application.OpenURL("https://github.com/TheBlackArms/TheBlackArmsSDX");
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("TheBlackArmsSDX Discord"))
            {
                Application.OpenURL("https://discord.gg/wngbcHS7NC");
            }
            if (GUILayout.Button("TheBlackArmsSDX Website"))
            {
                Application.OpenURL("https://trigon.systems/");
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(4);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("SDK Ver. 2.3.2"))
            {
                Application.OpenURL("https://chillzone.live/changelog/");
            }
            GUILayout.EndHorizontal();
            GUILayout.Label("TheBlackArmsSDX Version 2.3.2");
            GUILayout.Space(2);
            GUILayout.Label("Instructions for built in features and changelog");
            changeLogScroll = GUILayout.BeginScrollView(changeLogScroll, GUILayout.Width(390));

            GUILayout.Label(
@"
== V2.3.2 ==

Overall:
- Updated to newest VRCHAT SDK
- Removed useless Stuff
- Made upload a little faster
- Avatar Performance is now always Perfect
");
            GUILayout.EndScrollView();
            GUI.backgroundColor = new Color(
            UnityEditor.EditorPrefs.GetFloat("SDKColor_R"),
            UnityEditor.EditorPrefs.GetFloat("SDKColor_G"),
            UnityEditor.EditorPrefs.GetFloat("SDKColor_B"),
            UnityEditor.EditorPrefs.GetFloat("SDKColor_A")
        );
            GUILayout.BeginHorizontal();
            EditorPrefs.SetBool("TheBlackArmsSDX_ShowInfoPanel", GUILayout.Toggle(EditorPrefs.GetBool("TheBlackArmsSDX_ShowInfoPanel"), "Show at startup"));
            GUILayout.EndHorizontal();
        }

    }
}