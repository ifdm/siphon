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

	protected Dictionary<string, AnimationType> actions = new Dictionary<string, AnimationType>();
	protected Dictionary<int, string> animations = new Dictionary<int, string>();

	[HideInInspector] public SkeletonAnimation skeletonAnimation;
	[HideInInspector] public Spine.AnimationState state;
	[HideInInspector] public float TimeScale = 1.0f;
	
	public virtual void Start() {
		skeletonAnimation = GetComponent<SkeletonAnimation>();
		state = skeletonAnimation.state;

		state.Start += Start;
		state.End += End;
		state.Event += Event;
		state.Complete += Complete;
	}

	void Update() {
		state.TimeScale = TimeScale;
	}

	public void Start(Spine.AnimationState state, int trackIndex) {}
	public void End(Spine.AnimationState state, int trackIndex) {}
	public void Complete(Spine.AnimationState state, int trackIndex, int loop) {}
	public void Event(Spine.AnimationState state, int trackIndex, Spine.Event e) {
		transform.parent.gameObject.BroadcastMessage("AnimationEvent", e.String, SendMessageOptions.DontRequireReceiver);

	}

	public void Set(string animation, bool loop = false, int track = 0, float timeScale = 1f) {
		if(Available(animation)) {
			TimeScale = timeScale;
			animations[track] = actions[animation].name;
			state.SetAnimation(track, actions[animation].name, loop);
		}
	}

	public void One(string animation, bool loop = false, int track = 0, float timeScale = 1f) {
		Set(animation, loop, track, timeScale);
	}

	public void Add(string animation, bool loop = false, int track = 0, float timeScale = 1f, float delay = 0f) {
		if(Available(animation)) {
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

	private bool Available(string animation) {
		return actions.ContainsKey(animation) && state != null;
	}
}