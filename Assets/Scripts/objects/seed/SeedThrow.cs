using UnityEngine;
using System.Collections;

public class SeedThrow : MonoBehaviour {
	
	public GameObject destiny;
	public Queue queue;

	private float health = 4.0f;
	
	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "Ground") {
			if(col.gameObject.GetComponent<Unplantable>() == null && (col.transform.parent == null || col.transform.parent.gameObject.GetComponent<Unplantable>() == null)) {
				queue.Enqueue(Instantiate(destiny, transform.position, Quaternion.identity));
				DestroyObject(gameObject);
			}
		}
	}

	void Update() {
		health -= Mathf.Min(health, Time.deltaTime);
		if(health == 0) {
			Destroy(gameObject);
		}
	}
}
