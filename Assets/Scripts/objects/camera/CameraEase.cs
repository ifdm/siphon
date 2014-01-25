using UnityEngine;
using System.Collections;

public class CameraEase : MonoBehaviour {

	public float size = 5;
	public float smooth = 3.0f;

	private CameraFollow cameraFollow;

	public void Start() {
		cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
	}

	public void OnTriggerStay2D(Collider2D col) {
		if(col.tag == "Player") {
			cameraFollow.targetSize = size;
			cameraFollow.sizeSmooth = smooth;
		}
	}

	public void OnTriggerExit2D(Collider2D col) {
		if(col.tag == "Player") {
			cameraFollow.targetSize = cameraFollow.defaultSize;
		}
	}
}
