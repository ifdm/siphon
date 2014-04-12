using UnityEngine;
using System.Collections;

public class BridgeBreak : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D col) {
		string name = col.gameObject.name;
		if(name == "Board1" || name == "Board2" || name == "Board3") {

			Destroy(col.gameObject.GetComponent<HingeJoint2D>());
			GameObject board3 = GameObject.Find("Board3"); // Always destroy board3 and boardedgeL so player can proceed.
			if(board3) {
				for(int i = 0; i < 2; i++) {
					Destroy(board3.GetComponent<HingeJoint2D>());
				}
			}

			StartCoroutine(killBridge());
		}
	}

	IEnumerator killBridge() {
		yield return new WaitForSeconds(.1f);
		GameObject.Find("Board1").GetComponent<BoxCollider2D>().isTrigger = true;
		GameObject.Find("Board2").GetComponent<BoxCollider2D>().isTrigger = true;
		GameObject.Find("Board3").GetComponent<BoxCollider2D>().isTrigger = true;
		GameObject.Find("BoardEdgeL").GetComponent<BoxCollider2D>().isTrigger = true;
		GameObject.Find("BoardEdgeL").GetComponent<CircleCollider2D>().isTrigger = true;
		GameObject.Find("BoardEdgeR").GetComponent<BoxCollider2D>().isTrigger = true;
		GameObject.Find("BoardEdgeR").GetComponent<CircleCollider2D>().isTrigger = true;

		GameObject.Find("Branch").GetComponent<BoxCollider2D>().isTrigger = true;

	}
}
