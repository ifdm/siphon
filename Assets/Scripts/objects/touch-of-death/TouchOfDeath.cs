using UnityEngine;
using System.Collections;

public class TouchOfDeath : MonoBehaviour {

	public enum DeathAnimation {
		frontHit,
		backHit,
		spikeDeath,
		fallDeath,
		drownDeath
	};

	public bool fallThrough = false;
	public TouchOfDeath.DeathAnimation animation = TouchOfDeath.DeathAnimation.frontHit;

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "Player") {
			PlayerControl control = col.gameObject.GetComponent<PlayerControl>();
			if(control.state != PlayerState.Dying) {
				col.gameObject.GetComponent<Rigidbody2D>().isKinematic = !fallThrough;
				col.gameObject.GetComponent<EdgeCollider2D>().isTrigger = true;
				col.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
				col.gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
				col.gameObject.GetComponent<PlayerAudio>().Play("Death");
				control.ChangeState(PlayerState.Dying);
				GameObject cam = GameObject.Find("Main Camera");
				cam.GetComponent<CameraFollow>().pullTo = new Vector3(col.transform.position.x, col.transform.position.y, cam.transform.position.z);
				switch(animation) {
					case TouchOfDeath.DeathAnimation.frontHit:
						control.animator.Set("Front Hit");
					break;

					case TouchOfDeath.DeathAnimation.backHit:
						control.animator.Set("Back Hit");
					break;

					case TouchOfDeath.DeathAnimation.spikeDeath:
						control.animator.Set("Spike Death");
					break;

					case TouchOfDeath.DeathAnimation.fallDeath:
						control.animator.Set("Fall Death " + (Mathf.Ceil(Random.value * 3)));
					break;

					case TouchOfDeath.DeathAnimation.drownDeath:
						control.animator.Set("Drown Death");
					break;
				}
			}
		}
	}
}
