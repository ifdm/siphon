using UnityEngine;
using System.Collections;

public class RopeLink : MonoBehaviour {

	void Start () {
		Climbable climbable = GetComponent<Climbable>();
		climbable.startPoint = new Vector3(transform.position.x, transform.position.y - .5f, transform.position.z);
		climbable.endPoint = new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z);
	}
}
