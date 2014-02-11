using UnityEngine;
using System.Collections;

public class StaticRootBridge : MonoBehaviour {
	
	public float delay = 1;

	public void Start() {
		transform.Find("Rootbridge").Find("Animation").gameObject.GetComponent<Animator>().speed = 0;
	}

	public void OnTriggerEnter2D(Collider2D col) {
		if(col.tag == "Player") {
			StartCoroutine(delayGrowth());
		}
	}
	
	IEnumerator delayGrowth() {
		yield return new WaitForSeconds(delay);
		transform.Find("Rootbridge").Find("Animation").gameObject.GetComponent<Animator>().speed = 1;
	}
}
