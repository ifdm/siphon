using UnityEngine;
using Spine;
using System;
using System.Collections;
using System.Collections.Generic;

public class WaterRobotAnimator : Animateur {

	public override void Start() {
		actions = new Dictionary<string, Animateur.AnimationType>() {
			{"Travel", new AnimationType(Type.Spine, "travel")},
			{"Suck", new AnimationType(Type.Spine, "sucking")},
			{"Dump", new AnimationType(Type.Spine, "dumping")}
		};

		base.Start();
	}

}