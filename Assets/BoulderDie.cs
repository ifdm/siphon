using UnityEngine;
using System.Collections;

public class BoulderDie : MonoBehaviour {

	private float health = 10f;
		
	void Update () {
		health -= Time.deltaTime;
		if(health <= 0){Destroy(gameObject);}
	}
}
