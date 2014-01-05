using UnityEngine;
using System.Collections;

public class FlyPath : MonoBehaviour {

	public Vector2 velocity = new Vector2(-2, 0);
	private bool porting = false;
	private bool entered = false;
	
	void FixedUpdate() {
		rigidbody2D.velocity = velocity;
	}
}
