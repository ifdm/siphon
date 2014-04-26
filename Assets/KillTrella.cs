using UnityEngine;
using System.Collections;

public class KillTrella : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.name == "Player") {
			//col.rigidbody2D.isKinematic = true;
			col.rigidbody2D.drag = 1;
			col.rigidbody2D.gravityScale = 0;
			StartCoroutine(ZoomOut());
		}
	}

	IEnumerator ZoomOut() {
		GameObject camera = GameObject.Find("Main Camera");
		CameraFollow follow = camera.GetComponent<CameraFollow>();
		GameObject player = GameObject.Find("Player");
		follow.pullTo = new Vector3(player.transform.position.x, player.transform.position.y, camera.transform.position.z);
		follow.pullSmooth = .5f;
		while(camera.transform.position.z > -120) {
			follow.pullTo = new Vector3(player.transform.position.x, player.transform.position.y, follow.pullTo.z);
			follow.pullTo -= Vector3.forward * Time.deltaTime;

			yield return null;
		}
	}
}
