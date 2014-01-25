using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	
	public float smooth = 0.5f;
	[HideInInspector] public GameObject player;
	[HideInInspector] public float targetSize;
	[HideInInspector] public float defaultSize;
	[HideInInspector] public float sizeSmooth;
	[HideInInspector] public float defaultSizeSmooth;

	public float padding = 1;

	private float sizeVel = 0;
	private Vector3 posVel = Vector3.zero;

	void Start() {
		player = GameObject.Find("Player");
		float z = transform.position.z;
		transform.position = player.transform.position;
		transform.position = new Vector3(transform.position.x, transform.position.y, z);
		defaultSize = camera.orthographicSize;
		targetSize = defaultSize;
		sizeSmooth = 0.65f;
		defaultSizeSmooth = sizeSmooth;
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

		Vector3 target = p;
		float s = smooth;

		if(p.x - target.x > w){target.x = p.x - w;}
		else if(target.x - p.x > w){target.x = p.x + w;}
		else if(p.y - target.y > h){target.y = p.y - h;}
		else if(target.y - p.y > h){target.y = p.y + h;}

		if(Mathf.Abs(player.rigidbody2D.velocity.y) > 20) {
			float z = 0.3f;
			s = smooth * (1 - Mathf.Clamp((Mathf.Abs(player.rigidbody2D.velocity.y) - 20) / 20, 0.0f, 0.75f));
			camera.orthographicSize = Mathf.SmoothDamp(camera.orthographicSize, 10, ref sizeVel, z);
			sizeSmooth = 0.65f;
		}
		else {
			float z = sizeSmooth;
			camera.orthographicSize = Mathf.SmoothDamp(camera.orthographicSize, targetSize, ref sizeVel, z);
		}

		transform.position = Vector3.SmoothDamp(transform.position, target, ref posVel, s);
	}
}
