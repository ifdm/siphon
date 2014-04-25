using UnityEngine;
using System.Collections;

public class BoulderSound : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (rigidbody2D.velocity.x > 0.01) {
			GetComponent<EntityAudio>().One("roll", 1f, true);
		}
	}

}
