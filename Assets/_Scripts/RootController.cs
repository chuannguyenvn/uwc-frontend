using System;
using UnityEngine;

public class RootController : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
        
#if UNITY_ANDROID && !UNITY_EDITOR
    Screen.fullScreen = false; //Should be unnecessary unless you changed it
    AndroidUtility.ShowStatusBar(Color.black);
#endif
    }
}