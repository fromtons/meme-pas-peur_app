using UnityEngine;
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

	TalkEventManager.TalkEvent onTalkEnded;
	
	MicEventManager.MicEvent onSoundCapBegin;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
		listenToCollider = listenTo.GetComponent<BoxCollider2D> ();
		initialPosition = this.gameObject.transform.position;

		TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID = "piri", AudioClipId = 0, Autoplay = false });
		onTalkEnded = new TalkEventManager.TalkEvent (OnTalkEnded);	
		TalkEventManager.TalkEnded += onTalkEnded;
		
		onSoundCapBegin = new MicEventManager.MicEvent (OnSoundCapBegin);
		MicEventManager.SoundCapBegin += onSoundCapBegin;
	}
	
	// Update is called once per frame
	void Update () {
		CheckTarget ();
	}

	void CheckTarget() {
		if (!clicked && firstSentenceDone) {
			
			if(Input.GetMouseButtonDown(0)) { 
				Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);
				if (hitCollider == listenToCollider) {
					clicked = true;
					TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID = "piri", AudioClipId = 1, Autoplay = true });
				}
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
	
	void OnSoundCapBegin(MicEventArgs eventArgs) {
		if (!clicked && firstSentenceDone) {
			clicked = true;
			TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID = "piri", AudioClipId = 1, Autoplay = true, Delay = 0.5f });
		}
	}

	void TriggerAnimation() {
		CurrentAnimationState = STATE_PROFILE_WALK;

		Hashtable ht = new Hashtable ();
		ht.Add ("position", new Vector3(this.initialPosition.x+8f,this.initialPosition.y,this.initialPosition.z));
		ht.Add ("time", 2f);
		ht.Add ("easetype", "linear");
		iTween.MoveTo (this.gameObject, ht);

		OngletManager.instance.HighlightNextOnglet ();
	}

	void OnDestroy() {
		TalkEventManager.TalkEnded -= onTalkEnded;
		MicEventManager.SoundCapBegin -= onSoundCapBegin;
	}
}
