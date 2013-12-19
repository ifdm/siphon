using UnityEngine;
using System.Collections;

public class GrappleVine : MonoBehaviour {

	private Vector3 startPoint;
	private Vector3 endPoint;
	private bool dirty;

	// Use this for initialization
	void Start () {
		startPoint = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
		Vector3 up = transform.position;
		up.y += 1000;
		RaycastHit2D cast = Physics2D.Linecast(transform.position, up, 1 << LayerMask.NameToLayer("Ground"));
		if(cast){endPoint = cast.point;}
		dirty = false;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawLine(startPoint, endPoint, Color.red);
		RaycastHit2D cast = Physics2D.Linecast(startPoint, endPoint, 1 << LayerMask.NameToLayer("Player"));
		if(cast) {
			PlayerMovement player = cast.rigidbody.gameObject.GetComponent("PlayerMovement") as PlayerMovement;
			if(!dirty) {
				player.state = PlayerMovement.PlayerState.CLIMBING;
				dirty = true;
			}
			else if(player.rigidbody2D.velocity.y < 0){dirty = false;}
		}
		else{dirty = false;}
	}
}