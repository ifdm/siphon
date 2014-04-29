using UnityEngine;
using System.Collections;

public class MenuToggle : MonoBehaviour {

	public bool activated = true;
	public string prefName;
	public Sprite onSprite;
	public Sprite offSprite;

	void Start() {
		if(prefName != "") {
			activated = PlayerPrefs.GetInt(prefName, activated ? 1 : 0) > 0;
		}

		if(activated) {
			GetComponent<SpriteRenderer>().sprite = onSprite;
		}
		else {
			GetComponent<SpriteRenderer>().sprite = offSprite;
		}
	}

	void MenuMouseClick() {
		activated = !activated;
		
		if(activated) {
			GetComponent<SpriteRenderer>().sprite = onSprite;
		}
		else {
			GetComponent<SpriteRenderer>().sprite = offSprite;
		}

		SendMessage("MenuToggleState", activated);

		if(prefName != "") {
			PlayerPrefs.SetInt(prefName, activated ? 1 : 0);
			PlayerPrefs.Save();
		}
	}
}
