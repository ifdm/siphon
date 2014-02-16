using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CameraAudio : Mozart {

	public static string FOREST_ENVIRONMENT = "Forest_environment";
	public static string FOREST_HAPPY = "Siphon_Forest_Happy";

	public float fadeRate = 0.2f;

	private bool firstSong = true;
	private float volumeFade;
	private String nextSongName = null;
	private String currentSongName = null;

	//Currently, there are only two song tracks...
	public override void Awake() {
		clips = new Dictionary<string, AudioClip>() {
			{FOREST_ENVIRONMENT, Resources.Load<AudioClip>(FOREST_ENVIRONMENT)},
			{FOREST_HAPPY, Resources.Load<AudioClip>(FOREST_HAPPY)}
		};
	}

	//Handles song fading and track changing
	public void Update(){
		if (nextSongName != null) {
			volumeFade -= fadeRate * Time.deltaTime;
			foreach(KeyValuePair<string, AudioSource> source in sources) {
				source.Value.volume = volumeFade;
			}
			if(volumeFade <= 0.1f){
				ClearAll ();
				One (nextSongName, 1, true, 0);
				currentSongName = nextSongName;
				nextSongName = null;
			}
		}
	}
	
	//To be used by audio triggers
	public void PlayAudio(String name){
		One(name);
	}

	//To be used by music triggers
	public void ChangeTrack(String name){
		if (firstSong)
		{
			currentSongName = name;
			One(name, 1, true, 0);
			firstSong = false;
		}
		
		if(name.Equals(currentSongName) || currentSongName == null)
			return;
			
		nextSongName = name; 
		volumeFade = 1.0f;
	}

}