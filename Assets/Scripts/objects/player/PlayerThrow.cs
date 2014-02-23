using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class PlayerThrow : MonoBehaviour {

	public GameObject seed;
	[HideInInspector] public Camera mainCamera;

	private CursorBehavior throwCursor;
	
	public Plant[] slots;
	private List<Queue> slotQueues;

	public float throwForce = 700f;
	public float topDeadZone = 15f;
	public float bottomDeadZone = 15f;

	public Texture2D[] cursors;

	private int curSeed;

	[HideInInspector] public int activeSlot;
	[HideInInspector] public bool throwable = true;
	
	private RaycastHit2D target;
	
	
	void Start() {
		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		throwCursor = GameObject.Find("Throw Cursor").GetComponent<CursorBehavior>();

		slotQueues = new List<Queue>();
		for(int i = 0; i < slots.Length; i++) {
			slotQueues.Add(new Queue());
		}
	}

	private IEnumerator SetInitialCursor() {
		yield return new WaitForSeconds(.1f);
		if(cursors.Length > 0) {
			Screen.showCursor = true;

			Cursor.SetCursor(cursors[0], Vector2.zero, CursorMode.Auto);
			curSeed = 0;

			//Cursor.SetCursor(cursors[0], Vector2.zero, CursorMode.Auto);
		}
	}

	public void AddSlotQueue() {
		slotQueues.Add(new Queue());
	}
	
	void Update () {
		
		if(slots.Length > 0) {
			bool targeting = Input.GetAxis("Seed Horizontal") != 0 || Input.GetAxis("Seed Vertical") != 0 || Input.GetMouseButton(0);
			bool throwing = Input.GetAxis("Seed Throw") != 0 || Input.GetMouseButtonUp(0);
			
			if(targeting && !throwing) {
				Vector3 playerPos = transform.position;
				BoxCollider2D box = GetComponent<BoxCollider2D>();
				playerPos.x += box.center.x * transform.lossyScale.x;
				playerPos.y += box.center.y * transform.lossyScale.y;
				
				Vector2 dir;
				if(Input.GetMouseButton(0)) {
					Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
					Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, 0));
					float distance;
					xy.Raycast(ray, out distance);
					dir = ray.GetPoint(distance) - playerPos;
				}
				else {
					dir = new Vector2(Input.GetAxis("Seed Horizontal"), Input.GetAxis("Seed Vertical"));
				}
				
				target = Physics2D.Raycast(playerPos, dir, 20, 1 << LayerMask.NameToLayer("Ground"));
				
				Vector3 end = playerPos + (Vector3)dir * 20;
				if(target) {
					end = target.point;
				}
				throwCursor.isCursorDrawing = true;
				throwCursor.raycast = target;
				throwCursor.plant = slots[activeSlot];
				throwCursor.position = end;
				Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
				
				Debug.DrawLine(playerPos, end, slots[activeSlot].canPlant(target) ? Color.green : Color.red);
			}
			else{
				throwCursor.isCursorDrawing = false;

				Cursor.SetCursor(cursors[curSeed], Vector2.zero, CursorMode.Auto);
			}
			
			
			if(throwing && slots[activeSlot].canPlant(target)) {
				StartCoroutine(delayThrow(activeSlot, target, .15f));
				
				GetComponent<PlayerControl>().animator.Set("Throw", false, 1);
				
				target = default(RaycastHit2D);
			}
			
			selectSeed();
		}
	}
	
	IEnumerator delayThrow(int slot, RaycastHit2D target, float delay) {
		yield return new WaitForSeconds(delay);
		
		while(slotQueues[slot].Count > 0) {
			Plant old = (Plant)slotQueues[slot].Dequeue();
			Destroy(old.gameObject);
		}
		Plant plant = (Plant)Instantiate(slots[slot], target.point, Quaternion.identity);
		slotQueues[slot].Enqueue(plant);
		plant.grow(target);
	}
	
	private void selectSeed() {
		for(int i = 0; i < slots.Length; i++) {
			if(Input.GetKeyDown((i + 1).ToString())) {
				activeSlot = i;
				
				Cursor.SetCursor(cursors[i], Vector2.zero, CursorMode.Auto);
				curSeed = i;

				//Cursor.SetCursor(cursors[i], Vector2.zero, CursorMode.Auto);

				break;
			}
		}

		if(Input.GetAxis("Mouse ScrollWheel") > 0) {
			activeSlot = (activeSlot + 1) % slots.Length;
			Cursor.SetCursor(cursors[activeSlot], Vector2.zero, CursorMode.Auto);
			curSeed = activeSlot;
			}
		else if(Input.GetAxis("Mouse ScrollWheel") < 0) {
			activeSlot--;
			if(activeSlot < 0){activeSlot = slots.Length - 1;}
			Cursor.SetCursor(cursors[activeSlot], Vector2.zero, CursorMode.Auto);
			curSeed = activeSlot;
			//Cursor.SetCursor(cursors[activeSlot], Vector2.zero, CursorMode.Auto);
		}
	
	}
}
