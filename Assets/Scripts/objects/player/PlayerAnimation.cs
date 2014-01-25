using UnityEngine;
using Spine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerAnimation : MonoBehaviour {

	[HideInInspector] public SkeletonAnimation skeletonAnimation;
	[HideInInspector] public Spine.AnimationState state;
	[HideInInspector] public float TimeScale = 1.0f;
	Dictionary<string, string> actions = new Dictionary<string, string>() {
		{"Jump", "jump-up"},
		{"Run", "run"},
		{"Fall", "fall"},
		{"Ledge", "ledge-hang"},
		{"PullUp", "ledge-climb"},
		{"Stop", "stop"},
		{"Land", "land"},
		{"Throw", "throw"},
		{"Idle", "idle"}
	};

	void Start() {
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
		if(actions.ContainsKey(animation)) {
			Normalize();
			state.SetAnimation(track, actions[animation], loop);
		}
	}

	public void Add(string animation, bool loop = false, int track = 0) {
		if(actions.ContainsKey(animation)) {
			Normalize();
			state.AddAnimation(track, actions[animation], loop, 0);
		}
	}

	private void Normalize() {
		TimeScale = 1.0f;
	}
}