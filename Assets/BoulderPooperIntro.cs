using UnityEngine;
using System.Collections;

public class BoulderPooperIntro : MonoBehaviour {

	public float disableControlDuration = 3.2f;
	private bool dirty = false;

	void OnTriggerEnter2D(Collider2D col) {
		if(!dirty) {
			if(col.gameObject.name == "Player") {
				col.gameObject.GetComponent<PlayerPhysics>().disableControl = true;
				StartCoroutine(GiveControlBack());
				dirty = true;
			}
		}
	}

	IEnumerator GiveControlBack() {
		yield return new WaitForSeconds(disableControlDuration);
		GameObject.Find("Player").GetComponent<PlayerPhysics>().disableControl = false;
	}
}
