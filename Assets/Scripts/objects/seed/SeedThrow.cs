using UnityEngine;
using System.Collections;

public class SeedThrow : MonoBehaviour {
	
	public GameObject destiny;
	public Queue queue;
	
	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.tag == "Ground") {
			if(col.gameObject.GetComponent<Unplantable>() == null && (col.transform.parent == null || col.transform.parent.gameObject.GetComponent<Unplantable>() == null)) {
				queue.Enqueue(Instantiate(destiny, transform.position, Quaternion.identity));
			}
			
			DestroyObject(gameObject);
		}
	}
}
