
using UnityEngine;

public class ResolutionSetUtility : MonoBehaviour {

    public int width = 860, height = 480;

    private void Start () {

        Screen.SetResolution(width, height, false);
    }
}
