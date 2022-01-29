
using System;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class OcularityToggle : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

    public Sprite idleImage, highlightedImage, clickedImage;
    public Color idleColor, highlightedColor, clickedColor;
    public Text titleText;

    public Action<string> onToggleMethod;

    private string[] _values;
    private int index;

    public string[] values { get { return _values; } set {} }
    public string value { get { return _values[index]; } set {} }

    private string title;
    private Image buttonImage;
    private bool highlighted;

    public string idname;

    public void Setup (string[] sa, Text t, string s) {

        _values = sa;
        titleText = t;
        title = s;
    }

    private void Start () {

        buttonImage = GetComponent<Image>();

        SetIndex(PlayerPrefs.GetInt(idname, 0));

        titleText.text = title + ": " + value;
    }

    private void Update () {

        if (!highlighted) return;

        if (Input.GetMouseButtonDown(0)) {

            buttonImage.sprite = clickedImage;
            buttonImage.color = clickedColor;
        }

        if (Input.GetMouseButtonUp(0)) {

            if (highlighted) {

                buttonImage.sprite = highlightedImage;
                buttonImage.color = highlightedColor;

            } else {

                buttonImage.sprite = idleImage;
                buttonImage.color = idleColor;
            }

            index++; if (index >= _values.Length) index = 0;
            value = values[index];

            titleText.text = title + ": " + value;

            if (onToggleMethod != null) onToggleMethod(value);
            PlayerPrefs.SetInt(idname, GetIndex());
        }
    }

    public void OnPointerClick (PointerEventData e) {

        // Uncomment these lines to enable sounds, Reverb is required.
        // if (OcularityPrefix.instance.clickSound != "")
        //     ReverbAudioManager.Play(OcularityPrefix.instance.clickSound);
    }

    public void OnPointerEnter (PointerEventData e) {

        buttonImage.sprite = highlightedImage;
        buttonImage.color = highlightedColor;
        highlighted = true;

        // Uncomment these lines to enable sounds, Reverb is required.
        // if (OcularityPrefix.instance.highlightSound != "")
        //     ReverbAudioManager.Play(OcularityPrefix.instance.highlightSound);
    }

    public void OnPointerExit (PointerEventData e) {

        buttonImage.sprite = idleImage;
        buttonImage.color = idleColor;
        highlighted = false;
    }

    public int GetIndex () => index;
    public void SetIndex (int i) { index = i; value = values[index]; }
}
