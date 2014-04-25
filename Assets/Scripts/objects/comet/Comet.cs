using UnityEngine;
using System.Collections;

public class Comet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		rigidbody2D.velocity = new Vector2(-38, -30);
		StartCoroutine(cometFlash());
	}
	
	void Update() {}
	
	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.name == "FallingTree") {
			GameObject camera = GameObject.Find("Main Camera");
			camera.GetComponent<CameraFollow>().shake = 1.0f;
			camera.GetComponent<CameraFollow>().shakeStrength = 6.0f;
			rigidbody2D.velocity = Vector2.zero;
			Destroy(GetComponent<SpriteRenderer>());
		}
	}

	IEnumerator cometFlash() {
		yield return new WaitForSeconds(0.2f);

		Light flash = transform.Find("Point light").gameObject.GetComponent<Light>();
		flash.intensity = 0;
		while(flash.intensity < 6) {
			flash.intensity += 12 * Time.deltaTime;
			yield return new WaitForSeconds(0);
		}

		while(flash.intensity > 0) {
			flash.intensity -= Time.deltaTime;
			yield return new WaitForSeconds(0);
		}

		Destroy(gameObject);
	}
}
