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
		Normalize();

		state.ClearTracks();
		state.AddAnimation(1, "jump-up", false, 0);
	}

	public void Run() {
		state.TimeScale = Mathf.Abs(gameObject.rigidbody2D.velocity.x) / 4.2f;
        state.AddAnimation(0, "run", false, 0);
	}

	public void Fall() {
		Normalize();
		
		state.ClearTracks();
        state.AddAnimation(0, "fall", false, 0);
	}

	public void Ledging() {

	}

	public void PullUp() {

	}

	public void Idle() {
		state.ClearTracks();
		state.AddAnimation(0, "idle", false, 0);
	}

	private void Normalize() {
		state.TimeScale = 1f;
	}

}