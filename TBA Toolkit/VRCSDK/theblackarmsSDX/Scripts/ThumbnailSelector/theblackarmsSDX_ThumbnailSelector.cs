using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace theblackarmsSDX.ThumbnailSelector
{
    public class theblackarmsSDX_ThumbnailSelector : MonoBehaviour {
        private bool bAddScript = false;
        public Texture2D texture;
	
	    void Update () {
            if (false == bAddScript) {
                GameObject obj = GameObject.Find("VRCCam");
                if (null != obj)
                {
                    bAddScript = true;
                    obj.AddComponent<theblackarmsSDX_ThumbnailOverlay>();
                    theblackarmsSDX_ThumbnailOverlay script = obj.GetComponent<theblackarmsSDX_ThumbnailOverlay>();
                    if (null == script) {
                        Debug.Log("theblackarmsSDX_ThumbnailOverlay Script not Found");
                        return;
                    }
                    script.enabled = false;
                    script.SetTextuer(texture);
                }
            }
        }
    }
}
