using UnityEngine;
using UnityEditor;
using System.IO;
using System;
//using Amazon.S3.Model;
using UnityEngine.Serialization;

namespace theblackarmsSDX
{

    [InitializeOnLoad]
    public class theblackarmsSDX_Settings : EditorWindow
    {
        private const string Url = "https://github.com/TheBlackArms/TheBlackArmsSDX/";
        private const string Url1 = "https://trigon.systems/";
        private const string Link = "all-sdk/discord/";
        private const string Link1 = "home/";

        public static string projectConfigPath = "Assets/VRCSDK/theblackarmsSDX/Configs/";
        private string backgroundConfig = "BackgroundVideo.txt";
        private static string projectDownloadPath = "Assets/VRCSDK/theblackarmsSDX/Assets/";
        private static GUIStyle vrcSdkHeader;
        public Color SDKColor = Color.white;
        public static bool UITextRainbow { get; set; }
        //public Gradient SDKGRADIENT;

        [MenuItem("theblackarmsSDX/Settings", false, 501)]
        public static void OpenSplashScreen()
        {
            GetWindow<theblackarmsSDX_Settings>(true);
        }

        public static string getAssetPath()
        {
            if (EditorPrefs.GetBool("theblackarmsSDX_onlyProject", false))
            {
                return projectDownloadPath;
            }

            var assetPath = EditorPrefs.GetString("theblackarmsSDX_customAssetPath", "%appdata%/theblackarmsSDX/")
                .Replace("%appdata%", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData))
                .Replace("/", "\\");

            if (!assetPath.EndsWith("\\"))
            {
                assetPath += "\\";
            }

            Directory.CreateDirectory(assetPath);
            return assetPath;
        }

