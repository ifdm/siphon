using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


//Triggers ambient audio in the gameworld
public class AudioTrigger : Mozart {
	
	private GameObject player;
	
	public bool repeats;
	public float volume;
	public string track;
	
	public bool allowOverlap = false;
	public float minDelayBetweenPlayback = 0.25f;
	
	private float coolDown = 0.0f;
	
	public override void Awake() {
		clips = new Dictionary<string, AudioClip>() {
			{track, Resources.Load<AudioClip>(track)}
		};
	}
	
	void Start(){
		player = GameObject.Find ("Player");
		base.Start();
	}
	
	void OnUpdate(){
		coolDown = (coolDown <= 0.0f) ? 0.0f : coolDown - Time.deltaTime * 0.1f;
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		
		if (col.gameObject.Equals (GameObject.Find ("Player")) && coolDown <= 0.0f) 
		{
			if(allowOverlap)
			{
				Play(track, volume, repeats, 0);
			}
			else
			{
				One(track, volume, repeats, 0);
			}
			coolDown = minDelayBetweenPlayback;
		}
	}
	
}

