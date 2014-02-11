using UnityEngine;
using System;
using System.Collections;

public class Plant : MonoBehaviour {

	public virtual bool canPlant(RaycastHit2D cast) {
		return true;
	}
	
	public virtual void grow(RaycastHit2D cast) {
		return;
	}
}