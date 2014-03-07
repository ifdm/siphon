using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour {

	void Start () {
		Climbable climbable = GetComponent<Climbable>();
		BoxCollider2D box = GetComponent<BoxCollider2D>();
		climbable.startPoint = new Vector3(transform.position.x + (box.center.x * transform.lossyScale.x), transform.position.y + ((box.center.y - (box.size.y / 2)) * transform.lossyScale.y), transform.position.z);
		climbable.endPoint = new Vector3(transform.position.x + (box.center.x * transform.lossyScale.x), transform.position.y + ((box.center.y + (box.size.y / 2)) * transform.lossyScale.y), transform.position.z);
	}
}