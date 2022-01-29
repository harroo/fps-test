
using UnityEngine;

public class FollowSmoothUtility : MonoBehaviour {

	public float lerpSpeed;
	public Transform target;
	public Vector3 offset;

	private void FixedUpdate () {

		transform.position = Vector3.Lerp(
			transform.position, 
			target.position + offset, 
			lerpSpeed * Time.deltaTime
		);
	}
}
