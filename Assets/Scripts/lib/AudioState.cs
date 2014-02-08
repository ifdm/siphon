using UnityEngine;
using System.Collections;

public class AudioState : MonoBehaviour {

	void Start() {
		Debug.Log(audio.clip);
		Debug.Log(audio.volume);
		audio.Play();
		Debug.Log(audio.isPlaying);
	}
	
	void Update() {
	
	}
}
