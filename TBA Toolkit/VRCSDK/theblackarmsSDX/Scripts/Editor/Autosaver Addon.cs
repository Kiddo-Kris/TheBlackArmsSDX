//https://docs.unity3d.com/ScriptReference/EditorApplication.SaveScene.html

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace AutoSaver
{
  [InitializeOnLoad]
    public class zombie2312SdkSplashScreen : EditorWindow
    {

        static zombie2312SdkSplashScreen()
        {
            EditorApplication.update -= DoSplashScreen;
            EditorApplication.update += DoSplashScreen;
        }

        private static void DoSplashScreen()
        {
            EditorApplication.update -= DoSplashScreen;
            if (!EditorPrefs.HasKey("ShowSplashScreen"))
            {
                EditorPrefs.SetBool("ShowSplashScreen", true);
            }
            if (EditorPrefs.GetBool("ShowSplashScreen"))
                OpenSplashScreen();
        }

        private static GUIStyle Header;
        private static Vector2 changeLogScroll;
		private float Timer = 60f;
		float TimeLeft = 0f;

        [MenuItem("theblackarmsSDX/Scene-autosaver", false, 500)]
        private static void Init()
		{
            zombie2312SdkSplashScreen window = EditorWindow.GetWindowWithRect<zombie2312SdkSplashScreen>(new Rect(0, 0, 400,180), true);
			window.Show();
		}

        public static void OpenSplashScreen()
        {
            GetWindowWithRect<zombie2312SdkSplashScreen>(new Rect(0, 0, 400,180), true);
        }

        public static void Open()
        {
            OpenSplashScreen();
        }

        public void OnEnable()
        {

            titleContent = new GUIContent("Auto Save");
            maxSize = new Vector2(300, 100);

        }

        public void OnGUI()
        {

			EditorGUILayout.LabelField("Time:", Timer + " Secs");
			int timeToSave = (int)(TimeLeft - EditorApplication.timeSinceStartup);
			EditorGUILayout.LabelField("time left:", timeToSave.ToString() + " Sec");
			this.Repaint();
			if (EditorApplication.timeSinceStartup > TimeLeft)
			{
				string[] path = EditorApplication.currentScene.Split(char.Parse("/"));
				path[path.Length - 1] = "AutoSave_" + path[path.Length - 1];
				EditorApplication.SaveScene(string.Join("/", path), true);
				TimeLeft = (int)(EditorApplication.timeSinceStartup + Timer);
			}


			GUILayout.BeginHorizontal();
			GUI.backgroundColor = Color.cyan;

			if (GUILayout.Button("Discord\n "))
			{
				Application.OpenURL("https://trigon.systems/all-sdk/discord/");
			}

			GUI.backgroundColor = Color.red;
			if (GUILayout.Button("chillzone\nworld"))
			{
				Application.OpenURL("vrchat://launch?ref=vrchat.com&id=wrld_cd44a1d5-35b7-4ded-b456-c9c386f9d96a");
			}

			GUI.backgroundColor = Color.yellow;
			if (GUILayout.Button("blackmarket\n(must be login)"))
			{
				Application.OpenURL("https://trigon.systems/");
			}

			GUI.backgroundColor = Color.green;
			if (GUILayout.Button("Avatar\nWorld (will be making one)"))
			{
				Application.OpenURL("");
			}

			GUI.backgroundColor = Color.white;
			GUILayout.EndHorizontal();
			GUILayout.Space(0);



			GUILayout.BeginHorizontal();
			GUI.backgroundColor = Color.gray;

			if (GUILayout.Button("Look for Updates\n(must be login)"))
			{
				Application.OpenURL("https://trigon.systems/");
			}





			GUI.backgroundColor = Color.white;
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			GUI.backgroundColor = Color.gray;

			if (GUILayout.Button("login"))
			{
				Application.OpenURL("https://trigon.systems/");
			}





			GUI.backgroundColor = Color.white;
			GUILayout.EndHorizontal();
			GUILayout.Space(0);

			changeLogScroll = GUILayout.BeginScrollView(changeLogScroll);
            GUILayout.EndScrollView();

            GUILayout.FlexibleSpace();

            GUILayout.BeginHorizontal();

            GUILayout.FlexibleSpace();
            EditorPrefs.SetBool("ShowSplashScreen", GUILayout.Toggle(EditorPrefs.GetBool("ShowSplashScreen"), "Toggle"));
            GUILayout.EndHorizontal();
        }

    }
}
#endif