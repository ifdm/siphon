using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {

	public float size = 5;
	public float smooth = 3.0f;

	private float x, y, w, h;
	private Transform player;

	[HideInInspector] public static bool dirty;

	public void Start() {
		size -= 5; // Legacy support ew

		BoxCollider2D box = GetComponent<BoxCollider2D>();
		w = box.size.x * transform.lossyScale.x;
		h = box.size.y * transform.lossyScale.y;
		x = transform.position.x - box.size.x * transform.lossyScale.x / 2;
		y = transform.position.y - box.size.y * transform.lossyScale.y / 2;

		player = GameObject.Find("Player").transform;
		y -= .5f;
	}

	public void Update() {
		CameraFollow camera = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
		if(player.position.x > x && player.position.y > y && player.position.x < x + w && player.position.y < y + h) {
			camera.additionalZ = size;
			camera.zSmooth = smooth;
			CameraZoom.dirty = true;
		}
		else if(!CameraZoom.dirty) {
			camera.additionalZ = 0;
		}
	}
}
