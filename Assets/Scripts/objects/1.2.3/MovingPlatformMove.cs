using UnityEngine;
using System.Collections;

public class MovingPlatformMove : MonoBehaviour {

	public float targetY;

	void Update() {
		float y = transform.localPosition.y;
		y += Mathf.Min(Mathf.Abs(y - targetY), .1f) * (Time.deltaTime * 25) * Mathf.Sign(targetY - y);
		transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
	}
}
