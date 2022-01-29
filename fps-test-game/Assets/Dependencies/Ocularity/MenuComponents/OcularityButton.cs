
using System;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class OcularityButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

    public Sprite idleImage, highlightedImage, clickedImage;
    public Color idleColor, highlightedColor, clickedColor;

    public Action onClickMethod;

    private Image buttonImage;
    private bool highlighted;

    public string idname;

    private void Start () {

        buttonImage = GetComponent<Image>();

        buttonImage.sprite = idleImage;
        buttonImage.color = idleColor;
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

            if (onClickMethod != null) onClickMethod();
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
}
