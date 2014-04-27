using UnityEngine;
using System.Collections;

public class GVCut : MonoBehaviour {

	public void Hooked() {
		transform.parent.Find("Vines").GetComponent<Animator>().SetTrigger("Kill");
		transform.parent.Find("Long Vines").GetComponent<Animator>().SetTrigger("Kill");
		Destroy(gameObject);
	}

}
