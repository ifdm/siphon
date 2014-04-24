using UnityEngine;
using System.Collections;

public class MenuHover : MonoBehaviour {

	private bool hovering = false;

	void Start() {
		
	}
	
	void Update() {
		Collider2D col;
		Vector3 mouse = Input.mousePosition;
		mouse.z = -Camera.main.transform.position.z;
		if(col = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(mouse))) {
			if(col.gameObject == gameObject) {
				if(Input.GetMouseButtonDown(0)) {
					SendMessage("MenuMouseClick");
					return;
				}
				else {
					if(!hovering) {
						SendMessage("MenuMouseFocus");
					}

					hovering = true;
					return;
				}
			}
		}

		if(hovering) {
			SendMessage("MenuMouseBlur");
		}

		hovering = false;
	}
}
