
using System;

using UnityEngine;
using UnityEngine.UI;

public class UIThemeUtility : MonoBehaviour {

    public bool loadOnStart = true;

    public ThemeProfile[] themes;

    private void Start () {

        if (loadOnStart)
            SetTheme(PlayerPrefs.GetString("THEME", "DARK"));
    }

    private void _SetTheme (string name) {

        ThemeColorProfile[] profiles = Array.Find(themes, c => c.name == name).profiles;

        foreach (var image in FindObjectsOfType<Image>(true)) {

            ThemeColorProfile profile = Array.Find(profiles, c => c.tag == image.gameObject.tag);

            if (profile != null) {

                image.color = profile.color;
            }
        }

        foreach (var text in FindObjectsOfType<Text>(true)) {

            ThemeColorProfile profile = Array.Find(profiles, c => c.tag == text.gameObject.tag);

            if (profile != null) {

                text.color = profile.color;
            }
        }
    }

    private static UIThemeUtility instance;

    private void Awake () {

        instance = this;
    }

    public static void SetUITheme (string themeName) {

        PlayerPrefs.SetString("THEME", themeName);

        instance._SetTheme(themeName);
    }

    public void SetTheme (string themeName) {

        PlayerPrefs.SetString("THEME", themeName);

        _SetTheme(themeName);
    }
}


[Serializable]
public class ThemeProfile {

    public string name;

    public ThemeColorProfile[] profiles;
}

[Serializable]
public class ThemeColorProfile {

    public string tag;

    public Color color;
}
