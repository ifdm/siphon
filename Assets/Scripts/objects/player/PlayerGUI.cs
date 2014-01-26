using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerGUI : MonoBehaviour {

	public GUIStyle style;
	[HideInInspector] public PlayerThrow script;

	private float[] buttonAlphas;
	private float[] buttonScales;
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
		buttonAlphas = new float[length];
		buttonScales = new float[length];
		activeButtons = new Rect[length];

		int padding = 10;
		int width = 36;
		int height = 55;
		int x = 10;
		int y = 10;

		for(int key = 0; key < length; key++) {
			activeButtons[key] = new Rect(x, key % 2 == 0 ? y + 10 : y, width, height);
			buttons[key] = new Rect(x, key % 2 == 0 ? y + 10 : y, width, height);

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
			GUI.color = new Color(1, 1, 1, buttonAlphas[key]);

			if(key == script.activeSlot) {
				buttonAlphas[key] = Mathf.Lerp(buttonAlphas[key], 1.0f, 8 * Time.deltaTime);
				GUI.Box(activeButtons[key], script.slots[key].guiTexture.texture, style);
			}
			else {
				buttonAlphas[key] = Mathf.Lerp(buttonAlphas[key], 0.65f, 8 * Time.deltaTime);
				if(GUI.Button(buttons[key], script.slots[key].guiTexture.texture, style)) {
					script.activeSlot = key;
				}
			}
		}
	}
}
