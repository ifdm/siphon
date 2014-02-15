using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MusicTrigger : MonoBehaviour {

	private GameObject player;
	private CameraAudio audio;
	public string track;

	void Start(){
		player = GameObject.Find ("Player");
		audio = Camera.main.GetComponent<CameraAudio> ();
	}


	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.Equals (player)) 
		{
			audio.ChangeTrack(track);
		}
	}

}

