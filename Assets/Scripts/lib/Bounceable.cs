using UnityEngine;
using System.Collections;

public class Bounceable : MonoBehaviour {

	public float bounceForce;
	[HideInInspector] public bool bounced = false;

	void Bounced(MushroomBounce mushroom) {
		if(gameObject.tag == "Player") {
			gameObject.GetComponent<PlayerControl>().ChangeState(PlayerState.Jumping);
			PlayerPhysics playerPhysics = gameObject.GetComponent<PlayerPhysics>();
			if(mushroom.horizontalForce > 0) {
				playerPhysics.airMove = false;
			}
			else {
				playerPhysics.airMove = true;
			}
		}
	}
}
