using UnityEngine;
using System.Collections;

public class FallSound : MonoBehaviour {

	private AudioSource audio;
	public AudioClip fallSound;
	private int stallFor = 100;
	// Use this for initialization
	void Start () {
		audio = gameObject.AddComponent<AudioSource>();
		audio.clip = fallSound;
	}

	void Update(){
		stallFor --;
	}

	void OnCollisionEnter2D(Collision2D collision) {
    if (stallFor < 0 && collision.relativeVelocity.magnitude > 10 && collision.gameObject.tag != "Player"){
        audio.Play();
      }
  }
}
