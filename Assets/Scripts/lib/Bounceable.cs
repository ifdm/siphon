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
		}
	}
}
