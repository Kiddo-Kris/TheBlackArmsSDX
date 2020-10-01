using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBA_Discord : MonoBehaviour
{

    private string url = "https://discord.gg/m6UfHkY";

    public void OpenURL()
    {
        Application.OpenURL(url);
    }
}