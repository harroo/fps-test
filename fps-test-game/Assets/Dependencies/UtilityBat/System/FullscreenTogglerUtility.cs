
using UnityEngine;

public class FullscreenTogglerUtility : MonoBehaviour {

    public int defaultWidth = 860, defaultHeight = 480;

    private Resolution highestResolution;

    private void Start () {

        foreach (Resolution resolution in Screen.resolutions) {

            if (
                resolution.width > highestResolution.width ||
                resolution.height > highestResolution.height
            ) {
                highestResolution = resolution;
            }
        }
    }

    private void Update () {

        if (!Input.GetKeyDown(KeyCode.F11)) return;

        if (Screen.fullScreen) {

            Screen.SetResolution(defaultWidth, defaultHeight, false);

        } else {

            Screen.SetResolution(highestResolution.width, highestResolution.height, FullScreenMode.ExclusiveFullScreen);
        }
    }
}