        public void OnEnable()
        {
            titleContent = new GUIContent("theblackarmsSDX Settings");

            maxSize = new Vector2(400, 600);
            minSize = maxSize;

            vrcSdkHeader = new GUIStyle
            {
                normal =
                {
                    background = Resources.Load("theblackarmsSDXHeader") as Texture2D,
                    textColor = Color.white
                },
                fixedHeight = 200
            };
            
            if (!EditorPrefs.HasKey("theblackarmsSDX_discordRPC"))
            {
                EditorPrefs.SetBool("theblackarmsSDX_discordRPC", true);
            }

            if (!File.Exists(projectConfigPath + backgroundConfig) || !EditorPrefs.HasKey("theblackarmsSDX_background"))
            {
                EditorPrefs.SetBool("theblackarmsSDX_background", false);
                File.WriteAllText(projectConfigPath + backgroundConfig, "False");
            }
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
            EditorGUILayout.BeginHorizontal();
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
            GUI.backgroundColor = new Color(
            UnityEditor.EditorPrefs.GetFloat("SDKColor_R"),
            UnityEditor.EditorPrefs.GetFloat("SDKColor_G"),
            UnityEditor.EditorPrefs.GetFloat("SDKColor_B"),
            UnityEditor.EditorPrefs.GetFloat("SDKColor_A")
        );

            GUILayout.Space(4);
            EditorGUILayout.BeginVertical();
            GUI.backgroundColor = new Color(
           UnityEditor.EditorPrefs.GetFloat("SDKColor_R"),
           UnityEditor.EditorPrefs.GetFloat("SDKColor_G"),
           UnityEditor.EditorPrefs.GetFloat("SDKColor_B"),
           UnityEditor.EditorPrefs.GetFloat("SDKColor_A")
       );

            EditorGUILayout.LabelField("SDK Settings", EditorStyles.boldLabel);
            EditorGUILayout.Space(10);
            //if (GUILayout.Button("Set Color"))
            //{
            //    UnityEditor.EditorPrefs.SetFloat("SDKColor_R", SDKColor.r);
            //    UnityEditor.EditorPrefs.SetFloat("SDKColor_G", SDKColor.g);
            //    UnityEditor.EditorPrefs.SetFloat("SDKColor_B", SDKColor.b);
            //    UnityEditor.EditorPrefs.SetFloat("SDKColor_A", SDKColor.a);
            //}

            EditorGUI.BeginChangeCheck();

            SDKColor = EditorGUI.ColorField(new Rect(3, 270, position.width - 6, 15), "SDK Color", SDKColor);
            //SDKGRADIENT = EditorGUI.GradientField(new Rect(3, 360, position.width - 6, 15), "SDK Gradient", SDKGRADIENT);

            if (EditorGUI.EndChangeCheck())
            {
                UnityEditor.EditorPrefs.SetFloat("SDKColor_R", SDKColor.r);
                UnityEditor.EditorPrefs.SetFloat("SDKColor_G", SDKColor.g);
                UnityEditor.EditorPrefs.SetFloat("SDKColor_B", SDKColor.b);
                UnityEditor.EditorPrefs.SetFloat("SDKColor_A", SDKColor.a);
            }

            EditorGUILayout.Space();
            if (GUILayout.Button("Reset Color"))
            {
                Color SDKColor = Color.gray;

                UnityEditor.EditorPrefs.SetFloat("SDKColor_R", SDKColor.r);
                UnityEditor.EditorPrefs.SetFloat("SDKColor_G", SDKColor.g);
                UnityEditor.EditorPrefs.SetFloat("SDKColor_B", SDKColor.b);
                UnityEditor.EditorPrefs.SetFloat("SDKColor_A", SDKColor.a);
            }

            //SDKGRADIENT = EditorGUI.GradientField(new Rect(3, 290, position.width - 6, 15), "SDK Gradient", SDKGRADIENT);

            EditorGUILayout.Space(10);
            EditorGUILayout.EndVertical();
            GUILayout.Label("Overall:");
            GUILayout.BeginHorizontal();
            var isDiscordEnabled = EditorPrefs.GetBool("theblackarmsSDX_discordRPC", true);
            var enableDiscord = EditorGUILayout.ToggleLeft("Discord RPC", isDiscordEnabled);
            if (enableDiscord != isDiscordEnabled)
            {
                EditorPrefs.SetBool("theblackarmsSDX_discordRPC", enableDiscord);
            }

            GUILayout.EndHorizontal();
            //Hide Console logs
            GUILayout.Space(4);
            GUILayout.BeginHorizontal();
            var isHiddenConsole = EditorPrefs.GetBool("theblackarmsSDX_HideConsole");
            var enableConsoleHide = EditorGUILayout.ToggleLeft("Hide Console Errors", isHiddenConsole);
            if (enableConsoleHide == true)
            {
                EditorPrefs.SetBool("theblackarmsSDX_HideConsole", true);
                Debug.ClearDeveloperConsole();
                Debug.unityLogger.logEnabled = false;
            }
            else if (enableConsoleHide == false)
            {
                EditorPrefs.SetBool("theblackarmsSDX_HideConsole", false);
                Debug.ClearDeveloperConsole();
                Debug.unityLogger.logEnabled = true;
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(4);
            GUILayout.BeginHorizontal();
            var isUITextRainbowEnabled = EditorPrefs.GetBool("theblackarmsSDX_UITextRainbow", false);
            var enableUITextRainbow = EditorGUILayout.ToggleLeft("Rainbow Text", isUITextRainbowEnabled);
            if (enableUITextRainbow != isUITextRainbowEnabled)
            {
                EditorPrefs.SetBool("theblackarmsSDX_UITextRainbow", enableUITextRainbow);
                UITextRainbow = true;
            }
            else
            {
                UITextRainbow = false;
            }


            GUILayout.EndHorizontal();
            GUILayout.Space(4);
            GUILayout.Label("Upload panel:");
            GUILayout.BeginHorizontal();
            var isBackgroundEnabled = EditorPrefs.GetBool("theblackarmsSDX_background", false);
            var enableBackground = EditorGUILayout.ToggleLeft("Custom background", isBackgroundEnabled);
            if (enableBackground != isBackgroundEnabled)
            {
                EditorPrefs.SetBool("theblackarmsSDX_background", enableBackground);
                File.WriteAllText(projectConfigPath + backgroundConfig, enableBackground.ToString());
            }

            GUILayout.EndHorizontal();


            GUILayout.Space(4);
            GUILayout.Label("Import panel:");
            GUILayout.BeginHorizontal();
            var isOnlyProjectEnabled = EditorPrefs.GetBool("theblackarmsSDX_onlyProject", false);
            var enableOnlyProject = EditorGUILayout.ToggleLeft("Save files only in project", isOnlyProjectEnabled);
            if (enableOnlyProject != isOnlyProjectEnabled)
            {
                EditorPrefs.SetBool("theblackarmsSDX_onlyProject", enableOnlyProject);
            }

            GUILayout.EndHorizontal();

            GUILayout.Space(4);
            GUI.backgroundColor = new Color(
             UnityEditor.EditorPrefs.GetFloat("SDKColor_R"),
             UnityEditor.EditorPrefs.GetFloat("SDKColor_G"),
             UnityEditor.EditorPrefs.GetFloat("SDKColor_B"),
             UnityEditor.EditorPrefs.GetFloat("SDKColor_A")
         );
            GUILayout.Label("Asset path:");
            GUILayout.BeginHorizontal();
            var customAssetPath = EditorGUILayout.TextField("",
                EditorPrefs.GetString("theblackarmsSDX_customAssetPath", "%appdata%/theblackarmsSDX/"));
            if (GUILayout.Button("Choose", GUILayout.Width(60)))
            {
                var path = EditorUtility.OpenFolderPanel("Asset download folder",
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "theblackarmsSDX");
                if (path != "")
                {
                    Debug.Log(path);
                    customAssetPath = path;
                }
            }

            if (GUILayout.Button("Reset", GUILayout.Width(50)))
            {
                customAssetPath = "%appdata%/theblackarmsSDX/";
            }

            if (EditorPrefs.GetString("theblackarmsSDX_customAssetPath", "%appdata%/theblackarmsSDX/") != customAssetPath)
            {
                EditorPrefs.SetString("theblackarmsSDX_customAssetPath", customAssetPath);
            }

            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            EditorPrefs.SetBool("theblackarmsSDX_ShowInfoPanel", GUILayout.Toggle(EditorPrefs.GetBool("theblackarmsSDX_ShowInfoPanel"), "Show at startup"));
            GUILayout.EndHorizontal();
        }
    }
}
// Soph waz 'ere