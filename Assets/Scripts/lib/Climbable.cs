using UnityEngine;
using System.Collections;

public class Climbable : MonoBehaviour {

	[HideInInspector] public Vector3 startPoint;
	[HideInInspector] public Vector3 endPoint;
	public int direction = 0;
}