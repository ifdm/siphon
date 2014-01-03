using UnityEngine;
using System.Collections;

public class MushroomBounce : MonoBehaviour {

	void Awake() {		
		RaycastHit2D hit = Physics2D.Linecast(transform.position, transform.position - (Vector3.up * 100), 1 << LayerMask.NameToLayer("Ground"));
		if(hit) {
			transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		bool flag = false;
		foreach (ContactPoint2D contact in col.contacts) {
			if(contact.collider.name.Equals("Player") && contact.normal.y < 0){
				flag = true;
			}
		}
		if (flag) {
			col.collider.SendMessage("MushroomBounceEvent");
		}
	}
}