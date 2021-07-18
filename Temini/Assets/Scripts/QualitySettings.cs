using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QualitySettings : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    
    private void Awake()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        var options = new List<string>();
        int currentResolutionIndex = 0;
        
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == UnityEngine.Screen.currentResolution.width && resolutions[i].height == UnityEngine.Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    
    public void resolutionDropdownClicked(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        bool fullScreen = Screen.fullScreen == true;

        Screen.SetResolution(resolution.width, resolution.height, fullScreen);
    }
    
    public void windowDropdownClicked(int style)
    {
        switch (style)
        {
            // borderless
            case 0:
                Screen.fullScreen = true;
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            // fullscreen
            case 1:
                Screen.fullScreen = true;
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            // windowed
            case 2:
                Screen.fullScreen = false;
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
        }
    }
}
