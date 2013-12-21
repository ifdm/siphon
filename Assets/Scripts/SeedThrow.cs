using UnityEngine;
using System.Collections;

public class SeedThrow : MonoBehaviour {
	
	public GameObject destiny;
	public Queue queue;
	
	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.tag == "Ground") {
			DestroyObject(gameObject);
			queue.Enqueue(Instantiate(destiny, transform.position, Quaternion.identity));
		}
		else{Debug.Log(col.gameObject.tag);}
	}
}
