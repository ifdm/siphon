using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.gameObject.tag == "Player") {
			GameObject checkpoint = GameObject.Find("Checkpoint Controller");
			if(checkpoint) {
				CheckpointController controller = checkpoint.GetComponent<CheckpointController>();
				controller.position = transform.position;
				controller.slots = (GameObject[])collider.gameObject.GetComponent<PlayerThrow>().slots.Clone();
			}
		}
	}
}