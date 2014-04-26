using UnityEngine;
using System.Collections;

public class FallSound : MonoBehaviour {

	private AudioSource audio;
	public AudioClip fallSound;
	private int stallFor = 50;
	public float threshold = 10f;
	public bool player = false;
	// Use this for initialization
	void Start () {
		audio = gameObject.AddComponent<AudioSource>();
		audio.clip = fallSound;
	}

	void Update(){
		stallFor --;
	}

	void OnCollisionEnter2D(Collision2D collision) {
    if (stallFor < 0 && collision.relativeVelocity.magnitude > threshold && (collision.gameObject.tag != "Player" || player)){
        audio.Play();
      }
  }
}
