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

			if (value == STATE_REVEAL) {
				insist = false;
				TalkEventManager.TriggerTalkStop (new TalkEventArgs { ID = "piri" });
				TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID = "piri", AudioClipId = 3, Autoplay = true });
			} else if (value == STATE_BOUNCE_TO_FRONT) {
				TalkEventManager.TriggerTalkStop (new TalkEventArgs { ID = "piri" });
				insistRevealed = false;
			}
		}
	}

	public GameObject hiddenBy;
	BoxCollider2D hiddenBy_BC;

	BoxCollider2D boxCollider;

	public MicrophoneInput micInputToListen;
	public float loudnessCap = 40f;

	bool insist = true;
	int insistCpt = 0;

	bool insistRevealed = true;
	int insistRevealdCpt = 0;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();
		hiddenBy_BC = hiddenBy.GetComponent<BoxCollider2D> ();
		boxCollider = this.GetComponent<BoxCollider2D> ();

		TalkEventManager.TalkEnded += new TalkEventManager.TalkEvent (OnTalkEnded);
		StartCoroutine (Insist ());
	}

	void OnTalkEnded(TalkEventArgs e) {
		if (e.ID == "piri") {
			if (e.AudioClipId < 3)
				StartCoroutine (Insist ());
			else if (e.AudioClipId < 5)
				StartCoroutine (InsistRevealed ());
			else if (e.AudioClipId == 5)
				TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID = "brother", AudioClipId = 1, Autoplay = true });
				
		} else if (e.ID == "brother") {
			if (e.AudioClipId == 0) {
				TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID = "piri", AudioClipId = 5, Autoplay = true });
			} else if (e.AudioClipId == 1) {
				OngletManager.instance.HighlightNextOnglet ();
			}
		}
	}

	IEnumerator Insist() {
		yield return new WaitForSeconds (3f);
		if(insist)
			TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID = "piri", AudioClipId = (insistCpt % 3), Autoplay = true });
		insistCpt++;
	}

	IEnumerator InsistRevealed() {
		yield return new WaitForSeconds (5f);
		if(insistRevealed)
			TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID = "piri", AudioClipId = 4, Autoplay = true });
		insistRevealdCpt++;
	}

	public void BrotherReact() {
		TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID = "brother", AudioClipId = 0, Autoplay = true });
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
