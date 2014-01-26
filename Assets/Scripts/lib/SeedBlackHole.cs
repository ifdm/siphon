using UnityEngine;
using System.Collections;

public class SeedBlackHole : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D col) {
		if(col.tag == "Seed") {
			Destroy(col.gameObject);
		}
	}
}
