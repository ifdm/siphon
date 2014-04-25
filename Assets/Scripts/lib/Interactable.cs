using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Interactable : MonoBehaviour {

	public bool pull = true;
	public bool push = true;

	[HideInInspector] public bool moved = false;

	public float staticWeight = 100000;
	public float dynamicWeight = 5;
	public bool movePlayer;
	public float force = 50;
	public float forceOffsetY = 0f;
	public float offsetX = .2f;
	public AudioClip pushSound;


	private AudioSource audio;
	private Vector2 velocity;

	[HideInInspector] public bool pulling = false;
	[HideInInspector] public bool pushing = false;

	void Start() {
		if(rigidbody2D == null) {
			throw new Exception("Interactable requires a rigid body.");
		}
		else {
			rigidbody2D.mass = staticWeight;
		}
		audio = gameObject.AddComponent<AudioSource>();
		audio.clip = pushSound;
		audio.loop = true;
	}

	void Update() {
	}


	void OnTriggerStay2D(Collider2D col) {
	}

	void OnTriggerExit2D(Collider2D col) {
		if(col.gameObject.tag == "Player") {
			rigidbody2D.mass = staticWeight;
		}
	}
}