using UnityEngine;
using System.Collections;

public class GrappleVine : MonoBehaviour {

	[HideInInspector] public Vector3 startPoint;
	[HideInInspector] public Vector3 endPoint;

	void Start () {
		startPoint = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
		Vector3 up = startPoint;
		up.y += 100;
		RaycastHit2D cast = Physics2D.Linecast(startPoint, up, 1 << LayerMask.NameToLayer("Ground"));
		endPoint = (Vector3)cast.point;
		
		if(!cast){DestroyObject(gameObject);}
		
		Climbable climbable = GetComponent<Climbable>();
		climbable.startPoint = startPoint;
		climbable.endPoint = endPoint;
	}

	void Update() {
		Debug.DrawLine(startPoint, endPoint, Color.red);
	}
}