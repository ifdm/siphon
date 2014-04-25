using UnityEngine;
using System.Collections;

public class BoulderAftermath : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {
		Interactable interactable = col.gameObject.GetComponent<Interactable>();
		if(interactable) {
			col.rigidbody2D.mass = interactable.staticWeight;
			Destroy(interactable);
			GameObject.Find("Player").GetComponent<PlayerPhysics>().disableControl = true;
			GameObject.Find("Player").GetComponent<PlayerControl>().ChangeState(PlayerState.Idling);
			StartCoroutine(control());
		}
	}

	IEnumerator control() {
		yield return new WaitForSeconds(3f);
		GameObject.Find("Player").GetComponent<PlayerPhysics>().disableControl = false;
	}
}
