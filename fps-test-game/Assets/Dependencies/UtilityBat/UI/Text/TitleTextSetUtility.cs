
using UnityEngine;
using UnityEngine.UI;

public class TitleTextSetUtility : MonoBehaviour {

    private void Start () {

        GetComponent<Text>().text =
            Application.productName + " " + Application.version;
    }
}
