using UnityEngine;
using System.Collections;

[RequireComponent (typeof(BoxCollider2D))]
public class Scene_07_Mushroom : MonoBehaviour {

	protected Animator animator;

	protected const uint STATE_IDLE = 0;
	protected const uint STATE_SHAKE = 1;

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

	BoxCollider2D boxCollider;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();
		boxCollider = this.GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (CurrentAnimationState == STATE_SHAKE)
			CurrentAnimationState = STATE_IDLE;

		if (Input.GetMouseButtonDown(0) && CurrentAnimationState == STATE_IDLE) {
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);
			if (hitCollider == boxCollider) {
				CurrentAnimationState = STATE_SHAKE;
			}
		}
	}
}
