
using UnityEngine;

public class MainMenu : MonoBehaviour {

    static OcularMenu title = new OcularMenu () {

        gridSize = new Vector2(300, 300),
        spacing = new Vector2(32, 32),

        buttons = {

            new OcularButton() { title = "Play", order = 1,
            onClick = () => {

                UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
            } },

            new OcularButton() { title = "Quit", order = 2,
            onClick = () => {

                Application.Quit();
            } },
        },
    };

    private void Start () {

        Ocularity.ShowMenu(title);
    }
}
