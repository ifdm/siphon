using UnityEngine;
using System.Collections;
using System.Reflection;

public class Menu : MonoBehaviour {

	public string page = "Main";
	public Texture2D cursor;

	private string prevPage = "";

	void Start() {
		StartCoroutine(SetCursor());
		Transform pages = transform.Find("Pages");
		for(int i = 0; i < pages.childCount; i++) {
			pages.GetChild(i).gameObject.SetActive(false);
		}
		MenuChangePage(page);

		if(PlayerPrefs.GetInt("OptionsSound", 1) == 0) {
			AudioListener.pause = true;
		}

		if(PlayerPrefs.HasKey("OptionsQuality")) {
			QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("OptionsQuality"));
			Debug.Log(QualitySettings.names[QualitySettings.GetQualityLevel()]);
		}
	}
	
	void MenuChangePage(string newPage) {
		GetComponent<AudioSource> ().Play ();
		BroadcastMessage("MenuMouseBlur", null, SendMessageOptions.DontRequireReceiver);
		if(page != "") {
			transform.Find("Pages").Find(page).gameObject.SetActive(false);
		}
		if(newPage == "Previous") {
			newPage = prevPage;
		}
		prevPage = page;
		page = newPage;
		if(page != "") {
			transform.Find("Pages").Find(page).gameObject.SetActive(true);
		}
	}

	IEnumerator SetCursor() {
		yield return new WaitForSeconds(.1f);
		Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
	}
}