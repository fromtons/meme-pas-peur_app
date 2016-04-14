using UnityEngine;
using System.Collections;
using System.Reflection;

public class Scene_05_Leafs : MonoBehaviour {
	protected Animator animator;

	protected const uint STATE_IDLE = 0;
	protected const uint STATE_OPEN_SMALL = 1;
	protected const uint STATE_OPEN_WIDE = 2;

	protected uint _currentAnimationState = STATE_IDLE;
	public uint CurrentAnimationState {
		get {
			return _currentAnimationState;
		}

		set {
			animator.SetInteger ("state", (int)value);
			_currentAnimationState = value;

			if (value == STATE_OPEN_SMALL)
				Eyes.CurrentAnimationState = Scene_05_Eyes.STATE_OPEN;
			else 
				Eyes.CurrentAnimationState = Scene_05_Eyes.STATE_CLOSE;
		}
	}

	public MicrophoneInput micInputToListen;
	public float loudnessCap;
	public Scene_05_Eyes Eyes;
	public Scene_05_Bunny Bunny;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		StartCoroutine (TimeWaiter ());
	}

	IEnumerator TimeWaiter() {
		yield return new WaitForSeconds(6f);
		OpenSmall ();
	}

	void OpenSmall() {
		CurrentAnimationState = STATE_OPEN_SMALL;
	}

	void ToggleBunny() {
		Bunny.CurrentAnimationState = Scene_05_Bunny.STATE_VISIBLE;
	}
	
	// Update is called once per frame
	void Update () {
		if (CurrentAnimationState == STATE_OPEN_SMALL) {
			if (micInputToListen.loudness > loudnessCap) {
				CurrentAnimationState = STATE_OPEN_WIDE;
			}
		}
	}
}
