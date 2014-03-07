using UnityEngine;
using System.Collections;

public class WaterRobotAI : MonoBehaviour {

	//Obviously does not actually change direction facing; I just mean in terms of direction MOVING

	public float speed;
	public float lookAhead;

	private float targetX;
	private int direction = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//TODO Has to do a raytrace to check for missing ground beneath it
		//to avoid walking off cliffs
		Vector3 pos = transform.position;

		Vector2 l1 = new Vector2 (pos.x - this.lookAhead, pos.y);
		Vector2 l2 = new Vector2 (pos.x - this.lookAhead, pos.y - 10);
		RaycastHit2D left = Physics2D.Linecast(l1, l2, (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("One-Way Ground")));

		Vector2 r1 = new Vector2 (pos.x + this.lookAhead, pos.y);
		Vector2 r2 = new Vector2 (pos.x + this.lookAhead, pos.y - 10);
		RaycastHit2D right = Physics2D.Linecast(r1, r2, (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("One-Way Ground")));

		if (!left || !right) {
			this.direction *= -1;		
		}

		pos.x += (this.direction * this.speed);
		transform.position = pos;

	}

	void OnCollisionEnter2D(Collision2D coll){
		if (!(coll.gameObject.tag == "Player")) {
			//turn around when we hit something
			this.direction = this.direction * -1;
			Debug.Log("direction: " + this.direction);
		}
	}
}
