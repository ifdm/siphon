using UnityEngine;
using Spine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BraceAnimator : Animateur {

	public override void Start() {
		actions = new Dictionary<string, Animateur.AnimationType>() {
			{"Idle", new AnimationType(Type.Spine, "idle")},
			{"Crack", new AnimationType(Type.Spine, "cracks")},
			{"Break", new AnimationType(Type.Spine, "breaks")}
		};

		base.Start();
	}

}