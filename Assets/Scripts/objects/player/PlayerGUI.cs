using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerGUI : MonoBehaviour {

	public GUIStyle style;
	[HideInInspector] public PlayerThrow script;

	private Rect[] buttons;
	private Rect[] activeButtons;

	void Start () {
		script = GameObject.Find("Player").GetComponent<PlayerThrow>();
		buttons = new Rect[script.slots.Length];
		activeButtons = new Rect[script.slots.Length];

		int padding = 10;
		int width = 100;
		int height = 35;
		int x = 10;
		int y = Screen.height - height - padding;

		for(int key = 0; key < script.slots.Length; key++) {
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
		for(int key = 0; key < script.slots.Length; key++) {
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
