using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Animator))]
public class Scene_05_Eyes : MonoBehaviour {

	protected Animator animator;

	public const uint STATE_CLOSE = 0;
	public const uint STATE_OPEN = 1;

	protected uint _currentAnimationState = STATE_CLOSE;
	public uint CurrentAnimationState {
		get {
			return _currentAnimationState;
		}

		set {
			animator.SetInteger ("state", (int)value);
			_currentAnimationState = value;
		}
	}

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
