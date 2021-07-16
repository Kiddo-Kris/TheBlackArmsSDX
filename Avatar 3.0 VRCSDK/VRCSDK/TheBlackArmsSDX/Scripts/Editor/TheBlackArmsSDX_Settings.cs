using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using UnityEngine;
using UnityEditor;
//using Amazon.S3.Model;
using UnityEngine.Serialization;

namespace TheBlackArmsSDX
{
    [InitializeOnLoad]
    public class TheBlackArmsSDX_Settings : EditorWindow
    {
        public static string projectConfigPath = "Assets/VRCSDK/TheBlackArmsSDX/Configs/";
        private string backgroundConfig = "BackgroundVideo.txt";
        private static string projectDownloadPath = "Assets/VRCSDK/TheBlackArmsSDX/Assets/";
        private static GUIStyle vrcSdkHeader;
        
        public Color SDKColor = Color.gray;
       

        [MenuItem("Phoenix/Settings", false, 501)]
        public static void OpenSplashScreen()
        {
            GetWindow<TheBlackArmsSDX_Settings>(true);
        }

        public static string getAssetPath()
        {
            if (EditorPrefs.GetBool("TheBlackArmsSDX_onlyProject", false))
            {
                return projectDownloadPath;
            }

            var assetPath = EditorPrefs.GetString("TheBlackArmsSDX_customAssetPath", "%appdata%/TheBlackArmsSDX/")
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
            titleContent = new GUIContent("TheBlackArmsSDX Settings");

            maxSize = new Vector2(400, 700);
            minSize = maxSize;

            vrcSdkHeader = new GUIStyle
            {
                normal =
                {
                    background = Resources.Load("TheBlackArmsSDXHeader") as Texture2D,
                    textColor = Color.white
                },
                fixedHeight = 200
            };
            
            if (!EditorPrefs.HasKey("TheBlackArmsSDX_discordRPC"))
            {
                EditorPrefs.SetBool("TheBlackArmsSDX_discordRPC", true);
            }

            if (!File.Exists(projectConfigPath + backgroundConfig) || !EditorPrefs.HasKey("chill_zoneSDK_background"))
            {
                EditorPrefs.SetBool("TheBlackArmsSDX_background", false);
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

            if (GUILayout.Button("Set Color"))
            {
                UnityEditor.EditorPrefs.SetFloat("SDKColor_R", SDKColor.r);
                UnityEditor.EditorPrefs.SetFloat("SDKColor_G", SDKColor.g);
                UnityEditor.EditorPrefs.SetFloat("SDKColor_B", SDKColor.b);
                UnityEditor.EditorPrefs.SetFloat("SDKColor_A", SDKColor.a);
            }

            SDKColor = EditorGUI.ColorField(new Rect(3, 340, position.width - 6, 15), "SDK Color", SDKColor);

            
            EditorGUILayout.Space();
            if (GUILayout.Button("Reset Color"))
            {
                Color SDKColor = Color.gray;

                UnityEditor.EditorPrefs.SetFloat("SDKColor_R", SDKColor.r);
                UnityEditor.EditorPrefs.SetFloat("SDKColor_G", SDKColor.g);
                UnityEditor.EditorPrefs.SetFloat("SDKColor_B", SDKColor.b);
                UnityEditor.EditorPrefs.SetFloat("SDKColor_A", SDKColor.a);
            }

            // SDKGRADIENT = EditorGUI.GradientField(new Rect(3, 290, position.width - 6, 15), "SDK Gradient", SDKGRADIENT);

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            GUILayout.Label("Overall:");
            GUILayout.BeginHorizontal();
            var isDiscordEnabled = EditorPrefs.GetBool("TheBlackArmsSDX_discordRPC", true);
            var enableDiscord = EditorGUILayout.ToggleLeft("Discord RPC", isDiscordEnabled);
            if (enableDiscord != isDiscordEnabled)
            {
                EditorPrefs.SetBool("TheBlackArmsSDX_discordRPC", enableDiscord);
            }

            GUILayout.EndHorizontal();


            GUILayout.Space(4);
            GUILayout.Label("Upload panel:");
            GUILayout.BeginHorizontal();
            var isBackgroundEnabled = EditorPrefs.GetBool("TheBlackArmsSDX_background", false);
            var enableBackground = EditorGUILayout.ToggleLeft("Custom background", isBackgroundEnabled);
            if (enableBackground != isBackgroundEnabled)
            {
                EditorPrefs.SetBool("TheBlackArmsSDX_background", enableBackground);
                File.WriteAllText(projectConfigPath + backgroundConfig, enableBackground.ToString());
            }

            GUILayout.EndHorizontal();


            GUILayout.Space(4);
            GUILayout.Label("Import panel:");
            GUILayout.BeginHorizontal();
            var isOnlyProjectEnabled = EditorPrefs.GetBool("TheBlackArmsSDX_onlyProject", false);
            var enableOnlyProject = EditorGUILayout.ToggleLeft("Save files only in project", isOnlyProjectEnabled);
            if (enableOnlyProject != isOnlyProjectEnabled)
            {
                EditorPrefs.SetBool("TheBlackArmsSDX_onlyProject", enableOnlyProject);
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
                EditorPrefs.GetString("TheBlackArmsSDX_customAssetPath", "%appdata%/chill_zoneSDK/"));
            if (GUILayout.Button("Choose", GUILayout.Width(60)))
            {
                var path = EditorUtility.OpenFolderPanel("Asset download folder",
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "chill_zoneSDK");
                if (path != "")
                {
                    Debug.Log(path);
                    customAssetPath = path;
                }
            }

            if (GUILayout.Button("Reset", GUILayout.Width(50)))
            {
                customAssetPath = "%appdata%/TheBlackArmsSDX/";
            }

            if (EditorPrefs.GetString("TheBlackArmsSDX_customAssetPath", "%appdata%/TheBlackArmsSDX/") != customAssetPath)
            {
                EditorPrefs.SetString("TheBlackArmsSDX_customAssetPath", customAssetPath);
            }

            GUILayout.EndHorizontal();
        }
    }
}