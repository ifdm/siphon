using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.gameObject.tag == "Player") {
			CheckpointController controller = GameObject.Find("Checkpoint Controller").GetComponent<CheckpointController>();
			controller.position = transform.position;
			controller.slots = (GameObject[])collider.gameObject.GetComponent<PlayerThrow>().slots.Clone();
		}
	}
}