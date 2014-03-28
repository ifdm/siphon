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
	}

	void Update() {}
	void OnTriggerStay2D(Collider2D col) {}
	void OnTriggerExit2D(Collider2D col) {
		if(col.gameObject.tag == "Player") {
			rigidbody2D.mass = staticWeight;
		}
	}
}