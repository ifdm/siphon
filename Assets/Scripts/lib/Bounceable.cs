using UnityEngine;
using System.Collections;

public class Bounceable : MonoBehaviour {

	public float bounceForce;
	[HideInInspector] public bool bounced = false;

	void Bounced() {
		if(gameObject.tag == "Player") {
			gameObject.GetComponent<PlayerControl>().ChangeState(PlayerState.Jumping);
		}
	}
}
