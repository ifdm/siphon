using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerAudio : Mozart {

	public override void Awake() {
		clips = new Dictionary<string, AudioClip>() {
			{"Jump", Resources.Load<AudioClip>("Trella_Jump")},
			{"Run", Resources.Load<AudioClip>("Trella_Walk_2step")},
			{"Left Foot Step", Resources.Load<AudioClip>("Trella_step1")},
			{"Right Foot Step", Resources.Load<AudioClip>("Trella_step2")}
		};
	}

	public override void AnimationEvent(string action) {
		// Use this for special events based on player animations
	}
}