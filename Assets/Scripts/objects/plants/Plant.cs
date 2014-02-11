using UnityEngine;
using System;
using System.Collections;

public class Plant : MonoBehaviour {

	public virtual Vector3 PlantPosition(Vector3 target) {
		return target;
	}
}