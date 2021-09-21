using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowedscreenHandler : MonoBehaviour
{
    private bool inFullscreen;
    public Image checkBoxImage;

    public void Start()
    {
        //check if the game starts in fullscreen
        if (Screen.fullScreen == true) 
            {
            inFullscreen = true;
            checkBoxImage.enabled = true;
            }
    }

    public void WindowedHandler() 
    {

        if (inFullscreen)
        { 
            Debug.Log("set to windowed");
            Screen.fullScreen = false;
            inFullscreen = false; 
            checkBoxImage.enabled = false;
        }
        else 
        {
            Debug.Log("set to fullscreen");
            Screen.fullScreen = true;
            inFullscreen = true;
            checkBoxImage.enabled = true;
        }
    }

}
