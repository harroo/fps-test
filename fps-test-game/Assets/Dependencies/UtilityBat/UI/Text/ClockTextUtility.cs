
using System;

using UnityEngine;
using UnityEngine.UI;

public class ClockTextUtility : MonoBehaviour {

	private Text text;

	private void Start () {

		text = GetComponent<Text>();
	}

	private void Update () {

		text.text = DateTime.Now.ToString();
	}
}
