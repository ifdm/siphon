using UnityEngine;
using System.Collections;

public class MenuHoverSprite : MonoBehaviour {

	public Sprite changeTo;
	private Sprite original;

	void Start() {
		original = GetComponent<SpriteRenderer>().sprite;
	}
	
	void MenuMouseFocus() {
		GetComponent<SpriteRenderer>().sprite = changeTo;
	}

	void MenuMouseBlur() {
		GetComponent<SpriteRenderer>().sprite = original;
	}
}
