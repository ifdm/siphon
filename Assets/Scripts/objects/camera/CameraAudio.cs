using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CameraAudio : Mozart {

	public static string FOREST_ENVIRONMENT = "Forest_environment";
	public static string FOREST_HAPPY = "Siphon_Forest_Happy";

	private bool firstSong = true;
	private bool fadeout = false;
	private float volumeFade;
	private String nextSongName = null;

	public override void Awake() {
		clips = new Dictionary<string, AudioClip>() {
			{FOREST_ENVIRONMENT, Resources.Load<AudioClip>(FOREST_ENVIRONMENT)},
			{FOREST_HAPPY, Resources.Load<AudioClip>(FOREST_HAPPY)}
		};
	}

	public void Update(){
		if (fadeout) {
			Debug.Log("Fading!");
			volumeFade -= .1f * Time.deltaTime;
			foreach(KeyValuePair<string, AudioSource> source in sources) {
				source.Value.volume = volumeFade;
			}
			if(volumeFade <= 0.1f){
				ClearAll ();
				One (name, 1, true, 0);
				fadeout = false;
				volumeFade = 1.0f;
			}
		}

	}

	public void ChangeTrack(String name){
		nextSongName = name; 
		fadeout = true;
		firstSong = false;
	}

}