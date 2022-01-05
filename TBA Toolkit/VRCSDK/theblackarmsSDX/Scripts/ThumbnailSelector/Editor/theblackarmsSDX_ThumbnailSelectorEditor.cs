using UnityEditor;
using UnityEngine;
using System.IO;

namespace theblackarmsSDX.ThumbnailSelector
{
    [CustomEditor(typeof(theblackarmsSDX_ThumbnailOverlay))]
    public class VRCThumbnailSelectorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Image"))
            {
                GameObject obj = GameObject.Find("VRCCam");
                if (null == obj)
                {
                    Debug.Log("VRCCam not Found");
                    return;
                }

                theblackarmsSDX_ThumbnailOverlay script = obj.GetComponent<theblackarmsSDX_ThumbnailOverlay>();
                if (null == script)
                {
                    Debug.Log("theblackarmsSDX_ThumbnailOverlay Script not Found");
                    return;
                }

                string path = EditorUtility.OpenFilePanel("Nigga Select Image", "", "png,jpg,jpeg");

                if (path.Length > 0)
                {
                    Texture2D texture = new Texture2D(1, 1);
                    if (null != texture)
                    {
                        texture.LoadImage(File.ReadAllBytes(path));
                        texture.filterMode = FilterMode.Point;
                        script.SetTextuer(texture);
                        script.enabled = true;
                    }
                }
            }
        }
    }
}