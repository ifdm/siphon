using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerThrow : MonoBehaviour {

	public GameObject seed;
	[HideInInspector] public Camera mainCamera;
	
	public GameObject[] slots;
	private List<Queue> slotQueues;

	public float throwForce = 700f;
	public float topDeadZone = 15f;
	public float bottomDeadZone = 15f;

	public Texture2D[] cursors;

	[HideInInspector] public int activeSlot;
	[HideInInspector] public bool throwable = true;

	void Start() {
		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		
		slotQueues = new List<Queue>();
		for(int i = 0; i < slots.Length; i++) {
			slotQueues.Add(new Queue());
		}

		StartCoroutine(SetInitialCursor());
	}

	private IEnumerator SetInitialCursor() {
		yield return new WaitForSeconds(.1f);
		if(cursors.Length > 0) {
			//Screen.showCursor = true;
			//Cursor.SetCursor(cursors[0], Vector2.zero, CursorMode.Auto);
		}
	}

	public void AddSlotQueue() {
		slotQueues.Add(new Queue());
	}
	
	void Update () {
		PlayerControl player = gameObject.GetComponent<PlayerControl>();
		PlayerPhysics playerPhysics = gameObject.GetComponent<PlayerPhysics>();

		if(CanThrow(player)) {
			Vector3 playerPos = transform.position;
			BoxCollider2D box = GetComponent<BoxCollider2D>();
			playerPos.x += box.center.x * transform.lossyScale.x;
			playerPos.y += box.center.y * transform.lossyScale.y;

			GameObject thrownSeed = (GameObject)Instantiate(this.seed, playerPos, Quaternion.identity);
			thrownSeed.name = "Seed";

			Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
			Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, 0));
			float distance;
			xy.Raycast(ray, out distance);
			Vector2 p = ray.GetPoint(distance);
			Vector2 v = (p - new Vector2(transform.position.x, transform.position.y));

			float angle = (float)Mathf.Atan2 (v.x, v.y);
			angle = angle * Mathf.Rad2Deg;

			//Adjust player rotation
			if(playerPhysics.facingRight && angle < 0 || !playerPhysics.facingRight && angle > 0)
				playerPhysics.ChangeDirection();

			float top = 0;
			float bottom = 180;
			float tDangle = Mathf.Abs(Mathf.DeltaAngle (angle, top));
			float bDangle = Mathf.Abs(Mathf.DeltaAngle (angle, bottom));

			//If outside of the bounds above and below, simply throw straight down
			if(tDangle < topDeadZone || bDangle < bottomDeadZone)
			{
				v = new Vector2(0, 0);
			}

			thrownSeed.GetComponent<SeedThrow>().destiny = slots[activeSlot];
			while(slotQueues[activeSlot].Count > 0) {
				Destroy((GameObject)slotQueues[activeSlot].Dequeue());
			}

			thrownSeed.GetComponent<SeedThrow>().destiny = slots[activeSlot];
			thrownSeed.GetComponent<SeedThrow>().queue = slotQueues[activeSlot];
			thrownSeed.rigidbody2D.AddForce(v.normalized * throwForce);
			player.animator.Set("Throw", false, 1);
		}
		
		for(int i = 0; i < slots.Length; i++) {
			if(Input.GetKeyDown((i + 1).ToString())) {
				activeSlot = i;
				//Cursor.SetCursor(cursors[i], Vector2.zero, CursorMode.Auto);
				break;
			}
		}

		if(Input.GetAxis("Mouse ScrollWheel") > 0) {
			activeSlot = (activeSlot + 1) % slots.Length;
			//Cursor.SetCursor(cursors[activeSlot], Vector2.zero, CursorMode.Auto);
		}
		else if(Input.GetAxis("Mouse ScrollWheel") < 0) {
			activeSlot--;
			if(activeSlot < 0){activeSlot = slots.Length - 1;}
			//Cursor.SetCursor(cursors[activeSlot], Vector2.zero, CursorMode.Auto);
		}
	}


	//Checks if the player can throw
	private bool CanThrow(PlayerControl player){
		return Input.GetMouseButtonDown (0) && 
			player.state != PlayerState.Ledging && 
				throwable && 
				!GameObject.Find ("Seed") && 
				slots.Length > 0;
	}
}
