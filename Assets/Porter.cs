using UnityEngine;
using System.Collections;

public class Porter : MonoBehaviour {

	public GameObject next;
	public GameObject previous;
	public float delay = 1f;
	// Next is 1, previous is 0
	[HideInInspector] public int direction = 1;
	private Porter nextScript;
	private Porter previousScript;
	private bool destination = false;
	private float timer = 0;
	private float gracePeriod = 1f;

	void Start() {
		nextScript = (next) ? next.GetComponent<Porter>() : null;
		previousScript = (previous) ? previous.GetComponent<Porter>() : null;
	}
	
	void Update() {
		if((Time.time - timer) > (gracePeriod + delay)) {
			destination = false;
			timer = Time.time;
			Debug.Log(destination);
		}
	}

	public void Port(GameObject other) {
		if(!destination) {
			if(direction == 1) {
				if(next) {
					other.transform.position = next.transform.position;
					nextScript.destination = true;
					direction = nextScript.direction = 0;
				}
			}
			else if(direction == 0) {
				if(previous) {
					other.transform.position = previous.transform.position;
					previousScript.destination = true;
					direction = previousScript.direction = 1;
				}
			}
		}
	}
}
