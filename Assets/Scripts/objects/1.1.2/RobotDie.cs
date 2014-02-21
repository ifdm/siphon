using UnityEngine;
using System.Collections;

public class RobotDie : MonoBehaviour {

	private bool broken = false;

	public void Start() {
		GameObject vine = GameObject.Find("GrappleVine/Animation");
		vine.GetComponent<Animator>().speed = 0;
	}

	// Use this for initialization
	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.name == "Boulder" && !broken) {
			GetComponent<SpriteRenderer>().color = new Color(.5f, .5f, .5f);
			GameObject vine = GameObject.Find("GrappleVine/Animation");
			vine.GetComponent<Animator>().speed = 1;
			broken = true;
		}
	}
}
