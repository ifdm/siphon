using UnityEngine;
using System.Collections;

public class Float : MonoBehaviour {

	float t;
	float c = .1f;
	Vector2 orig;

	// Use this for initialization
	void Start () {
		orig = new Vector2(transform.position.x, transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
		float x = orig.x + Mathf.Cos(t) * c;
		float y = orig.y + Mathf.Sin(t) * c;

		transform.position = new Vector3(x, y, transform.position.z);
		t += Time.deltaTime * 2;
	}
}
