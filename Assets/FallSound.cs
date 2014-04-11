using UnityEngine;
using System.Collections;

public class FallSound : MonoBehaviour {

	private AudioSource audio;
	public AudioClip fallSound;
	// Use this for initialization
	void Start () {
		audio = gameObject.AddComponent<AudioSource>();
		audio.clip = fallSound;
	}

	void OnCollisionEnter2D(Collision2D collision) {
    if (collision.relativeVelocity.magnitude > 2 && collision.gameObject.tag == "Ground"){
        audio.Play();
      }
  }
}
