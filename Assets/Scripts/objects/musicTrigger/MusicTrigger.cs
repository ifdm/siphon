using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MusicTrigger : MonoBehaviour {

	private GameObject player;
	private CameraAudio cameraAudio;
	public string track;

	void Start(){
		player = GameObject.Find ("Player");
		cameraAudio = Camera.main.GetComponent<CameraAudio> ();
	}


	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.Equals (player)) 
		{
			cameraAudio.ChangeTrack(track);
		}
	}

}

