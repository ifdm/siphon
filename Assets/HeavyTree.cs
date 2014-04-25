using UnityEngine;
using System.Collections;

public class HeavyTree : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D co){
		EntityAudio audio = GetComponent<EntityAudio>();
		audio.One("Tree_Land_Heavy");
	}
}
