using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerThrow : MonoBehaviour {

	public GameObject seed;
	public Camera camera;
	
	public GameObject[] slots;
	private List<Queue> slotQueues;
	
	private int activeSlot;
	
	void Start() {
		AdjustSlotQueues();
	}

	public void AdjustSlotQueues() {
		slotQueues = new List<Queue>();
		for(int i = 0; i < slots.Length; i++) {
			slotQueues.Add(new Queue());
		}
	}
	
	void Update () {
		if(Input.GetMouseButtonDown(0)) {
			GameObject thrownSeed = (GameObject)Instantiate(this.seed, transform.position, Quaternion.identity);
			Vector2 p = camera.ScreenToWorldPoint(Input.mousePosition);
			Vector2 v = (p - new Vector2(transform.position.x, transform.position.y));
			thrownSeed.GetComponent<SeedThrow>().destiny = slots[activeSlot];
			while(slotQueues[activeSlot].Count > 0) {
				Destroy((GameObject)slotQueues[activeSlot].Dequeue());
			}
			thrownSeed.GetComponent<SeedThrow>().destiny = slots[activeSlot];
			thrownSeed.GetComponent<SeedThrow>().queue = slotQueues[activeSlot];
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
