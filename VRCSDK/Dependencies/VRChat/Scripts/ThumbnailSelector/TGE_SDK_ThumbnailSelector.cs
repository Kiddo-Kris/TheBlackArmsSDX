using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGE_SDK.ThumbnailSelector
{
    public class TGE_SDK_ThumbnailSelector : MonoBehaviour {
        private bool bAddScript = false;
        public Texture2D texture;
	
	    void Update () {
            if (false == bAddScript) {
                GameObject obj = GameObject.Find("VRCCam");
                if (null != obj)
                {
                    bAddScript = true;
                    obj.AddComponent<TGE_SDK_ThumbnailOverlay>();
                    TGE_SDK_ThumbnailOverlay script = obj.GetComponent<TGE_SDK_ThumbnailOverlay>();
                    if (null == script) {
                        Debug.Log("TGE_SDK_ThumbnailOverlay Script not Found");
                        return;
                    }
                    script.enabled = false;
                    script.SetTextuer(texture);
                }
            }
        }
    }
}
