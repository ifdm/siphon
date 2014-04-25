using UnityEngine;
using System.Collections;

public class WaterRobotAI : MonoBehaviour {

	//Obviously does not actually change direction facing; I just mean in terms of direction MOVING

	public float speed;
	public float lookAhead;
	public int pauseTime;
	public Transform leftWaypoint;
	public Transform rightWaypoint;
	public Transform stopWaypoint;

	private int direction = 1;
	private int pause = 0;
	private bool left = true;
	private bool stopping = false;
	private bool stopped = false;
	private Interactable interactable;
	private EntityAudio audio;

	// Use this for initialization
	void Start () {
		interactable = GetComponentInChildren<Interactable>();
		audio = GetComponent<EntityAudio>();
		audio.One("WaterRobot_Idle", 1f, true);
		audio.One("WaterRobot_Move", 1f, true);

	}

	// Update is called once per frame
	void Update () {
		if(stopped) return;

		float x = transform.position.x;

		if(x > stopWaypoint.position.x && !stopped) {
			stopping = true;
			return;
		}
		if(interactable.moved)
			return;
		if(stopping){
			rigidbody2D.mass = 10000000f;
			interactable.dynamicWeight = 100000000f;
			audio.One("WaterRobot_Stopping");
			audio.Stop("WaterRobot_Move");
			audio.Stop("WaterRobot_Idle");
			return;
		}
		if(pause > 0){
			pause--;
			if(pause == 0) {
				Vector3 theScale = transform.localScale;
				theScale.x *= -1;
				transform.localScale = theScale;
				direction *= -1;
				audio.One("WaterRobot_Move", 1f, true);
			}
			return;
		}


		if(x < leftWaypoint.position.x && !left) {
			left = !left;
			pause = pauseTime;
			audio.Stop("WaterRobot_Move");
		}
		else if(x > rightWaypoint.position.x && left) {
			left = !left;
			pause = pauseTime;
			audio.Stop("WaterRobot_Move");
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
			audio.One("WaterRobot_Kill");
		}
	}
}
