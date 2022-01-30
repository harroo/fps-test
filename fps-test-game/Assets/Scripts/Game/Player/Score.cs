
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public Text text;
    private int score;

    public void Up () {

        score++;

        text.text = "Score: " + score.ToString();
    }

    private void Start () {

        Reset();
    }

    public void Reset () {

        score = 0;

        text.text = "Score: " + score.ToString();
    }
}
