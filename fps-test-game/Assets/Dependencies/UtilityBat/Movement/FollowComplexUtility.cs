
using UnityEngine;

public class FollowComplexUtility : MonoBehaviour {

	private Transform parent;

	private void Start () {

		parent = transform.parent;
		transform.parent = null;
	}

	public bool destroyOnParentNull;
	public bool overridePosition, overrideRotation;
	public Vector3 positionOverride, rotationOverride;
	public Vector3 positionOffset;

	private void Update () {

		if (parent == null) {

			if (destroyOnParentNull) Destroy(gameObject);
			return;
		}

		if (overridePosition) transform.position = positionOverride;
		else transform.position = parent.position + positionOffset;

		if (overrideRotation) transform.eulerAngles = rotationOverride;
		else transform.rotation = parent.rotation;
	}
}
