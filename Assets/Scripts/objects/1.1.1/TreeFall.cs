using UnityEngine;
using System.Collections;

public class TreeFall : MonoBehaviour {

	private bool fallen = false;

	void OnTriggerEnter2D(Collider2D col) {
		if(col.tag == "Player" && !fallen) {
			col.gameObject.GetComponent<PlayerPhysics>().disableControl = true;
			StartCoroutine(treeFall());
			fallen = true;
		}
	}

	IEnumerator treeFall() {
		GameObject player = GameObject.Find("Player");
		GameObject camera = GameObject.Find("Main Camera");
		GameObject comet = GameObject.Find("Comet");
		GameObject tree = GameObject.Find("FallingTree");
		EntityAudio audio = GetComponent<EntityAudio>();
		audio.One("Comet", 1, false, -25);
		yield return new WaitForSeconds(2.5f);
		camera.GetComponent<CameraFollow>().shake = 2.3f;
		camera.GetComponent<CameraFollow>().shakeStrength = 1.0f;
		yield return new WaitForSeconds(2.8f);
		comet.AddComponent("Comet");
		yield return new WaitForSeconds(0.5f);
		player.GetComponent<PlayerPhysics>().disableControl = false;
		tree.rigidbody2D.isKinematic = false;
		tree.rigidbody2D.AddTorque(-9000000);
		audio.One("Tree_Fall_Heavy");
//tree.rigidbody2D.AddForceAtPosition(new Vector2(1000, 0), new Vector2(0, -50));
		tree.AddComponent("TouchOfDeath");
<<<<<<< HEAD
=======
		tree.transform.Find("Renderer").GetComponent<SpriteRenderer>().sprite = ravagedTreeSprite;
>>>>>>> b40ad8bd71416bdf47bad605963f793ddb63eff3
	}
}
