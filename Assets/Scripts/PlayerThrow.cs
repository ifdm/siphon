using UnityEngine;
using System.Collections;

public class PlayerThrow : MonoBehaviour {

	public GameObject seed;
	public Camera camera;
	public float initialSpeed;

	// Use this for initialization
	void Start() {

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)) {
			// Throw seed
			GameObject thrownSeed = (GameObject)Instantiate(this.seed, transform.position, Quaternion.identity);
			Vector2 p = camera.ScreenToWorldPoint(Input.mousePosition);
			Vector2 v = (p - new Vector2(transform.position.x, transform.position.y));
			thrownSeed.rigidbody2D.AddForce(v.normalized * 700);

			/*var worldCoords = camera.ScreenToWorldPoint (Input.mousePosition);
			var x = worldCoords.x;
			var y = worldCoords.y;
			var v = initialSpeed;
			var g = Physics.gravity.y;

			var fac = Mathf.Pow (v, 4) - (g * (g * (x * x)) + (2 * y * (v * v)));
			if(fac < 0) {
				// zomg
				Debug.Log("zomg holy chicken nuggetz");
			}
			var sqr = Mathf.Sqrt (fac);
			var num = (v * v) + sqr;
			var den = (g * x);
			//Debug.Log("sqr: " + sqr + " num: " + num + " den: " + den + " fac: " + fac + " g: " + g + " x: " + x + " y: " + y);
			var theet = Mathf.Atan (num / den);
			Debug.Log(theet);*/
		}
	}
}
