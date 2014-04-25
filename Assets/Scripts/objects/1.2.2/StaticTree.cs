using UnityEngine;
using System.Collections;

public class StaticTree : MonoBehaviour {

	public float disableControlDuration = .5f;

	void OnTriggerEnter2D(Collider2D col) {
		var interactable = col.gameObject.GetComponent<Interactable>();
		if(interactable) {
			Destroy(interactable);
			GameObject.Find("Player").GetComponent<PlayerControl>().ChangeState(PlayerState.Idling);
			GameObject.Find("Player").GetComponent<PlayerPhysics>().disableControl = true;
			StartCoroutine(giveControlBack());
		}
	}

	IEnumerator giveControlBack() {
		yield return new WaitForSeconds(disableControlDuration);
		GameObject.Find("Player").GetComponent<PlayerPhysics>().disableControl = false;
	}
}
