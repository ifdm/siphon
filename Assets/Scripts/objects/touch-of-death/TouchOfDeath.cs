using UnityEngine;
using System.Collections;

public class TouchOfDeath : MonoBehaviour {

	public enum DeathAnimation {
		frontHit,
		backHit,
		spikeDeath,
		fallDeath
	};

	public TouchOfDeath.DeathAnimation animation = TouchOfDeath.DeathAnimation.frontHit;
	
	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "Player") {
			PlayerControl control = col.gameObject.GetComponent<PlayerControl>();
			if(control.state != PlayerState.Dying) {
				Destroy(col.gameObject.GetComponent<Rigidbody2D>());
				Destroy(col.gameObject.GetComponent<EdgeCollider2D>());
				Destroy(col.gameObject.GetComponent<BoxCollider2D>());
				Destroy(col.gameObject.GetComponent<CircleCollider2D>());
				control.ChangeState(PlayerState.Dying);
				switch(animation) {
					case TouchOfDeath.DeathAnimation.frontHit:
						control.animator.Set("FrontHit");
					break;

					case TouchOfDeath.DeathAnimation.backHit:
						control.animator.Set("BackHit");
					break;

					case TouchOfDeath.DeathAnimation.spikeDeath:
						control.animator.Set("SpikeDeath");
					break;

					case TouchOfDeath.DeathAnimation.fallDeath:
						control.animator.Set("FallDeath");
					break;
				}
			}
		}
	}
}
