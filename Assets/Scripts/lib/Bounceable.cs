using UnityEngine;
using System.Collections;

public class Bounceable : MonoBehaviour {

	public float bounceForce;
	[HideInInspector] public bool bounced = false;

	void Bounced(bool locked) {
		if(gameObject.tag == "Player") {
			PlayerControl playerControl = gameObject.GetComponent<PlayerControl>();
			if(playerControl.state != PlayerState.Dying) {
				playerControl.ChangeState(PlayerState.Jumping);
			}
			PlayerPhysics playerPhysics = gameObject.GetComponent<PlayerPhysics>();
			if(locked) {
				playerPhysics.airMove = false;
				gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z);
			}
			else {
				playerPhysics.airMove = true;
			}
		}
	}
}
