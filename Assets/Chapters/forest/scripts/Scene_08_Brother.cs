using UnityEngine;
using System.Collections;

public class Scene_08_Brother : MonoBehaviour {

	protected Animator animator;

	protected const uint STATE_IDLE = 0;
	protected const uint STATE_WITH_BABY_OWL = 1;

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

	public float delay = 3f;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();

		StartCoroutine (LaunchAnimation());
	}

	IEnumerator LaunchAnimation() {
		yield return new WaitForSeconds (delay);
		CurrentAnimationState = STATE_WITH_BABY_OWL;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
