using UnityEngine;
using Spine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MushroomAnimator : Animateur {

	public override void Start() {
		actions = new Dictionary<string, Animateur.AnimationType>() {
			{"Grow", new AnimationType(Type.Spine, "grow")},
			{"Bounce", new AnimationType(Type.Spine, "bounce")}
		};

		base.Start();
	}
}