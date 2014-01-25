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
		state.SetAnimation(0, "jump-up", false);
	}

	public void Run() {
		Normalize();
        state.SetAnimation(0, "run", true);
	}

	public void Fall() {
		Normalize();
        state.AddAnimation(0, "fall", true, 0);
	}

	public void Ledging() {
		Normalize();
		state.SetAnimation(0, "ledge-hang", false);
	}

	public void PullingUp() {
		Normalize();
		state.SetAnimation(0, "ledge-climb", false);
	}

	public void Idle() {
		state.SetAnimation(0, "idle", true);
	}

	public void Landing() {
		Normalize();
		state.SetAnimation(0, "land", false);
	}

	private void Normalize() {
		state.TimeScale = 1f;
	}

}