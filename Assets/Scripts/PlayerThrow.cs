using UnityEngine;
using System.Collections;

public class PlayerThrow : MonoBehaviour {

	public GameObject seed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)) {
			// Throw seed
			Instantiate(this.seed, transform.position, Quaternion.identity);
		}
	}
}
