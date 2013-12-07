using UnityEngine;
using System.Collections;

public class LineGizmo : MonoBehaviour {
	void OnDrawGizmos () {
		Gizmos.color = Color.cyan;
		var collider = (EdgeCollider2D)collider2D;
		for(var i = 0; i < collider.pointCount - 1; i++) {
			Gizmos.DrawLine (transform.position + (Vector3)collider.points[i], transform.position + (Vector3)collider.points[i + 1]);
		}
	}
}
