using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

	private bool paused;
	private float z = 5;
	private float alpha;
	private float delta;
	private float t;
	private SpriteRenderer[] graphics;

	void Start() {
		graphics = gameObject.GetComponentsInChildren<SpriteRenderer>();
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.P)) {
			TogglePause();
		}

		delta = Time.realtimeSinceStartup - t;
		if(paused) {
			Time.timeScale -= Mathf.Min(Time.deltaTime * 4 + .01f, Time.timeScale);
			alpha = Mathf.Lerp(alpha, 1, 8 * delta);
		}
		else {
			Time.timeScale += Mathf.Min(Time.deltaTime * 8 + .01f, 1 - Time.timeScale);
			alpha = Mathf.Lerp(alpha, 0, 8 * delta);
		}

		Color color = new Color(1, 1, 1, alpha);
		foreach(SpriteRenderer graphic in graphics) {
			graphic.material.color = color;
		}
		GameObject camera = GameObject.Find("Main Camera");
		transform.position = camera.camera.ScreenToWorldPoint(new Vector3(camera.camera.pixelWidth / 2, camera.camera.pixelHeight / 2 + 30, 15));
		t = Time.realtimeSinceStartup;
	}

	void TogglePause() {
		paused = !paused;
		alpha = paused ? 0 : 1;
		foreach(SpriteRenderer graphic in graphics) {
			graphic.material.color = new Color(1, 1, 1, alpha);
		}
	}
}
