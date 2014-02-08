using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Hydroconveyor : MonoBehaviour {
	private Transform pointA;
	private Transform pointB;
	private static float radius = 5;
	public int numBuckets = 14;
	public int ticksBetweenBuckets = 60;
	private int tickNum = 0;

	private List<Quaternion> rotations = new List<Quaternion> ();
	private List<Vector2> points = new List<Vector2> ();
	private List<GameObject> buckets = new List<GameObject> ();

	// Use this for initialization
	void Start() {

		Transform[] childTransforms = new Transform[2];
		int i = 0;
		foreach(Transform child in transform) {
			childTransforms[i] = child;
			i++;
		}


		pointA = childTransforms [0];
		pointB = childTransforms [1];

		Vector2 diffPos = (pointA.position - pointB.position);
		float angle = Mathf.Atan2 (diffPos.x, diffPos.y);


		DoPoints(Mathf.PI, pointA.position, angle);
		DoPoints(0, pointB.position, angle);

		//DoBuckets();

	}

	private void DoPoints(float d0, Vector3 p0, float angle) {

		float step = Mathf.PI / 50;
		for(float delta = d0; delta < (d0 + Mathf.PI); delta += step) {
			Vector3 point = p0 + new Vector3 (radius * Mathf.Cos (0 - angle - delta), radius * Mathf.Sin (0 - angle - delta));

			Quaternion rotation = Quaternion.identity;
			rotation.eulerAngles = new Vector3(0, 0, 180 - delta * Mathf.Rad2Deg);

			points.Add(point);
			rotations.Add(rotation);
		}
	}
	
	void Update() {
		tickNum++;
		if(buckets.Count < numBuckets && tickNum >= ticksBetweenBuckets) {
			//var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			//cube.transform.position = points;
		}
	}
}
