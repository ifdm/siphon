using UnityEngine;
using System.Collections;

public class CameraFrame : MonoBehaviour {

	public float size = 5;
	public float smooth = 3.0f;

	private float x, y, w, h;
	private Transform player;

	private bool active = false;

	void Start () {
		BoxCollider2D box = GetComponent<BoxCollider2D>();
		w = box.size.x * transform.lossyScale.x;
		h = box.size.y * transform.lossyScale.y;
		x = transform.position.x - box.size.x * transform.lossyScale.x / 2;
		y = transform.position.y - box.size.y * transform.lossyScale.y / 2;

		player = GameObject.Find("Player").transform;
		y -= .5f;
	}
	
	// Update is called once per frame
	void Update () {
		CameraFollow camera = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
		if(player.position.x > x && player.position.y > y && player.position.x < x + w && player.position.y < y + h) {
			camera.zSmooth = smooth;
			Vector3 v = transform.Find("PullTo").position;
			camera.pullTo = new Vector3(v.x, v.y, camera.zStart - size);
			camera.pullSmooth = smooth;
			active = true;
		}
		else if(active) {
			camera.pullTo = Vector3.zero;
			camera.additionalZ = 0;
			active = false;
		}
	}
}
