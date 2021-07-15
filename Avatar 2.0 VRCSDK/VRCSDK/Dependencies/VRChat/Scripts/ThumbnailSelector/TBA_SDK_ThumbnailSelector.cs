using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBA_SDK.ThumbnailSelector
{
    public class TBA_SDK_ThumbnailSelector : MonoBehaviour {
        private bool bAddScript = false;
        public Texture2D texture;
	
	    void Update () {
            if (false == bAddScript) {
                GameObject obj = GameObject.Find("VRCCam");
                if (null != obj)
                {
                    bAddScript = true;
                    obj.AddComponent<TBA_SDK_ThumbnailOverlay>();
                    TBA_SDK_ThumbnailOverlay script = obj.GetComponent<TBA_SDK_ThumbnailOverlay>();
                    if (null == script) {
                        Debug.Log("TBA_SDK_ThumbnailOverlay Script not Found");
                        return;
                    }
                    script.enabled = false;
                    script.SetTextuer(texture);
                }
            }
        }
    }
}
