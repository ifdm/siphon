using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

//A simple extension of the Mozart class allowing names of audio files to be set through the inspector.
//Call the Play or One functions of this class when necessary inside the behavior of the relevant GameObject
public class EntityAudio : Mozart {

	public string[] names;

	public override void Awake() {
		clips = new Dictionary<string, AudioClip>();
		foreach (string name in names){
			clips.Add(name, Resources.Load<AudioClip>(name));
		}
	}
}
