using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CameraAudio : Mozart {

	public static string FOREST_ENVIRONMENT = "Forest_environment";
	public static string FOREST_HAPPY = "Siphon_Forest_Happy";
	public string currentTrack;

	//Currently, there are only two song tracks...
	public override void Awake() {
		clips = new Dictionary<string, AudioClip>() {
			{FOREST_ENVIRONMENT, Resources.Load<AudioClip>(FOREST_ENVIRONMENT)},
			{FOREST_HAPPY, Resources.Load<AudioClip>(FOREST_HAPPY)}
		};
	}

	public override void Start() {
		base.Start();
		Play(FOREST_ENVIRONMENT, 0.5f);
	}

}