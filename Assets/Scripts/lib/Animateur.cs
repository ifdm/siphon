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
		Debug.Log(e.TrackIndex + " " + skeletonAnimation.state.GetCurrent(e.TrackIndex) + ": event " + e.Event + ", " + e.Event.Int);
	}

	public void Set(string animation, bool loop = false, int track = 0) {
		if(actions.ContainsKey(animation) && state != null) {
			Normalize();
			state.SetAnimation(track, actions[animation].name, loop);
		}
	}

	public void Add(string animation, bool loop = false, int track = 0) {
		if(actions.ContainsKey(animation) && state != null) {
			Normalize();
			state.AddAnimation(track, actions[animation].name, loop, 0);
		}
	}

	private void Normalize() {
		TimeScale = 1.0f;
	}
}