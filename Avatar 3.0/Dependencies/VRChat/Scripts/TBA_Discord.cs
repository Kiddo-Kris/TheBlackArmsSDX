using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBA_Discord : MonoBehaviour {

    private string url = "https://pastebin.com/022rqDb1";

    public void OpenURL()
    {
        Application.OpenURL(url);
    }
}
