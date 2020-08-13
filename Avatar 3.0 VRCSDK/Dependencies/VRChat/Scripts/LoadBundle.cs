using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class LoadBundle : MonoBehaviour
{
    [HideInInspector]
    public bool World;

    [HideInInspector]
    public string World_AssetURL = "file:///C://";

    [HideInInspector]
    public bool Avtar = true;

    [HideInInspector]
    public string Avatar_AssetURL = "file:///C://";

    private IEnumerator Start()
    {
        if (World)
        {
            WWW www = new WWW(World_AssetURL);
            while (!www.isDone)
                yield return null;
            AssetBundle world = www.assetBundle;
            string[] scenePaths = world.GetAllScenePaths();
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePaths[0]);
            SceneManager.LoadSceneAsync(sceneName);
        }
        else
        {
            WWW www = new WWW(Avatar_AssetURL);
            while (!www.isDone)
                yield return null;
            AssetBundle avatar = www.assetBundle;
            GameObject ava = avatar.LoadAsset("_CustomAvatar") as GameObject;
            Instantiate(ava);
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(LoadBundle))]
public class RandomScript_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        LoadBundle loadbundle = (LoadBundle)target;
        loadbundle.World = EditorGUILayout.Toggle("World", loadbundle.World);
        if (loadbundle.World)
        {
            loadbundle.World_AssetURL = EditorGUILayout.TextField("World Asset URL", loadbundle.World_AssetURL);
            loadbundle.Avtar = false;
        }

        loadbundle.Avtar = EditorGUILayout.Toggle("Avatar", loadbundle.Avtar);
        if (loadbundle.Avtar)
        {
            loadbundle.Avatar_AssetURL = EditorGUILayout.TextField("Avatar Asset URL", loadbundle.Avatar_AssetURL);
            loadbundle.World = false;
        }
    }
}
#endif