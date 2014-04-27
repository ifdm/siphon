using UnityEngine;
using System.Collections;

public class IntroducePillars : MonoBehaviour {

	private bool dirty = false;

	void OnTriggerEnter2D(Collider2D col) {
		if(!dirty && col.gameObject.name == "Player") {
			col.gameObject.GetComponent<PlayerPhysics>().disableControl = true;
			StartCoroutine(GiveControlBack());
			dirty = false;
		}
	}

	IEnumerator GiveControlBack() {
		yield return new WaitForSeconds(2);
		GameObject.Find("Player").GetComponent<PlayerPhysics>().disableControl = false;
	}
}
