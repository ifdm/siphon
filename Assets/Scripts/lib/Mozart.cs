using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Mozart : MonoBehaviour {

	public float fade = 0.2f;

	protected Dictionary<string, AudioClip> clips = new Dictionary<string, AudioClip>();
	protected Dictionary<string, AudioSource> sources = new Dictionary<string, AudioSource>();
	protected Dictionary<int, string> tracks = new Dictionary<int, string>();

	private float faded;
	private float fadeRate;

	public virtual void AnimationEvent(string action) {}
	public virtual void Awake() {}

	public virtual void Start() {
		AudioSource audio;
		foreach(KeyValuePair<string, AudioClip> clip in clips) {
			audio = gameObject.AddComponent<AudioSource>();	
			audio.clip = clip.Value;
			sources.Add(clip.Key, audio);
		}
	}

	void Update() {
		var removal = tracks.Where(key => !sources[key.Value].isPlaying).ToArray();
		foreach(var item in removal){tracks.Remove(item.Key);}
	}

	public void Play(string name, float volume = 1f, bool loop = false, int track = 0, ulong delay = 0) {
		if(Available(name)) {
			tracks[track] = name;
			sources[name].volume = volume;
			sources[name].loop = loop;
			sources[name].Play(delay);
		}
	}

	public void One(string name, float volume = 1f, bool loop = false, int track = 0, ulong delay = 0) {
		if(Available(name)) {
			if(!sources[name].isPlaying) {
				Play(name, volume, loop, track, delay);
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

	public void CrossFade(string to, float duration, int trackIndex = 0) {
		if(tracks.ContainsKey(trackIndex)) {
			string name = tracks[trackIndex];
			StartCoroutine(Fader(name, to, duration));
		}
		else {
			StartCoroutine(Fader(null, to, duration));
		}

		Play(to, 1f, true, 1);
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

	private IEnumerator Fader(string from, string to, float duration) {
		float maxDuration = duration;
		sources[to].volume = 0f;

		while(duration > 0) {
			if(Available(from)) sources[from].volume -= Mathf.Max(Time.deltaTime / maxDuration, 0f);
			sources[to].volume += Mathf.Min(Time.deltaTime / maxDuration, 1f);

			duration -= Time.deltaTime;
			Debug.Log(duration);

			yield return new WaitForSeconds(0);
		}

		Stop(from);
	}

}