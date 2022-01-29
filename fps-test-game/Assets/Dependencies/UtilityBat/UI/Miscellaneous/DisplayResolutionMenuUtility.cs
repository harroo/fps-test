
using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;

public class DisplayResolutionMenuUtility : MonoBehaviour {

    public Dropdown resolutionDropdown;
    public Toggle fullScreenToggle;

    private Resolution[] resolutions;

    private void Start () {

        fullScreenToggle.isOn = Screen.fullScreen;
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++) {

            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (
                resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height
            ) {

                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void Apply () {

        Resolution resolution = resolutions[resolutionDropdown.value];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        Screen.fullScreen = fullScreenToggle.isOn;
    }
}
