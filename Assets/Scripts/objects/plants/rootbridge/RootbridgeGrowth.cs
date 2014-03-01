using UnityEngine;
using System.Collections;

public class RootbridgeGrowth : Plant {

	public override void grow(RaycastHit2D cast) {
		Vector3 extents = transform.Find("body").gameObject.GetComponent<BoxCollider2D>().size * .5f;
		extents = Vector2.Scale(extents, transform.lossyScale);

		Transform animation = transform.Find("Animation");
		if(cast.normal.x < 0) {
			transform.position = new Vector3(transform.position.x - extents.x, transform.position.y, transform.position.z);
			animation.rotation = new Quaternion(0, -180, 0, 1);
		}
		else {
			transform.position = new Vector3(transform.position.x + extents.x, transform.position.y, transform.position.z);
			animation.position = new Vector3(animation.position.x + extents.x - .85f, animation.position.y, animation.position.z);
			animation.rotation = new Quaternion(0, 0, 0, 1);
		}

	}
	
	public override bool canPlant(RaycastHit2D cast) {
		if(!cast){return false;}
		
		Vector3 extents = transform.Find("body").gameObject.GetComponent<BoxCollider2D>().size * .5f;
		extents = Vector2.Scale(extents, transform.lossyScale);
		
		Vector3 p1 = cast.point;
		Vector3 p2 = cast.point;
		
		p1.x -= .1f;
		p2.x += .1f;
		
		p1.y += extents.y + .1f;
		p2.y += extents.y + .1f;
		
		Debug.DrawLine(p1, p2, Color.blue);
		
		if(!Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer("Ground"))) {
			return false;
		}
		
		p1.y -= extents.y + .2f;
		p2.y -= extents.y + .2f;
		
		Debug.DrawLine(p1, p2, Color.blue);
		
		if(!Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer("Ground"))) {
			return false;
		}
		
		return true;
	}
}
