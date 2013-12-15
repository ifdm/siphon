using UnityEngine;
using System.Collections;

public class GrappleVine : MonoBehaviour {

	private Vector3 endPoint;

	// Use this for initialization
	void Start () {
		Vector3 up = transform.position;
		up.y += 1000;
		RaycastHit2D cast = Physics2D.Linecast(transform.position, up, 1 << LayerMask.NameToLayer("Ground"));
		if(cast) {
			endPoint = cast.point;
		}

		Debug.Log(endPoint);
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawLine(transform.position, endPoint, Color.red);
	}
}