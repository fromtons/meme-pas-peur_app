using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(AudioSource))]
[RequireComponent (typeof(BoxCollider2D))]
public class Talker : MonoBehaviour {

	// Editor variables
	public string ID;
	public List<AudioClip> audioClips;
	public MouthManager mouthManager;
	public GameObject newMessageUiPrefab;

	// Gameobject Components
	AudioSource audioSource;
	BoxCollider2D collider;
	SpriteRenderer sprite;

	// Talker related
	GameObject newMessageUi;
	int currentClipId = 0;
	bool isNew = false;
	public bool IsNew {
		get { return isNew; }
		set { 
			isNew = value; 
			newMessageUi.SetActive (isNew);
		}
	}
	List<bool> audioClipsPlayed;
	public List<bool> AudioClipsPlayed {
		get { return audioClipsPlayed; }
	}

	// Use this for initialization
	void Awake () {
		// Fill the components
		audioSource = this.GetComponent<AudioSource> ();
		collider = this.GetComponent<BoxCollider2D> ();
		sprite = this.GetComponent<SpriteRenderer> ();

		// Subscribe to talkset events
		TalkEventManager.TalkSet+= new TalkEventManager.TalkEvent(OnTalkSet);

		// Fill the list of played audioclips
		audioClipsPlayed = new List<bool> ();
		foreach(AudioClip clip in audioClips) {
			audioClipsPlayed.Add (false);
		}

		// Adds the UI part
		newMessageUi = Instantiate (newMessageUiPrefab);
		newMessageUi.transform.SetParent (this.transform);
		newMessageUi.SetActive (false);
		// Try to position it according to sprite bounds
		if (sprite)
			newMessageUi.transform.position = new Vector3 (sprite.bounds.center.x + sprite.bounds.size.x / 2, sprite.bounds.center.y + sprite.bounds.size.y / 2, sprite.bounds.center.z);
	}

	// toggles the UI + arm the new clip 
	void SetCurrentClip(int newClipId, bool autoplay) {
		IsNew = true;
		currentClipId = newClipId;
		audioSource.Stop ();

		if (autoplay)
			PlayCurrentClip ();
	}

	// play the current clip according to its ID
	void PlayCurrentClip() {
		audioSource.time = 0f;
		audioSource.clip = audioClips [currentClipId];
		audioSource.Play ();

		if (mouthManager)
			mouthManager.play ();

		IsNew = false;

		TalkEventManager.TriggerTalkBegin(new TalkEventArgs { ID = this.ID, AudioClipId = currentClipId });

		StartCoroutine (WaitForClipEnd ());
	}

	// Waits for the current clip to end playing
	IEnumerator WaitForClipEnd() {
		yield return new WaitForSeconds (audioSource.clip.length);
		audioClipsPlayed [currentClipId] = true;

		if (mouthManager)
			mouthManager.stop ();

		TalkEventManager.TriggerTalkEnded(new TalkEventArgs { ID = this.ID, AudioClipId = currentClipId });
	}

	// TalkSet event listener which allows to call SetCurrentClip without access to this talker's instance
	void OnTalkSet(TalkEventArgs eventArgs) {
		if (eventArgs.ID == this.ID) {
			SetCurrentClip (eventArgs.AudioClipId, eventArgs.Autoplay);
		}
	}
	
	// Update is called once per frame
	void Update () {

		// Click listener
		if (Input.GetMouseButtonDown(0) && isNew) {
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);
			if (hitCollider == collider) {
				PlayCurrentClip ();
			}
		}
	}
}
