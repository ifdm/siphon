using UnityEngine;
using System.Collections;

public class NewSeed : MonoBehaviour {

	public float alpha;
	private float z;
	private GameObject camera;
	private bool snap = true;

	void Start () {
		alpha = 0;
		camera = GameObject.Find("Main Camera");
		z = camera.camera.farClipPlane;
		StartCoroutine(fadeIn());
		renderer.material.color = new Color(1, 1, 1, 0);
	}

	void Update() {
		Color color = renderer.material.color;
		color.a = alpha;
		renderer.material.color = color;
		if(snap) {
			transform.position = camera.camera.ScreenToWorldPoint(new Vector3(camera.camera.pixelWidth / 2, camera.camera.pixelHeight / 2, z));
		}
	}

	IEnumerator fadeIn() {
		while(true) {
			alpha = Mathf.Lerp(alpha, 1, 8 * Time.deltaTime);

			if(z > 15) {
				z = Mathf.Lerp(z, 15 - 1, 8 * Time.deltaTime);
				if(z < 15){
					z = 15;
					camera.GetComponent<CameraFollow>().shake = 0.5f;
					camera.GetComponent<CameraFollow>().shakeStrength = 1.0f;
					camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z - 1);
					StartCoroutine(fadeOut());
					break;
				}
			}

			yield return new WaitForSeconds(0);
		}
	}

	IEnumerator fadeOut() {
		yield return new WaitForSeconds(1.35f);
		snap = false;

		while(true) {
			alpha = Mathf.Lerp(alpha, 0, 10 * Time.deltaTime);
			if(z < camera.camera.farClipPlane) {
				z = Mathf.Lerp(z, camera.camera.farClipPlane + 1, 5 * Time.deltaTime);
				if(z > camera.camera.farClipPlane) {
					Destroy(gameObject);
					break;
				}
			}

			transform.position = Vector3.Lerp(transform.position, camera.camera.ScreenToWorldPoint(new Vector3(10, camera.camera.pixelHeight -10, z)), 5 * Time.deltaTime);

			yield return new WaitForSeconds(0);
		}
	}
}
