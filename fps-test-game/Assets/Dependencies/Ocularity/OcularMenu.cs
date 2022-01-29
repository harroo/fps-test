
using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class OcularMenu {

    public string name = "oMenu";
    public string title;

    public List<OcularButton> buttons = new List<OcularButton>();
    public List<OcularSlider> sliders = new List<OcularSlider>();
    public List<OcularToggle> toggles = new List<OcularToggle>();

    public Vector2 gridSize = new Vector2(300, 300);
    public Vector2 cellSize = new Vector2(265, 64);
    public Vector2 spacing = new Vector2(6, 6);

    public TextAnchor alignment = TextAnchor.MiddleCenter;
}

[Serializable]
public class OcularButton {

    public string name = "oButton";
    public string title;
    public int order;

    public Action onClick;
}

[Serializable]
public class OcularSlider {

    public string name = "oSldier";
    public string title;
    public int order;

    public float min = 0, max = 100;

    public Action<OcularitySlider> onLoad;
    public Action<float> onAdjust, onEndAdjust;
}

[Serializable]
public class OcularToggle {

    public string name = "oToggle";
    public string title;
    public int order;

    public string[] values;

    public Action<OcularityToggle> onLoad;
    public Action<string> onToggle;
}
