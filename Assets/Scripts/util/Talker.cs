using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MPP.Events;

namespace MPP.Util {
	[RequireComponent (typeof(AudioSource))]
	[RequireComponent (typeof(BoxCollider2D))]
	public class Talker : MonoBehaviour {

		// Editor variables
		public string ID;
		public List<AudioClip> audioClips;
		public MouthManager mouthManager;
		public GameObject newMessageUiPrefab;
		public GameObject newMessageUi;

		// Gameobject Components
		AudioSource audioSource;
		BoxCollider2D collider;
		SpriteRenderer sprite;

		// Talker related
		int currentClipId = 0;
		bool isNew = false;
		public bool IsNew {
			get { return isNew; }
			set { 
				isNew = value; 
				if (newMessageUi)
					newMessageUi.GetComponent<Animator>().SetBool("show", value);
			}
		}
		// TODO - Check if the public getter here is still usefull (dont think so)
		List<bool> audioClipsPlayed;
		public List<bool> AudioClipsPlayed {
			get { return audioClipsPlayed; }
		}

		TalkEventManager.TalkEvent onTalkSet;
		TalkEventManager.TalkEvent onTalkStop;

		// Use this for initialization
		void Awake () {
			// Fill the components
			audioSource = this.GetComponent<AudioSource> ();
			collider = this.GetComponent<BoxCollider2D> ();
			sprite = this.GetComponent<SpriteRenderer> ();

			if (mouthManager != null) {
				mouthManager.AudioSource = audioSource;
			}

			// Subscribe to talkset events
			onTalkSet = new TalkEventManager.TalkEvent(OnTalkSet);
			onTalkStop = new TalkEventManager.TalkEvent (OnTalkStop);
			TalkEventManager.TalkSet += onTalkSet;
			TalkEventManager.TalkStop += onTalkStop;

			// Fill the list of played audioclips
			audioClipsPlayed = new List<bool> ();
			foreach(AudioClip clip in audioClips) {
				audioClipsPlayed.Add (false);
			}

			if (newMessageUiPrefab && !newMessageUi) {
				// Adds the UI part
				newMessageUi = Instantiate (newMessageUiPrefab);
				newMessageUi.transform.SetParent (this.transform);
				IsNew = false;
				// Try to position it according to sprite bounds
				if (sprite)
					newMessageUi.transform.position = new Vector3 (sprite.bounds.center.x + sprite.bounds.size.x / 2, sprite.bounds.center.y + sprite.bounds.size.y / 2, sprite.bounds.center.z);
			}
		}

		// toggles the UI + arm the new clip 
		void SetCurrentClip(int newClipId, bool autoplay, float delay) {
			// We consider that if we change current clip while another one was playing, this has been played, so we stop it cleanly.
			if(audioSource.isPlaying && currentClipId!=newClipId && !AudioClipsPlayed[currentClipId]) StopCurrent ();

			IsNew = true;
			currentClipId = newClipId;
			audioSource.Stop ();

			if (autoplay) {
				StartCoroutine(DelayedPlayCurrentClip(delay));	
			}
		}

		// play the current clip according to its ID
		void PlayCurrentClip() {
			audioSource.time = 0f;
			audioSource.clip = audioClips [currentClipId];
			audioSource.Play ();

			IsNew = false;

			TalkEventManager.TriggerTalkBegin(new TalkEventArgs { ID = this.ID, AudioClipId = currentClipId });

			StartCoroutine (WaitForClipEnd ());
		}
		
		IEnumerator DelayedPlayCurrentClip(float delay) {
			IsNew = false;
			yield return new WaitForSeconds(delay);
			PlayCurrentClip();
		}

		// Waits for the current clip to end playing
		IEnumerator WaitForClipEnd() {
			// We have to store it in a variable because currentClipId may change between this call and the delay below. 
			// Of course we don't want to stop a clip we weren't waiting for. That's why we store it in a local var.
			int clipIdWeWaitFor = currentClipId;

			yield return new WaitForSeconds (audioSource.clip.length);
			if(!audioClipsPlayed[clipIdWeWaitFor])
				StopCurrent ();
		}

		// Allows to really stop the current clip -> we are available for another play immediately, without side effects
		void StopCurrent() {
			audioSource.Stop ();
			audioClipsPlayed [currentClipId] = true;

			TalkEventManager.TriggerTalkEnded(new TalkEventArgs { ID = this.ID, AudioClipId = currentClipId });
		}

		// TalkSet event listener which allows to call SetCurrentClip without access to this talker's instance
		void OnTalkSet(TalkEventArgs eventArgs) {
			if (eventArgs.ID == this.ID) {
				
				// If it already has been played, we reset it
				if (audioClipsPlayed [eventArgs.AudioClipId])
					audioClipsPlayed [eventArgs.AudioClipId] = false;
				
				SetCurrentClip (eventArgs.AudioClipId, eventArgs.Autoplay, eventArgs.Delay);
			}
		}

		void OnTalkStop(TalkEventArgs eventArgs) {
			if (eventArgs.ID == this.ID) {
				StopCurrent ();
			}
		}
		
		// Update is called once per frame
		void Update () {
			// Click listener
			if (Input.GetMouseButtonDown(0) && isNew) {
				Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);
				if ((hitCollider == collider) || (hitCollider == newMessageUi.GetComponent<BoxCollider2D>())) {
					PlayCurrentClip ();
				}
			}
		}

		void OnDestroy() {
			TalkEventManager.TalkSet -= this.onTalkSet;
			TalkEventManager.TalkStop -= this.onTalkStop;
		}
	}
}