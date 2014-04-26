using UnityEngine;
using System.Collections;

public class CrushBoulder : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.name == "Rolling Boulder") {
			StartCoroutine(Crush());
			col.gameObject.GetComponent<EntityAudio>().One("robotCrash");
		}
	}

	IEnumerator Crush() {
		var animation = transform.Find("Animation");

		while(transform.localScale.y > .4f) {
			transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - Time.deltaTime / 3, transform.localScale.z);
			transform.position = new Vector3(transform.position.x, transform.position.y - (1.2f * transform.localScale.y * Time.deltaTime), transform.position.z);
			animation.renderer.material.color = Color.Lerp(Color.white, Color.gray, (.75f - transform.localScale.y) / .35f);
			yield return new WaitForSeconds(0);
		}

		if(transform.localScale.y <= .4f) {
			animation.GetComponent<Animator>().speed = 0.0f;
			transform.Find("Steam_A").GetComponent<ParticleSystem>().Stop();
			transform.Find("Steam_B").GetComponent<ParticleSystem>().Stop();
			transform.Find("Steam_C").GetComponent<ParticleSystem>().Stop();
		}
	}
}
