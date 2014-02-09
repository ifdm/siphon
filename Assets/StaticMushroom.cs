using UnityEngine;
using System.Collections;

public class StaticMushroom : MonoBehaviour {
	
	public float delay = 1;

	public void Start() {
		transform.Find("Mushroom").gameObject.GetComponent<MushroomAnimator>().TimeScale = 0;
	}

	public void OnTriggerEnter2D(Collider2D col) {
		if(col.tag == "Player") {
			StartCoroutine(delayGrowth());
		}
	}
	
	IEnumerator delayGrowth() {
		yield return new WaitForSeconds(delay);
		transform.Find("Mushroom").gameObject.GetComponent<MushroomAnimator>().TimeScale = 1;
	}
}
