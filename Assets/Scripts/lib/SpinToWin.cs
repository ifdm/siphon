using UnityEngine;
using System.Collections;

public class SpinToWin : MonoBehaviour {
	public float speed = 1f;
	
	void Update () {
		transform.Rotate(new Vector3(0, 0, speed * Time.deltaTime));
	}
}
