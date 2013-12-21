using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	
	public float smooth = 16;
	public GameObject player;
	public float mouseThreshold = 5;

	void Update() {
		Vector3 mouse = camera.ScreenToWorldPoint(Input.mousePosition);
		Vector3 p = player.transform.position;
		mouse.z = 0;
		p.z = 0;
		
		Vector3 target;
		if(Mathf.Abs(mouse.x - p.x) > mouseThreshold || Mathf.Abs(mouse.y - p.y) > mouseThreshold){target = Vector3.Lerp(mouse, p, .5f);}
		else{target = p;}
		
		target.z = transform.position.z;
		transform.position = Vector3.Lerp(transform.position, target, Mathf.Clamp(smooth * Time.deltaTime, 0.0f, 1.0f));
	}
}
