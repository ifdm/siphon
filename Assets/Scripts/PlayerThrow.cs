using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerThrow : MonoBehaviour {

	public GameObject seed;
	[HideInInspector] public Camera mainCamera;
	
	public GameObject[] slots;
	private List<Queue> slotQueues;
	
	[HideInInspector] public int activeSlot;
	[HideInInspector] public bool throwable = true;
	
	void Start() {
		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		
		AdjustSlotQueues();
	}

	public void AdjustSlotQueues() {
		slotQueues = new List<Queue>();
		for(int i = 0; i < slots.Length; i++) {
			slotQueues.Add(new Queue());
		}
	}
	
	void Update () {
		if(Input.GetMouseButtonDown(0) && throwable && !GameObject.Find("Seed") && slots.Length > 0) {
			Vector3 playerPos = transform.position;
			BoxCollider2D box = GetComponent<BoxCollider2D>();
			playerPos.x += box.center.x * transform.lossyScale.x;
			playerPos.y += box.center.y * transform.lossyScale.y;

			GameObject thrownSeed = (GameObject)Instantiate(this.seed, playerPos, Quaternion.identity);
			thrownSeed.name = "Seed";
			Vector2 p = mainCamera.ScreenToWorldPoint(Input.mousePosition);
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
