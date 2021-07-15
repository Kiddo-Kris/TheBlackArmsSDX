using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBA_Discord : MonoBehaviour {

    private string url = "https://discord.gg/ryDrr4m";

    public void OpenURL()
    {
        Application.OpenURL(url);
    }
}
