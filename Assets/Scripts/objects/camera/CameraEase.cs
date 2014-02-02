using UnityEngine;
using System.Collections;

public class CameraEase : MonoBehaviour {

	public float size = 5;
	public float smooth = 3.0f;

	private float x, y, w, h;
	private Transform player;
	private float zVel = 0;

	public void Start() {
		BoxCollider2D box = GetComponent<BoxCollider2D>();
		w = box.size.x * transform.lossyScale.x;
		h = box.size.y * transform.lossyScale.y;
		x = transform.position.x - box.size.x * transform.lossyScale.x / 2;
		y = transform.position.y - box.size.y * transform.lossyScale.y / 2;
		
		player = GameObject.Find("Player").transform;
	}

	public void Update() {
		if(player.position.x > x && player.position.y > y && player.position.x < x + w && player.position.y < y + h) {
			Transform camera = GameObject.Find("Main Camera").transform;
			float z = Mathf.SmoothDamp(camera.position.z, -10 - size, ref zVel, smooth);
			camera.position = new Vector3(camera.position.x, camera.position.y, z);
		}
		else {
			Transform camera = GameObject.Find("Main Camera").transform;
			float z = Mathf.SmoothDamp(camera.position.z, -10, ref zVel, smooth);
			camera.position = new Vector3(camera.position.x, camera.position.y, z);
		}
	}
}
