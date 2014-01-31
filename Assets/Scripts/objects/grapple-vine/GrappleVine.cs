using UnityEngine;
using System.Collections;

public class GrappleVine : MonoBehaviour {

	public float maxDistance = 9;
	[HideInInspector] public Vector3 startPoint;
	[HideInInspector] public Vector3 endPoint;

	void Start() {
		RaycastHit2D hit = Physics2D.Linecast(transform.position, transform.position - (Vector3.up * 0.5f), 1 << LayerMask.NameToLayer("Ground"));
		if(hit){transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);}
		//else{Destroy(gameObject);}
		
		startPoint = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
		Vector3 up = startPoint;
		up.y += 100;
		RaycastHit2D cast = Physics2D.Linecast(startPoint, up, 1 << LayerMask.NameToLayer("Ground"));
		endPoint = (Vector3)cast.point;
		
		if(!cast){DestroyObject(gameObject);}
		
		Climbable climbable = GetComponent<Climbable>();
		climbable.startPoint = startPoint;
		climbable.endPoint = endPoint;

		if(Vector3.Distance(climbable.startPoint, climbable.endPoint) > maxDistance) {
			startPoint = (endPoint - (Vector3.up * maxDistance));
			climbable.startPoint = startPoint;
		}

		Transform animation = transform.Find("Animation");
		animation.position = new Vector3(animation.position.x, endPoint.y - 4.4f, animation.position.z);
		animation.localScale = new Vector2(animation.localScale.x, 0.5f);
	}

	void Update() {
		//Debug.DrawLine(startPoint, endPoint, Color.red);
	}
}