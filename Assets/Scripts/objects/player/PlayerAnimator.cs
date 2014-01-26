using UnityEngine;
using Spine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerAnimator : Animateur {

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
			{"Idle", new AnimationType(Type.Spine, "idle")}
		};

		base.Start();
	}
}