using UnityEngine;
using System.Collections;

public class TreeFall : MonoBehaviour {

	private bool fallen = false;
	public Sprite ravagedTreeSprite;

	void Start() {
		transform.Find("Darken").gameObject.SetActive(false);
	}

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
		transform.Find("Darken").gameObject.SetActive(true);
		transform.Find("Darken").gameObject.GetComponent<Darken>().speed = .25f;
		yield return new WaitForSeconds(2.5f);
		camera.GetComponent<CameraFollow>().shake = 2.3f;
		camera.GetComponent<CameraFollow>().shakeStrength = 1.0f;
		yield return new WaitForSeconds(2.8f);
		Destroy(GetComponent<Darken>());
		comet.AddComponent("Comet");
		yield return new WaitForSeconds(0.5f);
		transform.Find("Darken").gameObject.GetComponent<Darken>().speed = .06f;
		player.GetComponent<PlayerPhysics>().disableControl = false;
		BoxCollider2D box = tree.GetComponent<BoxCollider2D>();
		tree.AddComponent("TouchOfDeath");
		tree.rigidbody2D.isKinematic = false;
		tree.rigidbody2D.AddForceAtPosition(new Vector2(20, 0), new Vector2(0, tree.transform.position.y - (box.size.y * tree.transform.localScale.y)), ForceMode2D.Impulse);
		audio.One("Tree_Fall_Heavy");
		tree.transform.Find("Renderer").GetComponent<SpriteRenderer>().sprite = ravagedTreeSprite;
	}
}
