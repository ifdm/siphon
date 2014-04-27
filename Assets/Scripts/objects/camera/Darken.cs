using UnityEngine;
using System.Collections;

public class Darken : MonoBehaviour {

	private Color originalColor;
	private Color target;
	private bool fading;
	private CameraFollow camera;

	public float factor = .6f;
	public float speed = 5;

	void Start() {
		originalColor = RenderSettings.ambientLight;
		camera = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
		target = Color.Lerp(originalColor, Color.black, factor);
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.name == "Player") {
			camera.targetAmbient = target;
			camera.ambientSmooth = speed;
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if(col.gameObject.name == "Player" && !col.gameObject.rigidbody2D.isKinematic) {
			camera.targetAmbient = originalColor;
			camera.ambientSmooth = speed;
		}
	}
}
