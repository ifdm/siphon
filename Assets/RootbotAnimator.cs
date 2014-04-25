using UnityEngine;
using Spine;
using System;
using System.Collections;
using System.Collections.Generic;

public class RootbotAnimator : Animateur {

	public override void Start() {
		actions = new Dictionary<string, Animateur.AnimationType>() {
			{"Cut", new AnimationType(Type.Spine, "cut-roots")},
			{"Die", new AnimationType(Type.Spine, "death")}
		};

		base.Start();
	}
}