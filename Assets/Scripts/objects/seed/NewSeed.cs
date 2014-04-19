using UnityEngine;
using System.Collections;

public class NewSeed : MonoBehaviour {

	public float alpha;

	void Start () {
		transform.position = new Vector3(0, 0, 1);
		alpha = 0;
	}
	
	void Update () {
		Color color = renderer.material.color;
		color.a = alpha;
		renderer.material.color = color;
		alpha = Mathf.Lerp(alpha, 1, 5 * Time.deltaTime / 2);
		GameObject camera = GameObject.Find("Main Camera");
		float x = Mathf.Abs(camera.transform.position.z) * Mathf.Tan(Mathf.Deg2Rad * camera.GetComponent<Camera>().fieldOfView / 2);
		transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - x / 2, transform.position.y, transform.position.z), Time.deltaTime);
	}
}
