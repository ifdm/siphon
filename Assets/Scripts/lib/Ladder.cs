using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour {

	void Start () {
		Climbable climbable = GetComponent<Climbable>();
		BoxCollider2D box = GetComponent<BoxCollider2D>();
		climbable.startPoint = new Vector3(transform.position.x, transform.position.y - box.size.y * transform.lossyScale.y / 2, transform.position.z);
		climbable.endPoint = new Vector3(transform.position.x, transform.position.y + box.size.y * transform.lossyScale.y / 2, transform.position.z);
	}
}
