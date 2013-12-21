using UnityEngine;
using System.Collections;

public class PlayerThrow : MonoBehaviour {

	public GameObject seed;
	public Camera camera;
	
<<<<<<< HEAD
	public GameObject slot1;
	public GameObject slot2;
	public GameObject slot3;
=======
	public GameObject[] slots;
>>>>>>> 84f135a9b222248eabc45288989868da25b44a93
	
	private int activeSlot;
	
	void Update () {
		if(Input.GetMouseButtonDown(0)) {
			GameObject thrownSeed = (GameObject)Instantiate(this.seed, transform.position, Quaternion.identity);
			Vector2 p = camera.ScreenToWorldPoint(Input.mousePosition);
			Vector2 v = (p - new Vector2(transform.position.x, transform.position.y));
			thrownSeed.GetComponent<SeedThrow>().destiny = slots[activeSlot];
			thrownSeed.rigidbody2D.AddForce(v.normalized * 700);
<<<<<<< HEAD
			
			if(activeSlot == 0){thrownSeed.GetComponent<SeedThrow>().destiny = slot1;}
			else if(activeSlot == 1){thrownSeed.GetComponent<SeedThrow>().destiny = slot2;}
			else if(activeSlot == 2){thrownSeed.GetComponent<SeedThrow>().destiny = slot3;}

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
		
		if(Input.GetKeyDown(KeyCode.Alpha1)){activeSlot = 0;}
		else if(Input.GetKeyDown(KeyCode.Alpha2)){activeSlot = 1;}
		else if(Input.GetKeyDown(KeyCode.Alpha3)){activeSlot = 2;}
=======
		}
		
		for(int i = 0; i < slots.Length; i++) {
			if(Input.GetKeyDown((i + 1).ToString())) {
				activeSlot = i;
				break;
			}
		}
>>>>>>> 84f135a9b222248eabc45288989868da25b44a93
	}
}
