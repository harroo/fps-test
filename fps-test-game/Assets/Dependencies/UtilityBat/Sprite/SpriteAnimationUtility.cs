
using UnityEngine;

public class SpriteAnimationUtility : MonoBehaviour {

	public bool loop, destroyOnFinish;
	public float frameDelay;
	public Sprite[] frames;

	private float timer;
	private int index;
	private SpriteRenderer spriteRenderer;

	private void Start () {

		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void Update () {

		if (timer < 0) { timer = frameDelay;

			if (index == frames.Length) {

				if (loop) index = 0;
				else if (destroyOnFinish) Destroy(gameObject);

				timer = 0;
				return;
			}

			spriteRenderer.sprite = frames[index++];

		} else timer -= Time.deltaTime;
	}
}
