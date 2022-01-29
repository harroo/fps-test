
using UnityEngine;

public class RotateUtility : MonoBehaviour {

	public Vector3 rotationSpeeds;

	private void Update () {

		transform.eulerAngles += rotationSpeeds * Time.deltaTime;
	}
}
