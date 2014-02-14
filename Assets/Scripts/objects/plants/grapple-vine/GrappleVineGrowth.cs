using UnityEngine;
using System.Collections;

public class GrappleVineGrowth : Plant {
	
	private Vector3 startPoint;
	public GameObject GrappleVine;
	private float timer = 0.8f; // Use a coroutine.
	private GameObject child;

	public void Start() {
		startPoint = transform.position;
		transform.position = new Vector3(transform.position.x, transform.position.y - .3f, transform.position.z);
	}

	public void Update() {
		timer -= Mathf.Min(timer, Time.deltaTime);
		if(timer == 0 && !child) {
			transform.position = new Vector3(transform.position.x, transform.position.y + (24 * Time.deltaTime), transform.position.z);
			if(Physics2D.Linecast(transform.position, transform.position + (1.5f * Vector3.up), 1 << LayerMask.NameToLayer("Ground"))) {
				child = (GameObject)Instantiate(GrappleVine, startPoint, Quaternion.identity);
			}
		}
	}

	public void OnDestroy() {
		if(child){Destroy(child);}
	}
	
	public override bool canPlant(RaycastHit2D cast) {
		if(!cast){return false;}
		
		Vector3 p1 = cast.point;
		Vector3 p2 = cast.point;
		
		p1.y -= .01f;
		p2.y -= .01f;
		
		p1.y += .1f;
		p2.y -= .2f;
		
		p1.x -= .1f;
		p2.x -= .1f;
		Debug.DrawLine(p1, p2, Color.blue);
		cast = Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer("Ground"));
		if(!cast || cast.fraction == 0) {
			return false;
		}
		
		p1.x += .1f;
		p2.x += .1f;
		Debug.DrawLine(p1, p2, Color.blue);
		cast = Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer("Ground"));
		if(!cast || cast.fraction == 0) {
			return false;
		}
		
		p1.x += .1f;
		p2.x += .1f;
		Debug.DrawLine(p1, p2, Color.blue);
		cast = Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer("Ground"));
		if(!cast || cast.fraction == 0) {
			return false;
		}
		
		return true;
	}
}
