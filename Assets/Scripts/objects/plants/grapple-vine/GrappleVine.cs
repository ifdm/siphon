using UnityEngine;
using System.Collections;

public class GrappleVine : MonoBehaviour {

	public float maxDistance = 9;
	[HideInInspector] public Vector3 startPoint;
	[HideInInspector] public Vector3 endPoint;

	void Start() {
		startPoint = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
		Vector3 up = startPoint;
		up.y += 100;
		RaycastHit2D cast = Physics2D.Linecast(startPoint, up, 1 << LayerMask.NameToLayer("Ground"));
		endPoint = (Vector3)cast.point;
		
		if(!cast){DestroyObject(gameObject);}
		if(!cast.collider) return;

		GameObject platform = cast.collider.gameObject;
		if(platform.GetComponent<Unplantable>() || (platform.transform.parent && platform.transform.parent.gameObject.GetComponent<Unplantable>())){DestroyObject(gameObject);}
		
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
		Climbable climbable = GetComponent<Climbable>();
		Debug.DrawLine(climbable.startPoint, climbable.endPoint, Color.blue);
	}
}