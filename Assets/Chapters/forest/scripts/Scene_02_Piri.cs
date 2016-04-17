﻿using UnityEngine;
using System.Collections;

public class Scene_02_Piri : MonoBehaviour {

	protected Animator animator;

	protected const uint STATE_IDLE = 0;
	protected const uint STATE_PROFILE_WALK = 1;

	protected uint _currentAnimationState = STATE_IDLE;
	public uint CurrentAnimationState {
		get {
			return _currentAnimationState;
		}

		set {
			animator.SetInteger ("state", (int)value);
			_currentAnimationState = value;

			if (value == STATE_PROFILE_WALK) {
				mouthToHide.SetActive (false);
			}
		}
	}

	public GameObject mouthToHide;
	public GameObject listenTo;
	public float walkAwayDuration = 2f;

	bool firstSentenceDone = false;

	BoxCollider2D listenToCollider;
	bool clicked = false;

	Vector3 initialPosition;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
		listenToCollider = listenTo.GetComponent<BoxCollider2D> ();
		initialPosition = this.gameObject.transform.position;

		TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID = "piri", AudioClipId = 0, Autoplay = false });
		TalkEventManager.TalkEnded += new TalkEventManager.TalkEvent (OnTalkEnded);	
	}
	
	// Update is called once per frame
	void Update () {
		CheckTarget ();
	}

	void CheckTarget() {
		if (Input.GetMouseButtonDown(0) && !clicked && firstSentenceDone) {
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);
			if (hitCollider == listenToCollider) {
				TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID = "piri", AudioClipId = 1, Autoplay = true });
			}
		}
	}

	void OnTalkEnded(TalkEventArgs eventArgs) {
		if (eventArgs.ID == "piri") {
			if (eventArgs.AudioClipId == 0) {
				firstSentenceDone = true;
			} else if (eventArgs.AudioClipId == 1) {
				TriggerAnimation ();
			}
		}
	}

	void TriggerAnimation() {
		clicked = true;

		CurrentAnimationState = STATE_PROFILE_WALK;

		Hashtable ht = new Hashtable ();
		ht.Add ("position", new Vector3(this.initialPosition.x+8f,this.initialPosition.y,this.initialPosition.z));
		ht.Add ("time", 2f);
		ht.Add ("easetype", "linear");
		iTween.MoveTo (this.gameObject, ht);

		OngletManager.instance.HighlightNextOnglet ();
	}
}
