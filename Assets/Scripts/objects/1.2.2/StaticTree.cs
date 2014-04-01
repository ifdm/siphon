using UnityEngine;
using System.Collections;

public class StaticTree : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D col) {
		var interactable = col.gameObject.GetComponent<Interactable>();
		if(interactable) {
			Destroy(interactable);
		}
	}
}
