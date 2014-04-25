using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {

	public float zoomTo = 600;
	public float zoomSpeed = 15;

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.name == "Player") {
			col.gameObject.rigidbody2D.isKinematic = false;
			StartCoroutine(zoomOut());
		}
	}

	IEnumerator zoomOut() {
		GameObject camera = GameObject.Find ("Main Camera");
		Destroy(camera.GetComponent<CameraFollow> ());

		while (camera.transform.position.z > -zoomTo) {
			camera.transform.position -= Vector3.forward * Time.deltaTime * zoomSpeed;
			yield return new WaitForSeconds(0.1f);
		}
	}
}
