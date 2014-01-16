using UnityEngine;
using Spine;
using System;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {

	[HideInInspector] public SkeletonAnimation skeletonAnimation;
	[HideInInspector] public Spine.AnimationState state;

	void Start() {
		skeletonAnimation = GetComponent<SkeletonAnimation>();
		state = skeletonAnimation.state;
	}

	public void Jump() {
		state.ClearTracks();
		state.TimeScale = 1.5f;
		state.AddAnimation(1, "jump2", false, 0);
	}

	public void Run() {
		state.TimeScale = Mathf.Abs(gameObject.rigidbody2D.velocity.x) / 4;
        state.AddAnimation(0, "run", false, 0);
	}

	public void Fall() {
		state.ClearTracks();
		state.TimeScale = 1.0f;
		state.AddAnimation(0, "run", false, 0);
	}

	public void Ledging() {

	}

	public void PullUp() {

	}

	public void Idle() {
		state.ClearTracks();
		state.AddAnimation(0, "idle", false, 0);
	}

}