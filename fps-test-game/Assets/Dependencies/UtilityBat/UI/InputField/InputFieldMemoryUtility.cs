
using UnityEngine;
using UnityEngine.UI;

public class InputFieldMemoryUtility : MonoBehaviour {

	private InputField inputField;

	private void Start () {

		inputField = GetComponent<InputField>();

		inputField.text = PlayerPrefs.GetString(gameObject.name, "");

		inputField.onEndEdit.AddListener(delegate{
			PlayerPrefs.SetString(gameObject.name, inputField.text);
		});
	}
}
