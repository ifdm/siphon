using UnityEngine;
using System.Collections;

public class CheckpointController : MonoBehaviour {
	
	[HideInInspector] public Vector3 position;
	[HideInInspector] public Plant[] slots;
	private static CheckpointController instance = null;

	void Awake() {
		if(instance) {
			RaycastHit2D ground = Physics2D.Linecast(instance.position, (Vector3.up * -100) + instance.position, (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("One-Way Ground")));
			if(ground) instance.position = ground.point;

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
