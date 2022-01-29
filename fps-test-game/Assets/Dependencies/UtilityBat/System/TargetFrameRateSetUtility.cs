
using UnityEngine;

public class TargetFrameRateSetUtility : MonoBehaviour {

    public int targetFrameRate;

    private void Start () {

        Application.targetFrameRate = targetFrameRate;
    }
}
