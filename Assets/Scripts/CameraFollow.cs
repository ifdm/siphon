using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	
	public float smooth = 16;
	public GameObject player;

	void Update() {
		Vector3 target = Vector3.Lerp(camera.ScreenToWorldPoint(Input.mousePosition), player.transform.position, .75f);
		target.z = transform.position.z;
		
		transform.position = Vector3.Lerp(transform.position, target, Mathf.Clamp(smooth * Time.deltaTime, 0.0f, 1.0f));
	}
}
