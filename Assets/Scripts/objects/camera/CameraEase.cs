using UnityEngine;
using System.Collections;

public class CameraEase : MonoBehaviour {

	public float size = 5;
	public float smooth = 3.0f;

	private CameraFollow cameraFollow;
	private float distance;

	public void Start() {
		cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();

		BoxCollider2D box = GetComponent<BoxCollider2D>();
		distance = Mathf.Sqrt(Mathf.Pow(box.size.x * transform.lossyScale.x / 2, 2) + Mathf.Pow(box.size.y * transform.lossyScale.y / 2, 2));
	}

	public void Update() {
		if(Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) < distance) {
			cameraFollow.targetSize = size;
			cameraFollow.sizeSmooth = smooth;
		}
		else {
			cameraFollow.targetSize = cameraFollow.defaultSize;
		}
	}
}
