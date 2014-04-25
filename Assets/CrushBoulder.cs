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
		while(transform.localScale.y > .4) {
			transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - Time.deltaTime / 3, transform.localScale.z);
			transform.position = new Vector3(transform.position.x, transform.position.y - (1.2f * transform.localScale.y * Time.deltaTime), transform.position.z);
			renderer.material.color = Color.Lerp(Color.white, Color.gray, (.75f - transform.localScale.y) / .35f);
			yield return new WaitForSeconds(0);
		}
	}
}
