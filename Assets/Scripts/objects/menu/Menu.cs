using UnityEngine;
using System.Collections;
using System.Reflection;

public class Menu : MonoBehaviour {

	public string page = "Main";
	public Texture2D cursor;

	void Start() {
		StartCoroutine(SetCursor());
		MenuChangePage(page);
	}
	
	void MenuChangePage(string newPage) {
		transform.Find(page).gameObject.SetActive(false);
		page = newPage;
		transform.Find(page).gameObject.SetActive(true);
	}

	IEnumerator SetCursor() {
		yield return new WaitForSeconds(.1f);
		Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
	}
}