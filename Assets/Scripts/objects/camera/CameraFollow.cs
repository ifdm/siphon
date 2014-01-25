using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	
	public float smooth = 16;
	public float mouselookSmooth = 8;
	[HideInInspector] public GameObject player;
	public float padding = 1;
	public float mouselookDelay = .5f;
	public float mouselookForgive = .5f;
	[HideInInspector] public float mouseTimer = 0;

	private Vector3 vel = Vector3.zero;

	void Start() {
		player = GameObject.Find("Player");
		float z = transform.position.z;
		transform.position = player.transform.position;
		transform.position = new Vector3(transform.position.x, transform.position.y, z);
	}

	void Update() {
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
		if((Mathf.Abs(mouse.x - my.x) > w || Mathf.Abs(mouse.y - my.y) > h) && state == PlayerState.Idling){mouseTimer += Time.deltaTime;}
		else {
			mouseTimer = Mathf.Min(mouseTimer, mouselookDelay + mouselookForgive);
			mouseTimer = Mathf.Max(mouseTimer - Time.deltaTime, 0.0f);
		}
		if(true || state != PlayerState.Idling){mouseTimer = 0;}
		
		if(mouseTimer > mouselookDelay) {
			Vector3 m = mouse;
			if(p.x - m.x > w){m.x = p.x - w;}
			if(m.x - p.x > w){m.x = p.x + w;}
			if(p.y - m.y > h){m.y = p.y - h;}
			if(m.y - p.y > h){m.y = p.y + h;}
			target = m;
		}
		else {
			target = p;
		}

		if(p.x - target.x > w){target.x = p.x - w;}
		if(target.x - p.x > w){target.x = p.x + w;}
		if(p.y - target.y > h){target.y = p.y - h;}
		if(target.y - p.y > h){target.y = p.y + h;}

		transform.position = Vector3.SmoothDamp(transform.position, target, ref vel, 0.3f);
	}
}
