using UnityEngine;
using System.Collections;

public class BoulderThrow : MonoBehaviour {

	private float timer;

	public float boulderRate = 7;
	public GameObject boulder;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(timer >= boulderRate) {
			timer = 0;
			var instance = (GameObject)Instantiate(boulder);
			instance.transform.position = transform.position;
			instance.rigidbody2D.AddForce(new Vector2(-10, 20));
		}
	}
}
