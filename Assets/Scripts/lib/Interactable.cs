using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Interactable : MonoBehaviour {

	public bool pull = true;
	public bool push = true;

	public float staticWeight = 100000;
	public float dynamicWeight = 5;
	private bool inRange = false;
	private Vector2 velocity;
	private PlayerControl player;

	[HideInInspector] public bool pulling = false;
	[HideInInspector] public bool pushing = false;

	void Start() {
		player = GameObject.Find("Player").GetComponent<PlayerControl>();

		if(rigidbody2D == null) {
			throw new Exception("Interactable requires a rigid body.");
		}
		else {
			rigidbody2D.mass = staticWeight;
		}
	}

	void Update() {}
	void OnTriggerStay2D(Collider2D col) {}
	void OnTriggerExit2D(Collider2D col) {
		if(col.gameObject.tag == "Player") {
			inRange = false;
			rigidbody2D.mass = staticWeight;
		}
	}
}