using UnityEngine;
using System.Collections;

public class Scene_06_Wolf : MonoBehaviour {
	protected Animator animator;

	public static uint STATE_SLEEPING = 0;
	public static uint STATE_AWAKEN = 1;

	protected uint _currentAnimationState = STATE_SLEEPING;
	public uint CurrentAnimationState {
		get {
			return _currentAnimationState;
		}

		set {
			animator.SetInteger ("state", (int)value);
			_currentAnimationState = value;

			nbOfFramesSinceAwakened = 0;
		}
	}

	int nbOfFramesSinceAwakened = 0;

	public MicrophoneInput micInputToListen;
	public float loudnessCap = 5f;

	void Start() {
		animator = this.GetComponent<Animator> ();
	}

	void Update() {
		if (CurrentAnimationState == STATE_AWAKEN) {
			if(nbOfFramesSinceAwakened>0)
				CurrentAnimationState = STATE_SLEEPING;

			nbOfFramesSinceAwakened++;
		}

		if (micInputToListen.loudness > loudnessCap) {
			CurrentAnimationState = STATE_AWAKEN;
		}
	}
}
