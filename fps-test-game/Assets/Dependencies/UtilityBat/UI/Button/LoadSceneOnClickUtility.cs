
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class LoadSceneOnClickUtility : MonoBehaviour {

    public string sceneToLoad;

	private void Start () {

		Button button = GetComponent<Button>();

		button.onClick.AddListener(delegate{
			SceneManager.LoadScene(sceneToLoad);
		});
	}
}
