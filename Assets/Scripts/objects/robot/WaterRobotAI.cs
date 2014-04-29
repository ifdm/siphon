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
	public float maxOffsetX = 10;

	private bool sucking = false;
	private int direction = 1;
	private int pause = 0;
	private bool left = true;
	private bool stopping = false;
	private bool stopped = false;
	private Interactable interactable;
	private EntityAudio audio;
	private WaterRobotAnimator animator;

	// Use this for initialization
	void Start () {
		animator = transform.Find("Animateur").GetComponent<WaterRobotAnimator>();
		interactable = GetComponentInChildren<Interactable>();
		audio = GetComponent<EntityAudio>();
		audio.GetAudioSource("WaterRobot_Idle").minDistance = 20f;
		audio.GetAudioSource("WaterRobot_Idle").maxDistance = 60f;
		audio.GetAudioSource("WaterRobot_Idle").rolloffMode = AudioRolloffMode.Linear;
		audio.GetAudioSource("WaterRobot_Move").minDistance = 20f;
		audio.GetAudioSource("WaterRobot_Move").maxDistance = 60f;
		audio.GetAudioSource("WaterRobot_Move").rolloffMode = AudioRolloffMode.Linear;
		audio.GetAudioSource("WaterRobot_Kill").minDistance = 20f;
		audio.GetAudioSource("WaterRobot_Kill").maxDistance = 60f;
		audio.GetAudioSource("WaterRobot_Kill").rolloffMode = AudioRolloffMode.Linear;
		audio.One("WaterRobot_Idle", 0.05f, true);
		audio.One("WaterRobot_Move", 0.05f, true);
	}

	// Update is called once per frame
	void Update () {
		if(stopped) return;

		float x = transform.position.x;

		if(x > rightWaypoint.position.x + maxOffsetX) {
			transform.position = Vector3.Lerp(transform.position, new Vector3(rightWaypoint.position.x + maxOffsetX, transform.position.y, transform.position.z), 4 * Time.deltaTime);
		}

		x = transform.position.x;

		if(x > stopWaypoint.position.x && !stopped) {
			stopping = true;
			return;
		}
		if(interactable.moved)
			return;
		if(stopping){
			rigidbody2D.mass = 10000000f;
			interactable.dynamicWeight = 100000000f;
			audio.Stop("WaterRobot_Move");
			audio.Stop("WaterRobot_Idle");
			return;
		}
		if(pause > 0){
			// Animation toggle
			if(pause == pauseTime) {
				if(sucking) {
					animator.Set("Dump", true);
					sucking = false;
				}
				else {
					animator.Set("Suck", true);
					sucking = true;
				}
			}

			pause--;
			if(pause == 0) {
				Vector3 theScale = transform.localScale;
				theScale.x *= -1;
				transform.localScale = theScale;
				direction *= -1;
				audio.One("WaterRobot_Move", 0.05f, true);
				animator.Set("Travel", true);
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
		rigidbody2D.velocity = new Vector2(x,y);
	}

	void OnCollisionEnter2D(Collision2D coll){
		if ((coll.gameObject.tag == "Player")
			&&(transform.position.x  - coll.transform.position.x) < -.5 && this.direction == 1) {

			PlayerControl player = coll.gameObject.GetComponent<PlayerControl>();
			player.ChangeState(PlayerState.Dying);
			audio.One("WaterRobot_Kill", 0.05f);
		}
	}

	void OnDrawGizmos() {
		Debug.DrawLine(rightWaypoint.position + Vector3.right * maxOffsetX - Vector3.up, rightWaypoint.position + Vector3.right * maxOffsetX + Vector3.up, Color.red);
	}
}
