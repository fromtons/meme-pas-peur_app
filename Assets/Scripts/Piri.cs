using UnityEngine;
using System.Collections;

public class Piri : MonoBehaviour {

	protected Animator animator;

	public float moveToX;
	public float moveToY;

	public static int STATE_TRISTE = 0;
	public static int STATE_NORMAL = 1;
	int _state = STATE_TRISTE;
	public int state {
		set {
			_state = value;
			animator.SetInteger("state", value);
		}
	}

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();

		iTween.MoveTo(gameObject, iTween.Hash("position", new Vector3(moveToX,moveToY, 0f), "time", Chapter1Manager.introAnimationDuration*0.8, "easetype", iTween.EaseType.easeInOutSine));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
