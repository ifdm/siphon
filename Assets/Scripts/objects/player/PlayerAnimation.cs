using UnityEngine;
using Spine;
using System;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {

	[HideInInspector] public SkeletonAnimation skeletonAnimation;
	[HideInInspector] public Spine.AnimationState state;
	[HideInInspector] public float TimeScale = 1.0f;

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

	public void Jump() {
		Normalize();
		state.SetAnimation(0, "jump-up", false);
	}

	public void Run() {
		Normalize();
        state.SetAnimation(0, "run", true);
	}

	public void Fall() {
		Normalize();
        state.SetAnimation(0, "fall", true);
	}

	public void Ledging() {
		Normalize();
		state.SetAnimation(0, "ledge-hang", false);
	}

	public void PullingUp() {
		Normalize();
		state.SetAnimation(0, "ledge-climb", false);
	}

	public void Stop() {
		Normalize();
		state.SetAnimation(0, "stop", false);
	}

	public void Idle() {
		Normalize();
		state.SetAnimation(0, "idle", true);
	}

	public void Landing() {
		Normalize();
		state.SetAnimation(0, "land", false);
	}

	public void Throw() {
		Normalize();
		state.SetAnimation(1, "throw", false);
	}

	private void Normalize() {
		TimeScale = 1.0f;
	}
}