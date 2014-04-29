using UnityEngine;
using System.Collections;

public class MenuOptionsQualitySlider : MonoBehaviour {

	public string quality;

	private bool focused;
	private int level;

	void Start() {
		for(int i = 0; i < QualitySettings.names.Length; i++) {
			if(QualitySettings.names[i] == quality) {
				level = i;
				if(QualitySettings.GetQualityLevel() == level) {
					MoveSliderPiece();
				}
				break;
			}
		}
	}

	void Update() {
		if(focused) {
			if(Input.GetMouseButton(0)) {
				MoveSliderPiece();
			}
			if(Input.GetMouseButtonUp(0)) {
				if(QualitySettings.GetQualityLevel() != level) {
					QualitySettings.SetQualityLevel(level);
					PlayerPrefs.SetInt("OptionsQuality", level);
				}
			}
		}
	}

	void MenuMouseFocus() {
		focused = true;
	}

	void MenuMouseBlur() {
		focused = false;
	}

	void MoveSliderPiece() {
		transform.parent.Find("SliderPiece").position = transform.position;
	}
}
