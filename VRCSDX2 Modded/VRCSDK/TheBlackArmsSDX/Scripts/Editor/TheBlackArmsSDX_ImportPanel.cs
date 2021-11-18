using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Newtonsoft.Json.Linq;

namespace TheBlackArmsSDX
{
    [InitializeOnLoad]
    public class TheBlackArmsSDX_ImportPanel : EditorWindow
    {
        private static GUIStyle _nanoHeader;
        private static Dictionary<string, string> assets = new Dictionary<string, string>();
        private static int _sizeX = 400;
        private static int _sizeY = 5000;
        private static Vector2 _changeLogScroll;
        

        [MenuItem("Phoenix/Import panel", false, 501)]
        public static void OpenImportPanel()
        {
            GetWindow<TheBlackArmsSDX_ImportPanel>(true);
        }

        public void OnEnable()
        {
            titleContent = new GUIContent("TheBlackArmsSDX Import panel");

            TheBlackArmsSDX_ImportManager.checkForConfigUpdate();
            LoadJson();

            maxSize = new Vector2(_sizeX, _sizeY);
            minSize = maxSize;

            _nanoHeader = new GUIStyle
            {
                normal =
                {
                    background = Resources.Load("TheBlackArmsSDXHeader") as Texture2D,
                    textColor = Color.white
                },
                fixedHeight = 200
            };
        }

        public static void LoadJson()
        {
            assets.Clear();
            
            dynamic configJson =
                JObject.Parse(File.ReadAllText(TheBlackArmsSDX_Settings.projectConfigPath + TheBlackArmsSDX_ImportManager.configName));

            Debug.Log("Server Asset Url is: " + configJson["config"]["serverUrl"]);
            TheBlackArmsSDX_ImportManager.serverUrl = configJson["config"]["serverUrl"].ToString();
            _sizeX = (int)configJson["config"]["window"]["sizeX"];
            _sizeY = (int)configJson["config"]["window"]["sizeY"];

            foreach (JProperty x in configJson["assets"])
            {
                var value = x.Value;

                var buttonName = "";
                var file = "";
                
                foreach (var jToken in value)
                {
                    var y = (JProperty) jToken;
                    switch (y.Name)
                    {
                        case "name":
                            buttonName = y.Value.ToString();
                            break;
                        case "file":
                            file = y.Value.ToString();
                            break;
                    }
                }
                assets[buttonName] = file;
            }
        }

        public void OnGUI()
        {
            GUILayout.Box("", _nanoHeader);
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
            //Update assets
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Update assets (config)"))
            {
                TheBlackArmsSDX_ImportManager.updateConfig();
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(4);



            //Imports V!V
            GUI.backgroundColor = new Color(
            UnityEditor.EditorPrefs.GetFloat("SDKColor_R"),
            UnityEditor.EditorPrefs.GetFloat("SDKColor_G"),
            UnityEditor.EditorPrefs.GetFloat("SDKColor_B"),
            UnityEditor.EditorPrefs.GetFloat("SDKColor_A")
        );
            _changeLogScroll = GUILayout.BeginScrollView(_changeLogScroll, GUILayout.Width(_sizeX));
            GUI.backgroundColor = new Color(
            UnityEditor.EditorPrefs.GetFloat("SDKColor_R"),
            UnityEditor.EditorPrefs.GetFloat("SDKColor_G"),
            UnityEditor.EditorPrefs.GetFloat("SDKColor_B"),
            UnityEditor.EditorPrefs.GetFloat("SDKColor_A")
        );
            foreach (var asset in assets)
            {
                GUILayout.BeginHorizontal();
                if (asset.Value == "")
                {
                    GUILayout.FlexibleSpace();
                    GUILayout.Label(asset.Key);
                    GUILayout.FlexibleSpace();
                }
                else
                {
                    if (GUILayout.Button(
                        (File.Exists(TheBlackArmsSDX_Settings.getAssetPath() + asset.Value) ? "Import" : "Download") +
                        " " + asset.Key))
                    {
                        TheBlackArmsSDX_ImportManager.downloadAndImportAssetFromServer(asset.Value.Split('\\')[0], asset.Value.Split('\\')[1]);
                    }

                    if (GUILayout.Button("Del", GUILayout.Width(40)))
                    {
                        TheBlackArmsSDX_ImportManager.deleteAsset(asset.Value);
                    }
                }
                GUILayout.EndHorizontal();
            }
            
            GUILayout.EndScrollView();
        }
    }
}