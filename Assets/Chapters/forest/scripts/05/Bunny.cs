using UnityEngine;
using System.Collections;

namespace MPP.Forest.Scene_05 {
	public class Bunny : MonoBehaviour {
		protected Animator animator;

		public static uint STATE_HIDDEN = 0;
		public static uint STATE_VISIBLE = 1;
		public static uint STATE_HOPPING = 2;
		
		public AudioClip onRevealSound;
		public AudioClip boingSound;

		AudioSource audioSource;

		protected uint _currentAnimationState = STATE_HIDDEN;
		public uint CurrentAnimationState {
			get {
				return _currentAnimationState;
			}

			set {
				animator.SetInteger ("state", (int)value);
				_currentAnimationState = value;

				if (value == STATE_VISIBLE) {
					StartCoroutine (AfterVisible());
				}
			}
		}

		// Use this for initialization
		void Start () {
			animator = this.GetComponent<Animator> ();
			audioSource = this.GetComponent<AudioSource>();
		}

		IEnumerator AfterVisible() {
			audioSource.clip = onRevealSound;
			audioSource.Play();
			yield return new WaitForSeconds (5f);
			CurrentAnimationState = STATE_HOPPING;
			audioSource.clip = boingSound;
			audioSource.Play();
		}

		void FinishedHopping() {
			TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID = "piri", AudioClipId = 2, Autoplay = true });	
		}
	}
}