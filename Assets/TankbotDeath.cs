using UnityEngine;
using System.Collections;

public class TankbotDeath : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.name == "Tree") {
			var animator = transform.Find("Animation").GetComponent<TankbotAnimator>();
			animator.One("Die");
		}
	}

}
