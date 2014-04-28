using UnityEngine;
using System.Collections;

public class ChangeGrass : MonoBehaviour {

	public Color colorEnd;

	private SpriteRenderer[] renderers;
	private bool growing;

	void Start() {
		renderers = GetComponentsInChildren<SpriteRenderer>();
	}

	void Update() {
		if(!growing) return;
		foreach(SpriteRenderer r in renderers) {
			r.color = new Color(Mathf.Min(r.color.r + Time.deltaTime / 5, 1.0f), Mathf.Min(r.color.g + Time.deltaTime / 5, 1.0f), Mathf.Min(r.color.b + Time.deltaTime / 5, 1.0f), 1);
		}
	}

	public void GrowGrass() {
		growing = true;
		transform.Find("Animation").GetComponent<Animator>().SetTrigger("Grow");
		StartCoroutine(GrowSeed(1.87f));
	}

	private IEnumerator GrowSeed(float delay) {
		yield return new WaitForSeconds(delay);
		GameObject plant = GameObject.Find("Floating Plant");
		plant.transform.Find("Animation").GetComponent<FloatingPlantAnimator>().Set("Grow");
	}

}
