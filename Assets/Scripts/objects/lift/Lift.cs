using UnityEngine;
using System.Collections;

public class Lift : MonoBehaviour {

	public bool loop = true;
	public bool playerEnabled = false;
	public bool breaks = false;
	public float distance = 5;
	public float speed = 2.5f;

	[HideInInspector] public float cachedSpeed;
	[HideInInspector] public bool running = false;
	private bool broken = false;
	private bool up = false;
	private Vector3 center;
	private Vector3 start;
	private Vector3 end;

	void Start () {
		start = transform.position;
		end = transform.position + Vector3.up * distance;
		cachedSpeed = speed;
		speed = (!playerEnabled) ? speed : 0f;
		up = (transform.position.y > end.y) ? true : false;
	}

	void Update() {
		if(!broken) rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, speed);

		var pivot = (up) ? (transform.position.y < end.y && speed < 0 || transform.position.y > start.y && speed > 0)
			: (transform.position.y > end.y && speed > 0 || transform.position.y < start.y && speed < 0);

		if(pivot && loop) {
			speed *= -1;
		}
		else if(pivot) {
			speed = 0;
			if(breaks) {
				broken = true;
				rigidbody2D.isKinematic = false;
			}
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.green;
		if(end == Vector3.zero) {
			Gizmos.DrawLine(transform.position, transform.position + Vector3.up * distance);
		}
		else {
			Gizmos.DrawLine(start, end);
		}
	}
}
