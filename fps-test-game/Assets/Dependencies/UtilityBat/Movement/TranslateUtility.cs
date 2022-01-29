
using UnityEngine;

public class TranslateUtility : MonoBehaviour {

	public Vector3 movementSpeeds;

	private void Update () {

		transform.position += movementSpeeds * Time.deltaTime;
	}
}
