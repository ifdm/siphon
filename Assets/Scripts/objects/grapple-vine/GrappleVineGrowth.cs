using UnityEngine;
using System.Collections;

public class GrappleVineGrowth : MonoBehaviour {
	
	private Vector3 startPoint;
	public GameObject GrappleVine;
	private float timer = 0.8f; // Use a coroutine.
	private GameObject child;

	public void Start() {
		startPoint = transform.position;
		transform.position = new Vector3(transform.position.x, transform.position.y - .3f, transform.position.z);
	}

	public void Update() {
		timer -= Mathf.Min(timer, Time.deltaTime);
		if(timer == 0 && !child) {
			transform.position = new Vector3(transform.position.x, transform.position.y + (24 * Time.deltaTime), transform.position.z);
			if(Physics2D.Linecast(transform.position, transform.position + (1.5f * Vector3.up), 1 << LayerMask.NameToLayer("Ground"))) {
				child = (GameObject)Instantiate(GrappleVine, startPoint, Quaternion.identity);
			}
		}
	}

	public void OnDestroy() {
		if(child){Destroy(child);}
	}
}
