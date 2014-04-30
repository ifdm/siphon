using UnityEngine;
using System.Collections;

public class LiquidSeep : MonoBehaviour {

	private float y;

	void Start() {
		y = transform.position.y;
	}


	void Update() {
		if(transform.position.y < y + 15) {
			transform.position += Vector3.up * Time.deltaTime * .22f;
		}
	}
}
