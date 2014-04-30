using UnityEngine;
using System.Collections;

public class StartLiquid : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.name == "Player") {
			transform.Find("Liquid").gameObject.AddComponent("LiquidSeep");
		}
	}
}
