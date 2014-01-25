using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerGUI : MonoBehaviour {

	public GUIStyle style;
	[HideInInspector] public PlayerThrow script;

	private Rect[] buttons;
	private Rect[] activeButtons;
	private int length;

	void Start () {
		script = GameObject.Find("Player").GetComponent<PlayerThrow>();
		length = script.slots.Length;

		Build();
	}

	void Build() {
		length = script.slots.Length;
		buttons = new Rect[length];
		activeButtons = new Rect[length];

		int padding = 10;
		int width = 100;
		int height = 35;
		int x = 10;
		int y = Screen.height - height - padding;

		for(int key = 0; key < length; key++) {
			activeButtons[key] = new Rect(x, y, width, height);
			buttons[key] = new Rect(x, y, width, height);

			x += width + padding;
		}
	}

	void Update() {		
		Vector2 mousePos = new Vector2(Input.mousePosition.x,Screen.height - Input.mousePosition.y);
		foreach(var button in buttons) {
			if(button.Contains(mousePos)) {
				script.throwable = false;
				break;
			}
			else {
				script.throwable = true;
			}
		}
	}
	
	void OnGUI() {
		if(script.slots.Length != length) Build();

		for(int key = 0; key < length; key++) {
			if(key == script.activeSlot) {
				GUI.Button(activeButtons[key], script.slots[key].name + "!");
			}
			else {
				if(GUI.Button(buttons[key], script.slots[key].name)) {
					script.activeSlot = key;
				}
			}
		}
	}
}
