using UnityEngine;
using Spine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerAnimator : Animateur {

	public string particleType;

	public override void Start() {
		actions = new Dictionary<string, Animateur.AnimationType>() {
			{"Jump", new AnimationType(Type.Spine, "jump-up")},
			{"Run", new AnimationType(Type.Spine, "run")},
			{"Fall", new AnimationType(Type.Spine, "fall")},
			{"Ledge", new AnimationType(Type.Spine, "ledge-hang")},
			{"PullUp", new AnimationType(Type.Spine, "ledge-climb")},
			{"Stop", new AnimationType(Type.Spine, "stop")},
			{"Land", new AnimationType(Type.Spine, "land")},
			{"Throw", new AnimationType(Type.Spine, "throw")},
			{"Idle", new AnimationType(Type.Spine, "idle")},
			{"Climb", new AnimationType(Type.Spine, "climb-gv")},
			{"Push", new AnimationType(Type.Spine, "push")},
			{"Pull", new AnimationType(Type.Spine, "pull")},
			{"Turn", new AnimationType(Type.Spine, "turn-around")},
			{"Edge", new AnimationType(Type.Spine, "near-edge")},
			{"SpikeDeath", new AnimationType(Type.Spine, "spike-death")},
			{"BackHit", new AnimationType(Type.Spine, "back-hit")},
			{"FrontHit", new AnimationType(Type.Spine, "front-hit")},
			{"FallDeath", new AnimationType(Type.Spine, "fall-death")}
		};

		particleType = "dust";

		base.Start();
	}

	public void Emit(int count) {
		GameObject emitter = GameObject.Find(particleType);
		emitter.transform.position = transform.position;
		emitter.GetComponent<ParticleSystem>().Emit(count);
	}

	public void AnimationEvent(string action) {
		if(action == "left-foot" || action == "right-foot") {
			Emit(30);
		}
	}
}