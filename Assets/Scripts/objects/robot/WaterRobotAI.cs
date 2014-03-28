using UnityEngine;
using System.Collections;

public class WaterRobotAI : MonoBehaviour {

	//Obviously does not actually change direction facing; I just mean in terms of direction MOVING

	public float speed;
	public float lookAhead;
	public int pauseTime;
	public Transform leftWaypoint;
	public Transform rightWaypoint;

	private int direction = 1;
	private int pause = 0;
	private bool left = true;
	private Interactable interactable;

	// Use this for initialization
	void Start () {
		interactable = GetComponentInChildren<Interactable>();
	}
	
	// Update is called once per frame
	void Update () {
		//TODO Has to do a raytrace to check for missing ground beneath it
		//to avoid walking off cliffs
		if(interactable.moved)
			return;
		if(pause > 0){
			pause--;
			return;
		}
	
		float x = transform.position.x;
		if(x < leftWaypoint.position.x && !left)
		{
			left = !left;
			pause = pauseTime;
			direction = 1;
			Debug.Log("changed direction " + left);
		}
		else if(x > rightWaypoint.position.x && left){
			left = !left;
			pause = pauseTime;
			direction = -1;
			Debug.Log("changed direction " + left);
		}

		rigidbody2D.AddForce(Vector2.right * this.direction * 100f);
		x = direction * Mathf.Min (speed, Mathf.Abs(rigidbody2D.velocity.x));
		float y = rigidbody2D.velocity.y;
		//Debug.Log (x + " " + rigidbody2D.velocity.x + " " + speed + " " + direction);
		rigidbody2D.velocity = new Vector2(x,y);

	}

	void OnCollisionEnter2D(Collision2D coll){
		if ((coll.gameObject.tag == "Player")
			&&(transform.position.x  - coll.transform.position.x) < -.5 && this.direction == 1) {

			PlayerControl player = coll.gameObject.GetComponent<PlayerControl>();
			player.ChangeState(PlayerState.Dying);
		}
	}
}
