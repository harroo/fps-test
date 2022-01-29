
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class OcularityMenuConstructor : MonoBehaviour {

    public static OcularityMenuConstructor instance;

    private List<GameObject> cache = new List<GameObject>();
    private GridLayoutGroup layoutGroup;

    private void Awake () {

        instance = this;

        layoutGroup = gameObject.AddComponent<GridLayoutGroup>();
    }

    public void Construct (OcularMenu oMenu) {

        Clear();

        GetComponent<RectTransform>().sizeDelta = oMenu.gridSize;
        layoutGroup.cellSize = oMenu.cellSize;
        layoutGroup.spacing = oMenu.spacing;

        layoutGroup.childAlignment = oMenu.alignment;

        oMenu.buttons.ForEach(delegate(OcularButton oButton) {

            GameObject button = new GameObject(oButton.name);
            button.transform.SetParent(transform);
            button.transform.localScale = new Vector3(1, 1, 1);
            cache.Add(button);

            GameObject textObject = new GameObject(oButton.name + " Text");
            textObject.transform.SetParent(button.transform);
            textObject.transform.localScale = new Vector3(1, 1, 1);

            Text text = textObject.AddComponent<Text>();
            textObject.GetComponent<RectTransform>().sizeDelta = oMenu.cellSize;
            text.font = OcularityPrefix.instance.font;
            text.text = oButton.title;
            text.color = OcularityPrefix.instance.fontColor;
            text.alignment = TextAnchor.MiddleCenter;

            var behaviour = button.AddComponent<OcularityButton>();
            behaviour.idleImage = OcularityPrefix.instance._idleImage;
            behaviour.idleColor = OcularityPrefix.instance.idleColor;
            behaviour.highlightedImage = OcularityPrefix.instance._highlightedImage;
            behaviour.highlightedColor = OcularityPrefix.instance.highlightedColor;
            behaviour.clickedImage = OcularityPrefix.instance._clickedImage;
            behaviour.clickedColor = OcularityPrefix.instance.clickedColor;
            behaviour.onClickMethod = oButton.onClick;

            behaviour.idname = oMenu.title + "::" + oButton.title;
        });

        oMenu.sliders.ForEach(delegate(OcularSlider oSlider) {

            GameObject slider = new GameObject(oSlider.name);
            slider.transform.SetParent(transform);
            slider.transform.localScale = new Vector3(1, 1, 1);
            cache.Add(slider);

            GameObject textObject = new GameObject(oSlider.name + " Text");
            textObject.transform.SetParent(slider.transform);
            textObject.transform.localScale = new Vector3(1, 1, 1);

            Text text = textObject.AddComponent<Text>();
            textObject.GetComponent<RectTransform>().sizeDelta = oMenu.cellSize;
            text.font = OcularityPrefix.instance.font;
            text.text = oSlider.title;
            text.color = OcularityPrefix.instance.fontColor;
            text.alignment = TextAnchor.MiddleCenter;
            text.raycastTarget = false;

            var behaviour = slider.AddComponent<OcularitySlider>();
            behaviour.backgroundImage = OcularityPrefix.instance.backgroundImage;
            behaviour.backgroundColor = OcularityPrefix.instance.backgroundColor;
            behaviour.idleImage = OcularityPrefix.instance._idleImage;
            behaviour.idleColor = OcularityPrefix.instance.idleColor;
            behaviour.highlightedImage = OcularityPrefix.instance._highlightedImage;
            behaviour.highlightedColor = OcularityPrefix.instance.highlightedColor;
            behaviour.clickedImage = OcularityPrefix.instance._clickedImage;
            behaviour.clickedColor = OcularityPrefix.instance.clickedColor;
            behaviour.onAdjustMethod = oSlider.onAdjust;
            behaviour.onEndAdjustMethod = oSlider.onEndAdjust;
            behaviour.minValue = oSlider.min;
            behaviour.maxValue = oSlider.max;
            if (oSlider.onLoad != null) oSlider.onLoad(behaviour);
            behaviour.Setup(new Vector2(oMenu.cellSize.x / 8.0f, oMenu.cellSize.y));

            behaviour.idname = oMenu.title + "::" + oSlider.title;
        });

        oMenu.toggles.ForEach(delegate(OcularToggle oToggle) {

            GameObject toggle = new GameObject(oToggle.name);
            toggle.transform.SetParent(transform);
            toggle.transform.localScale = new Vector3(1, 1, 1);
            cache.Add(toggle);

            GameObject textObject = new GameObject(oToggle.name + " Text");
            textObject.transform.SetParent(toggle.transform);
            textObject.transform.localScale = new Vector3(1, 1, 1);

            Text text = textObject.AddComponent<Text>();
            textObject.GetComponent<RectTransform>().sizeDelta = oMenu.cellSize;
            text.font = OcularityPrefix.instance.font;
            text.text = oToggle.title;
            text.color = OcularityPrefix.instance.fontColor;
            text.alignment = TextAnchor.MiddleCenter;
            text.raycastTarget = false;

            var behaviour = toggle.AddComponent<OcularityToggle>();
            behaviour.idleImage = OcularityPrefix.instance._idleImage;
            behaviour.idleColor = OcularityPrefix.instance.idleColor;
            behaviour.highlightedImage = OcularityPrefix.instance._highlightedImage;
            behaviour.highlightedColor = OcularityPrefix.instance.highlightedColor;
            behaviour.clickedImage = OcularityPrefix.instance._clickedImage;
            behaviour.clickedColor = OcularityPrefix.instance.clickedColor;
            behaviour.onToggleMethod = oToggle.onToggle;
            behaviour.Setup(oToggle.values, text, oToggle.title);
            if (oToggle.onLoad != null) oToggle.onLoad(behaviour);

            behaviour.idname = oMenu.title + "::" + oToggle.title;
        });

        oMenu.buttons.ForEach(delegate(OcularButton oButton) {

            foreach (var gameObject in cache) {

                OcularityButton behaviour = gameObject.GetComponent<OcularityButton>();
                if (behaviour == null) continue;

                if (behaviour.idname == oMenu.title + "::" + oButton.title)
                    gameObject.transform.SetSiblingIndex(oButton.order);
            }
        });

        oMenu.sliders.ForEach(delegate(OcularSlider oSlider) {

            foreach (var gameObject in cache) {

                OcularitySlider behaviour = gameObject.GetComponent<OcularitySlider>();
                if (behaviour == null) continue;

                if (behaviour.idname == oMenu.title + "::" + oSlider.title)
                    gameObject.transform.SetSiblingIndex(oSlider.order);
            }
        });

        oMenu.toggles.ForEach(delegate(OcularToggle oToggle) {

            foreach (var gameObject in cache) {

                OcularityToggle behaviour = gameObject.GetComponent<OcularityToggle>();
                if (behaviour == null) continue;

                if (behaviour.idname == oMenu.title + "::" + oToggle.title)
                    gameObject.transform.SetSiblingIndex(oToggle.order);
            }
        });
    }

    public void Clear () {

        cache.ForEach(delegate(GameObject go) {

            Destroy(go);
        });

        cache.Clear();
    }
}
