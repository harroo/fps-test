
using UnityEngine;

public class KeyToggleObjectUtility : MonoBehaviour {

    public GameObject go;

    public KeyCode key;

    private void Update () {

        if (Input.GetKeyUp(key))
            go.SetActive(!go.activeSelf);
    }
}
