using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	
	public float smooth = 0.5f;
	[HideInInspector] public GameObject player;

	public float padding = 1;

	private float sizeVel = 0;
	private Vector3 vel = Vector3.zero;

	private bool fastBall = false;

	void Start() {
		player = GameObject.Find("Player");
		float z = transform.position.z;
		transform.position = player.transform.position;
		transform.position = new Vector3(transform.position.x, transform.position.y, z);
	}

	void OnPreRender() {
		Vector3 my = transform.position;
		Vector3 mouse = camera.ScreenToWorldPoint(Input.mousePosition);
		Vector2 p2d = (Vector2)player.transform.position;
		Vector2 scale = (Vector2)player.transform.lossyScale;
		BoxCollider2D box = player.GetComponent<BoxCollider2D>();
		p2d += Vector2.Scale(box.center, scale);
		Vector3 p = new Vector3(p2d.x, p2d.y, my.z);
		mouse.z = my.z;
		
		float h = camera.orthographicSize - padding;
		float w = (camera.orthographicSize * camera.aspect) - padding;

		Vector3 target = transform.position;
		PlayerState state = player.GetComponent<PlayerControl>().state;

		float f = Mathf.Pow(Mathf.Clamp(Vector3.Distance(p, mouse) / h, 0, 1), 2);

		if(p.x - mouse.x > w){mouse.x = p.x - w;}
		else if(mouse.x - p.x > w){mouse.x = p.x + w;}
		else if(p.y - mouse.y > h){mouse.y = p.y - h;}
		else if(mouse.y - p.y > h){mouse.y = p.y + h;}

		float s = smooth;
		if(false && f > 0.5 && player.GetComponent<PlayerControl>().state == PlayerState.Idling) {
			f -= 0.5f;
			target = p + ((mouse - p) * f);
		}
		else {
			target = p;
		}

		if(p.x - target.x > w){target.x = p.x - w;}
		else if(target.x - p.x > w){target.x = p.x + w;}
		else if(p.y - target.y > h){target.y = p.y - h;}
		else if(target.y - p.y > h){target.y = p.y + h;}

		if(Mathf.Abs(player.rigidbody2D.velocity.y) > 20 || Input.GetKey(KeyCode.LeftShift)) {
			float z = 0.5f;
			fastBall = false;
			if(Input.GetKey(KeyCode.LeftShift)){z = 0.1f; fastBall = true;}
			camera.orthographicSize = Mathf.SmoothDamp(camera.orthographicSize, 10, ref sizeVel, z);
		}
		else {
			float z = 0.5f;
			if(fastBall){z = 0.1f;}
			camera.orthographicSize = Mathf.SmoothDamp(camera.orthographicSize, 5, ref sizeVel, z);
		}

		Debug.Log(s);
		transform.position = Vector3.SmoothDamp(transform.position, target, ref vel, s);
	}
}
