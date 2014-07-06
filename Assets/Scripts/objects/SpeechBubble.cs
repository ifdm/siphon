using UnityEngine;
using System.Collections;

public class SpeechBubble : MonoBehaviour {

	public string text = "";
	public Texture2D background;
	private GameObject player;
	private Camera cam;
	private GUIStyle style;
	private bool touched = false;
	private Color color;
	private float delay = 2;

	void Start () {
		player = GameObject.Find("Player");
		cam = GameObject.Find("Main Camera").camera;

		style = new GUIStyle();
		style.normal.textColor = Color.white;
		style.normal.background = background;
		style.alignment = TextAnchor.MiddleCenter;
		style.wordWrap = true;

		color = Color.white;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject == player && !touched) {
			touched = true;
			StartCoroutine(Fade());
		}
	}
	
	void OnGUI() {
		if(touched && color.a > 0) {
			GUI.color = color;
			GUI.backgroundColor = new Color(0.0f, 0.0f, 0.0f, color.a * .6f);
			Vector3 pos = cam.WorldToScreenPoint(player.transform.position);
			pos.y = Screen.height - pos.y;
			pos.x = pos.x - 50;
			pos.y = pos.y - 120;
			GUI.Label(new Rect(pos.x, pos.y, 100, 40), text, style);
		}
	}

	IEnumerator Fade() {
		yield return new WaitForSeconds(delay);
		while(color.a > 0) {
			color = new Color(color.r, color.g, color.b, Mathf.Max(color.a - Time.deltaTime, 0));
			yield return null;
		}
	}
}
