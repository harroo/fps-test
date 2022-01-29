
using UnityEngine;
using UnityEngine.UI;

public class InputFieldPrefSetUtility : MonoBehaviour {

	public string valueKey, defaultValue;

	private InputField inputField;

	private void Start () {

		inputField = GetComponent<InputField>();

		inputField.text = PlayerPrefs.GetString(valueKey, defaultValue);

		inputField.onEndEdit.AddListener(delegate{
			PlayerPrefs.SetString(valueKey, inputField.text);
		});
	}
}
