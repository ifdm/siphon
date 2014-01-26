using UnityEngine;
using Spine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MushroomAnimator : Animateur {

	public override void Start() {
		actions = new Dictionary<string, Animateur.AnimationType>() {
			{"Jump", new AnimationType(Type.Spine, "jump-up")},
			{"Run", new AnimationType(Type.Spine, "run")}
		};

		base.Start();
	}
}