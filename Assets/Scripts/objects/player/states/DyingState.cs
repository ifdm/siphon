using UnityEngine;
using System.Collections;

public class DyingState : PlayerState {

	private float deathDelay = 0.6f;
	private float dyingTimer = 0;

	public override void HandleInput(PlayerControl player) {
		dyingTimer -= Time.deltaTime;
		Debug.Log(dyingTimer);
		if(dyingTimer <= 0){Application.LoadLevel(Application.loadedLevel);}
		if(dyingTimer > deathDelay * .67) {
			player.transform.Rotate(0, 0, (-(Time.deltaTime * 90) / deathDelay) * 3);
		}
	}

	public override void Enter(PlayerControl player, PlayerState from) {
		dyingTimer = deathDelay;
		GameObject.Find("Main Camera").GetComponent<FadeOut>().StartFade(Color.black, deathDelay);
		player.rigidbody2D.isKinematic = true;
	}
}