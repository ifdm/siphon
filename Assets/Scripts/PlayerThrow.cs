using UnityEngine;
using System.Collections;

public class PlayerThrow : MonoBehaviour {

	public GameObject seed;
	public Camera camera;
	
	public GameObject[] slots;
	
	private int activeSlot;
	
	void Update () {
		if(Input.GetMouseButtonDown(0)) {
			GameObject thrownSeed = (GameObject)Instantiate(this.seed, transform.position, Quaternion.identity);
			Vector2 p = camera.ScreenToWorldPoint(Input.mousePosition);
			Vector2 v = (p - new Vector2(transform.position.x, transform.position.y));
			thrownSeed.GetComponent<SeedThrow>().destiny = slots[activeSlot];
			thrownSeed.rigidbody2D.AddForce(v.normalized * 700);
		}
		
		for(int i = 0; i < slots.Length; i++) {
			if(Input.GetKeyDown((i + 1).ToString())) {
				activeSlot = i;
				break;
			}
		}
	}
}
