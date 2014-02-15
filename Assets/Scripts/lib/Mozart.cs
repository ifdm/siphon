using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Mozart : MonoBehaviour {

	protected Dictionary<string, AudioClip> clips = new Dictionary<string, AudioClip>();
	protected Dictionary<string, AudioSource> sources = new Dictionary<string, AudioSource>();

	public virtual void AnimationEvent(string action) {}
	public virtual void Awake() {}

	public virtual void Start() {
		AudioSource audio;
		foreach(KeyValuePair<string, AudioClip> clip in clips) {
			audio = gameObject.AddComponent<AudioSource>();	
			audio.clip = clip.Value;
			if(clip.Value == null) 
				Debug.Log("clip " + clip + " is null");
			sources.Add(clip.Key, audio);
		}
	}

	public void Play(string name, float volume = 1f, bool loop = false, ulong delay = 0) {
		if(Available(name)) {
			sources[name].volume = volume;
			sources[name].loop = loop;
			sources[name].Play(delay);
		}
	}

	public void One(string name, float volume = 1f, bool loop = false, ulong delay = 0) {
		if(Available(name)) {
			if(!sources[name].isPlaying) {
				Play(name, volume, loop, delay);
			}
		}		
	}

	public void Pause(string name) {
		if(Available(name)) {
			if(sources[name].isPlaying) {
				sources[name].Pause();
			}
		}
	}

	public void Stop(string name) {
		if(Available(name)) {
			sources[name].Stop();
		}
	}

	public void Schedule(string name, float time) {
		if(Available(name)) {
			sources[name].PlayScheduled(time);
		}
	}

	public void ClearAll() {
		foreach(KeyValuePair<string, AudioSource> source in sources) {
			source.Value.Stop();
		}
	}

	private bool Available(string name) {
		return sources.ContainsKey(name);
	}
}