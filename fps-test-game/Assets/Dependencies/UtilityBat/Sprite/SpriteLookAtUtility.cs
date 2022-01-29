
using UnityEngine;

public class SpriteLookAtUtility : MonoBehaviour {

	public bool lookAtMouse;
	public Transform target;

	private void Update () {

		if (lookAtMouse) {

			Vector3 mousePosition = Input.mousePosition;
			mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

			transform.up = new Vector2(
				mousePosition.x - transform.position.x,
				mousePosition.y - transform.position.y
			);

		} else if (target != null) {

			transform.up = new Vector2(
				target.position.x - transform.position.x,
				target.position.y - transform.position.y
			);
		}
	}
}
