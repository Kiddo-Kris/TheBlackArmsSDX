using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TGE_Discord : MonoBehaviour
{

    private string url = "https://discord.gg/r7RcJCv";

    public void OpenURL()
    {
        Application.OpenURL(url);
    }
}