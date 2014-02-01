using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Mozart : MonoBehaviour {

	protected Dictionary<string, AudioClip> clips = new Dictionary<string, AudioClip>();
	private Dictionary<string, AudioSource> sources = new Dictionary<string, AudioSource>();

	public virtual void Awake() {}
	public virtual void Start() {
		AudioSource audio;
		foreach(KeyValuePair<string, AudioClip> clip in clips) {
			audio = gameObject.AddComponent<AudioSource>();			
			audio.clip = clip.Value;
			sources.Add(clip.Key, audio);
		}
	}

	public void Play(string name, bool loop = false,ulong delay = 0) {
		sources[name].loop = loop;
		sources[name].Play(delay);
	}

	public void Pause(string name) {}

	public void Stop(string name) {
		sources[name].Stop();
	}

	public void Add(string name) {}
	public void ClearAll() {}
}