using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptVerification : MonoBehaviour {

    [HideInInspector]
    public static bool isVerified = false;

    //i am aware that this method isnt exacly foolproof but hey, no need for complex stuffs

	// Use this for initialization
	public static bool Verify () {
        if (System.IO.File.Exists("Assets/VRCSDK/Dependecies/VR Chat/Scripts/Changelog_1334.txt"))
            isVerified = true;
        return isVerified;
    }
	
	// Update is called once per frame
	void Update () {
        if (isVerified = false)
            for (int i = 0; i < 1; i++)
            {
                Debug.Log("owo");
            }
	}
}
