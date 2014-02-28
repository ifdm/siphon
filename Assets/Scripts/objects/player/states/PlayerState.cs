using UnityEngine;
using System.Collections;
using Spine;

public class PlayerState {
	// States
	public static JumpingState Jumping = new JumpingState();
	public static IdlingState Idling = new IdlingState();
	public static RunningState Running = new RunningState();
	public static LedgingState Ledging = new LedgingState();
	public static ClimbingState Climbing = new ClimbingState();
	public static FallingState Falling = new FallingState();
	public static DyingState Dying = new DyingState();
	public static InteractingState Interacting = new InteractingState();
	public static SlidingState Sliding = new SlidingState();

	public virtual void HandleInput(PlayerControl player) {}
	public virtual void Update(PlayerControl player) {}
	public virtual void Enter(PlayerControl player, PlayerState from) {}
	public virtual void Exit(PlayerControl player, PlayerState to) {}
}