using UnityEngine;
using System.Collections;

public class CameraPull : MonoBehaviour {

	public float size = 5;
	public float smooth = 3.0f;
	public float duration = 1.0f;

	private float x, y, w, h;
	private Transform player;
	private bool pulling = false;

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
		if(!pulling) {
			CameraFollow camera = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
			if(player.position.x > x && player.position.y > y && player.position.x < x + w && player.position.y < y + h) {
				camera.additionalZ = size;
				camera.zSmooth = smooth;
				Vector3 v = transform.Find("PullTo").position;
				camera.pullTo = new Vector3(v.x, v.y, camera.transform.position.z);
				camera.pullSmooth = smooth;
				pulling = true;
				pulling = true;
				StartCoroutine(stopPulling());
			}
		}
		else {
			CameraZoom.dirty = true;
		}
	}


	IEnumerator stopPulling() {
		yield return new WaitForSeconds(smooth * 2 + duration);
		CameraFollow camera = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
		camera.pullTo = Vector3.zero;
		Destroy(gameObject);
	}
}
