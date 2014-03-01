using UnityEngine;
using System.Collections;

public class SeedThrow : MonoBehaviour {
	
	public GameObject destiny;
	public Queue queue;

	private float health = 4.0f;
	
	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.tag == "Ground") {
			EntityAudio audio = GetComponent<EntityAudio>();
			audio.One("plant");
			if(col.gameObject.GetComponent<Unplantable>() == null && (col.transform.parent == null || col.transform.parent.gameObject.GetComponent<Unplantable>() == null)) {
				if(destiny.name == "Mushroom" || destiny.name == "GrapplingVine") { // Yea yea..I know..
					RaycastHit2D hit = Physics2D.Linecast(transform.position, transform.position - (Vector3.up * .5f), 1 << LayerMask.NameToLayer("Ground"));
					if(hit) {
						queue.Enqueue(Instantiate(destiny, transform.position, Quaternion.identity));
						DestroyObject(gameObject);
					}
				}
				else {
					queue.Enqueue(Instantiate(destiny, transform.position, Quaternion.identity));
					DestroyObject(gameObject);
				}
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
