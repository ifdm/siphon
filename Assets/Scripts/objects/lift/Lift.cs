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
	private Vector3 center;
	private Vector3 start;
	private Vector3 end;

	void Start () {
		start = transform.position;
		end = transform.position + Vector3.up * distance;
		cachedSpeed = speed;
		speed = (!playerEnabled) ? speed : 0f;
	}

	void Update() {
		rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, speed);
		if(
			(transform.position.y < end.y && speed < 0
			|| transform.position.y > start.y && speed > 0)
			&& loop
		) {
			speed *= -1;
		}
		else if(
			(transform.position.y < end.y && speed < 0
			|| transform.position.y > start.y && speed > 0)
		) {
			speed = 0;
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
