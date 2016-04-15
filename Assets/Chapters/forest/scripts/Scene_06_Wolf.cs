using UnityEngine;
using System.Collections;

public class Scene_06_Wolf : MonoBehaviour {
	protected Animator animator;

	protected const uint STATE_SLEEPING = 0;
	protected const uint STATE_AWAKEN = 1;

	protected uint _currentAnimationState = STATE_SLEEPING;
	public uint CurrentAnimationState {
		get {
			return _currentAnimationState;
		}

		set {
			animator.SetInteger ("state", (int)value);
			_currentAnimationState = value;
		}
	}

	public MicrophoneInput micInputToListen;
	public float loudnessCap = 5f;

	void Start() {
		animator = this.GetComponent<Animator> ();
	}

	void Update() {
		if (CurrentAnimationState == STATE_AWAKEN)
			CurrentAnimationState = STATE_SLEEPING;

		if (micInputToListen.loudness > loudnessCap) {
			CurrentAnimationState = STATE_AWAKEN;
		}
	}
}
