using UnityEngine;
using System.Collections;

public class BridgeBreak : MonoBehaviour {

	// Use this for initialization
	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.name == "Branch1" || col.gameObject.name == "Branch2" || col.gameObject.name == "Branch3" || col.gameObject.name == "Branch4") { // I have a CS degree
			Destroy(GetComponent<HingeJoint2D>());
			GameObject board3 = GameObject.Find("Board3"); // Always destroy board3 and boardedgeL so player can proceed.
			GameObject boardL = GameObject.Find("BoardEdgeL");
			if(board3) {
				for(int i = 0; i < 2; i++) {
					Destroy(board3.GetComponent<HingeJoint2D>());
				}
			}
			
			StartCoroutine(killBridge());
		}
	}
	
	IEnumerator killBridge() {
		yield return new WaitForSeconds(1);
		GameObject.Find("Board1").GetComponent<BoxCollider2D>().isTrigger = true;
		GameObject.Find("Board2").GetComponent<BoxCollider2D>().isTrigger = true;
		GameObject.Find("Board3").GetComponent<BoxCollider2D>().isTrigger = true;
		GameObject.Find("BoardEdgeL").GetComponent<BoxCollider2D>().isTrigger = true;
		GameObject.Find("BoardEdgeR").GetComponent<BoxCollider2D>().isTrigger = true;
	}
}
