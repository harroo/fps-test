
using UnityEngine;
using UnityEngine.UI;

public class VersionTextSetUtility : MonoBehaviour {

    private void Start () {

        GetComponent<Text>().text = Application.version;
    }
}
