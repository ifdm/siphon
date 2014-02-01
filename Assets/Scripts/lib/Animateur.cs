using UnityEngine;
using Spine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Animateur : MonoBehaviour {

	protected enum Type {Spine, Sprite};
	protected struct AnimationType {
		public Type type;
		public String name;

		public AnimationType(Type type, String name) {
			this.type = type;
			this.name = name;
		}
	};

	[HideInInspector] public SkeletonAnimation skeletonAnimation;
	[HideInInspector] public Spine.AnimationState state;
	[HideInInspector] public float TimeScale = 1.0f;
	protected Dictionary<string, AnimationType> actions = new Dictionary<string, AnimationType>();
	
	public virtual void Start() {
		skeletonAnimation = GetComponent<SkeletonAnimation>();
		state = skeletonAnimation.state;
		state.Event += Event;
	}

	void Update() {
		state.TimeScale = TimeScale;
	}

	public void Event(object sender, EventTriggeredArgs e) {
		this.gameObject.transform.parent.SendMessage("AnimationEvent", e.Event.String);
	}

	public void Set(string animation, bool loop = false, int track = 0, float timeScale = 1f) {
		if(actions.ContainsKey(animation) && state != null) {
			TimeScale = timeScale;
			state.SetAnimation(track, actions[animation].name, loop);
		}
	}

	public void Add(string animation, bool loop = false, int track = 0, float timeScale = 1f, float delay = 0f) {
		if(actions.ContainsKey(animation) && state != null) {
			TimeScale = timeScale;
			state.AddAnimation(track, actions[animation].name, loop, delay);
		}
	}

	public void Clear(int track = 0) {
		state.ClearTrack(track);
	}

	public void ClearAll() {
		state.ClearTracks();
	}

	private void Normalize() {
		TimeScale = 1.0f;
	}
}