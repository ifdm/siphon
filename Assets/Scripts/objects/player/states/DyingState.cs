﻿using UnityEngine;
using System;
using System.Collections;

public class DyingState : PlayerState {

	private float deathDelay = 1.0f;
	private float dyingTimer = 0;

	public override void HandleInput(PlayerControl player) {
		Time.timeScale = Mathf.Lerp(Time.timeScale, .5f, 10 * Time.deltaTime);
		dyingTimer -= Time.deltaTime;
		if(dyingTimer <= 0) {
			Time.timeScale = 1;
			Application.LoadLevel(Application.loadedLevel);
		}
	}

	public override void Enter(PlayerControl player, PlayerState from) {
		dyingTimer = deathDelay;
		player.StartCoroutine(DelayFade(deathDelay * .4f));
	}

	private IEnumerator DelayFade(float delay) {
		yield return new WaitForSeconds(delay);
		GameObject.Find("Main Camera").GetComponent<FadeOut>().StartFade(Color.black, deathDelay * .6f);
	}
}