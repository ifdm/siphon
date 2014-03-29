using UnityEngine;
using System.Collections;

public class MovingSwitch : MonoBehaviour {

	public float leftY;
	public float rightY;

	private GameObject platform;
	private PlayerControl player;
	private bool interacting;

	void Start() {
		player = GameObject.Find("Player").GetComponent<PlayerControl>();
		platform = GameObject.Find("MovingPlatform");
	}

	void Update() {
		if(!interacting) interacting = player.isInteracting();
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(interacting) {
			if(col.gameObject.name == "Lever") {
				if(gameObject.name == "Switch Trigger Left") {
					platform.GetComponent<MovingPlatformMove>().targetY = leftY;
				}
				else {
					platform.GetComponent<MovingPlatformMove>().targetY = rightY;
				}

				interacting = false;
			}
		}
	}
}
