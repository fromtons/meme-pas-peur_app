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

			if(value == STATE_OPEN_WIDE) insist = false;
		}
	}
		
	public Scene_05_Eyes Eyes;
	public Scene_05_Bunny Bunny;
	
	public AudioClip bushMove1;
	public AudioClip bushMove2;
	public AudioClip bushSpread;
	
	AudioSource audioSource;

	int insistCpt = 0;
	bool insist = true;

	TalkEventManager.TalkEvent onTalkEnded;
	MicEventManager.MicEvent onSoundCapBegin;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		StartCoroutine (TimeWaiter ());
		audioSource = this.GetComponent<AudioSource>();

		onTalkEnded = new TalkEventManager.TalkEvent (OnTalkEnded);
		TalkEventManager.TalkEnded += onTalkEnded;

		onSoundCapBegin = new MicEventManager.MicEvent (OnSoundCapBegin);
		MicEventManager.SoundCapBegin += onSoundCapBegin;
	}

	IEnumerator TimeWaiter() {
		yield return new WaitForSeconds (1f);
		TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID = "piri", AudioClipId = 0, Autoplay = true });
		yield return new WaitForSeconds(5f);
		OpenSmall ();
	}

	void OpenSmall() {
		CurrentAnimationState = STATE_OPEN_SMALL;
		TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID = "piri", AudioClipId = 1, Autoplay = true });
	}

	void OnTalkEnded(TalkEventArgs e) {
		if (e.ID == "piri") {
			if (e.AudioClipId != 0 && e.AudioClipId != 2)
				StartCoroutine (Insist ());
			else if(e.AudioClipId == 2) 
				OngletManager.instance.HighlightNextOnglet ();
		}
	}

	IEnumerator Insist() {
		yield return new WaitForSeconds (3f);
		if(insist)
			TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID = "piri", AudioClipId = (insistCpt % 3)+3, Autoplay = true });
		insistCpt++;
	}

	void ToggleBunny() {
		Bunny.CurrentAnimationState = Scene_05_Bunny.STATE_VISIBLE;
	}
	
	void PlaySound(int id) {
		switch(id) {
			case 1:
				audioSource.clip = bushMove1;
				break;
			case 2:
				audioSource.clip = bushMove2;
				break;
			case 3:
				audioSource.clip = bushSpread;
				break;
		}
		
		audioSource.time = 0f;
		audioSource.Play();
	}
	
	void OnSoundCapBegin(MicEventArgs eventArgs) {
		if (eventArgs.OriginID == "leafs" && CurrentAnimationState == STATE_OPEN_SMALL) {
			CurrentAnimationState = STATE_OPEN_WIDE;
		}
	}

	void OnDestroy() {
		TalkEventManager.TalkEnded -= onTalkEnded;
		MicEventManager.SoundCapBegin -= onSoundCapBegin;
	}
}
