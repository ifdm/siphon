using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerAudio : Mozart {

	public override void Awake() {
		clips = new Dictionary<string, AudioClip>() {
			{"Jump", Resources.Load<AudioClip>("Trella_Jump")},
			{"Land", Resources.Load<AudioClip>("Trella_land")},
			{"Run", Resources.Load<AudioClip>("Trella_Walk_2step")},
			{"Left Foot Step", Resources.Load<AudioClip>("Trella_step1")},
			{"Right Foot Step", Resources.Load<AudioClip>("Trella_step2")},
			{"Death", Resources.Load<AudioClip>("Death")}
		};
	}

	public override void AnimationEvent(string action) {
		if(action == "left-foot") Play("Left Foot Step", 0.8f);
		else if(action == "right-foot") Play("Right Foot Step", 0.8f);
	}
}