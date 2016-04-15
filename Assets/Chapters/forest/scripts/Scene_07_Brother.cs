using UnityEngine;
using System.Collections;

[RequireComponent (typeof(BoxCollider2D))]
public class Scene_07_Brother : MonoBehaviour {

	protected Animator animator;

	protected const uint STATE_IDLE = 0;
	protected const uint STATE_REVEAL = 1;
	protected const uint STATE_BOUNCE_TO_FRONT = 2;

	protected uint _currentAnimationState = STATE_IDLE;
	public uint CurrentAnimationState {
		get {
			return _currentAnimationState;
		}

		set {
			animator.SetInteger ("state", (int)value);
			_currentAnimationState = value;
		}
	}

	public GameObject hiddenBy;
	BoxCollider2D hiddenBy_BC;

	BoxCollider2D boxCollider;

	public MicrophoneInput micInputToListen;
	public float loudnessCap = 40f;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();
		hiddenBy_BC = hiddenBy.GetComponent<BoxCollider2D> ();
		boxCollider = this.GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (CurrentAnimationState == STATE_IDLE && Input.GetMouseButtonDown(0)) {
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);
			if (hitCollider == hiddenBy_BC) {
				CurrentAnimationState = STATE_REVEAL;
			}
		}
			
		if (CurrentAnimationState == STATE_REVEAL && Input.GetMouseButtonDown(0)) {
			Vector2 mousePosition2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Collider2D hitCollider2 = Physics2D.OverlapPoint(mousePosition2);
			if (hitCollider2 == boxCollider) {
				CurrentAnimationState = STATE_BOUNCE_TO_FRONT;
			}
		}

		if (CurrentAnimationState == STATE_REVEAL && micInputToListen.loudness > loudnessCap)
			CurrentAnimationState = STATE_BOUNCE_TO_FRONT;
	}
}
