using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour {

	private bool flying = false;
	private float direction;
	private float speed;
	private float fadeDelay;
	private float moveDelay;

	void Update() {
		if(flying) {
			moveDelay -= Mathf.Min(moveDelay, Time.deltaTime);
			if(moveDelay <= 0) {
				Vector2 v = new Vector2(Mathf.Cos(direction), Mathf.Sin(direction)) * speed;
				transform.position += (Vector3)v * Time.deltaTime;
				direction += Random.Range(-15, 15) * Time.deltaTime;

				fadeDelay -= Mathf.Min(fadeDelay, Time.deltaTime);
				if(fadeDelay <= 0) {
					Color c = renderer.material.color;
					renderer.material.color = Color.Lerp(c, new Color(c.r, c.g, c.b, 0), 5 * Time.deltaTime);
					if(renderer.material.color.a < .001) {
						Destroy(gameObject);
					}
				}
			}
		}
		else {
			if(Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) < 8) {
				flying = true;
				direction = Random.Range(Mathf.PI / 4, 3 * Mathf.PI / 4);
				fadeDelay = Random.Range(.75f, 1.25f);
				moveDelay = Random.Range(.1f, .8f);
				speed = Random.Range(9.5f, 15);
			}
		}
	}
}