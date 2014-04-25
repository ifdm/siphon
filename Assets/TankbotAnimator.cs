using UnityEngine;
using Spine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TankbotAnimator : Animateur {

	public override void Start() {
		actions = new Dictionary<string, Animateur.AnimationType>() {
			{"Alive", new AnimationType(Type.Spine, "animation")},
			{"Die", new AnimationType(Type.Spine, "death")}
		};

		base.Start();
	}
}