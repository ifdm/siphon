using UnityEngine;
using System.Collections;

public class GhostLift : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.name == "Lift") {
			BoxCollider2D[] allChildren = col.gameObject.GetComponentsInChildren<BoxCollider2D>();
			foreach (BoxCollider2D child in allChildren) {
			    child.isTrigger = true;
			}
		}
	}
}
