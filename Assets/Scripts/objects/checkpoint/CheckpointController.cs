using UnityEngine;
using System.Collections;

public class CheckpointController : MonoBehaviour {
	
	[HideInInspector] public Vector3 position;
	[HideInInspector] public Plant[] slots;
	public static CheckpointController instance = null;

	void Awake() {
		if(instance) {
			GameObject.Find("Player").transform.position = instance.position;
			if(instance.slots != null) {
				GameObject.Find("Player").GetComponent<PlayerThrow>().slots = instance.slots;
			}
			DestroyImmediate(gameObject);
		}
		else {
			DontDestroyOnLoad(transform.gameObject);
			instance = this;
		}
	}
}
