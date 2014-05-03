using UnityEngine;
using System.Collections;

public class GVCut : MonoBehaviour {

	void Hooked() {
		transform.parent.parent.Find("Vines").GetComponent<Animator>().SetTrigger("Kill");
		transform.parent.parent.Find("Long Vines").GetComponent<Animator>().SetTrigger("Kill");
		Destroy(transform.parent.gameObject);
	}
}
