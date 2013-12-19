﻿using UnityEngine;
using System.Collections;

public class MushroomBounce : MonoBehaviour {

	void OnCollisionEnter2D (Collision2D col) {
		bool flag = false;
		foreach (ContactPoint2D contact in col.contacts) {
			if(contact.collider.name.Equals("Player") && contact.normal.y < 0){
				flag = true;
			}
		}
		if (flag) {
			col.collider.SendMessage("MushroomBounceEvent");
		}
	}
}