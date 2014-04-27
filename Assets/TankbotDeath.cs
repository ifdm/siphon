using UnityEngine;
using System.Collections;

public class TankbotDeath : MonoBehaviour {

	private bool ded = false;

	/*void Start() {
		ded = false;
	}*/

	void OnCollisionEnter2D(Collision2D col) {
		if(!ded && col.gameObject.name == "Tree") {
			var animator = transform.Find("Animation").GetComponent<TankbotAnimator>();
			animator.One("Die");
			ded = true;
			StartCoroutine(moreDeathStuff(col.gameObject));
		}
	}

	IEnumerator moreDeathStuff(GameObject tree) {
		Light light = transform.Find("Point light").gameObject.GetComponent<Light>();
		while(light.intensity > 0) {
			light.intensity -= Time.deltaTime;
			transform.localScale = transform.localScale + Vector3.up * -Time.deltaTime / 5;
			yield return null;
		}

		Destroy(transform.Find("Point light").gameObject);
		GameObject.Find("RobotSteam").GetComponent<ParticleSystem>().Stop();
		GameObject.Find("Dead Grass").SendMessage("GrowGrass");
	}
}
