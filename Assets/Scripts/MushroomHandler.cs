using UnityEngine;
using System.Collections;

public class MushroomHandler : MonoBehaviour {

	float minHeightReached = 0f;

	void MushroomBounceEvent(){
		print ("Mushroom Event: " + minHeightReached);	
	}


	void FixedUpdate(){
		if (transform.position.y < minHeightReached)
						minHeightReached = transform.position.y;
	}
}
