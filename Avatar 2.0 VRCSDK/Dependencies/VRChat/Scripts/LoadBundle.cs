using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class LoadBundle : MonoBehaviour
{
    public string AssetURL = "";

    private IEnumerator Start()
    {
        WWW www = new WWW(AssetURL);
        while (!www.isDone) yield return null;
		AssetBundle bundle = www.assetBundle;
	    if (AssetURL.EndsWith(".vrca") || AssetURL.StartsWith("http"))
	    {
			foreach (string asset in bundle.GetAllAssetNames())
            {
                if (asset.EndsWith(".prefab"))
                {
                    GameObject avatar = (GameObject)bundle.LoadAsset(asset);
                    Instantiate(avatar);
                    break;
                }
            }
	    }
	    
	    else if (AssetURL.EndsWith(".vrcw"))
        {
            string[] scenePaths = bundle.GetAllScenePaths();
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePaths[0]);
            SceneManager.LoadScene(sceneName);
        }
    }
}
