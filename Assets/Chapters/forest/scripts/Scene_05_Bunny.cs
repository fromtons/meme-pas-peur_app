using UnityEngine;
using System.Collections;

public class Scene_05_Bunny : MonoBehaviour {
	protected Animator animator;

	public static uint STATE_HIDDEN = 0;
	public static uint STATE_VISIBLE = 1;
	public static uint STATE_HOPPING = 2;

	protected uint _currentAnimationState = STATE_HIDDEN;
	public uint CurrentAnimationState {
		get {
			return _currentAnimationState;
		}

		set {
			animator.SetInteger ("state", (int)value);
			_currentAnimationState = value;

			if (value == STATE_VISIBLE) {
				StartCoroutine (AfterVisible());
			}
		}
	}

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();
	}

	IEnumerator AfterVisible() {
		yield return new WaitForSeconds (5f);
		CurrentAnimationState = STATE_HOPPING;
	}
}
