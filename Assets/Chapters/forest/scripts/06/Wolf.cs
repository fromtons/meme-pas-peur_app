using UnityEngine;
using System.Collections;
using MPP.Events;
using MPP.Inputs._Sound;

namespace MPP.Forest.Scene_06 {
	public class Wolf : MonoBehaviour {
		protected Animator animator;

		public static uint STATE_SLEEPING = 0;
		public static uint STATE_AWAKEN = 1;

		Coroutine awakeCoolDown;
		protected uint _currentAnimationState = STATE_SLEEPING;
		public uint CurrentAnimationState {
			get {
				return _currentAnimationState;
			}

			set {
				animator.SetInteger ("state", (int)value);
				_currentAnimationState = value;

				WolfEventManager.TriggerWolfChangeState (new WolfEventArgs { State = value });

				if (value == STATE_AWAKEN) {

					// Growl
					if(audioGrowl != null && !audioGrowl.isPlaying)
						audioGrowl.Play();

					// Awake debouncer
					if(awakeCoolDown != null) StopCoroutine (awakeCoolDown);
					awakeCoolDown = StartCoroutine (AwakeCoolDown());
				}
			}
		}

		public float shakeFactor = 0f;

		bool _listening = false;
		public AudioSource audio;
		public AudioSource audioGrowl;

		MicEventManager.MicEvent onSoundCapBegin;
		WolfEventManager.WolfEvent onListening;

		void Start() {
			animator = this.GetComponent<Animator> ();
			if(audio == null)
				audio = this.GetComponent<AudioSource> ();

			onSoundCapBegin = new MicEventManager.MicEvent (OnSoundCapBegin);
			MicEventManager.SoundCapBegin += onSoundCapBegin;

			onListening = new WolfEventManager.WolfEvent (OnListening);
			WolfEventManager.WolfListening += onListening;
		}

		void Update() {
			shakeFactor = AudioUtils.GetAveragedVolume (audio, 256) * 5;
		}

		void OnListening(WolfEventArgs eventArgs) {
			if(eventArgs.Listening != null)
				_listening = eventArgs.Listening;
		}

		void OnSoundCapBegin(MicEventArgs eventArgs) {
			if (_listening && eventArgs.OriginID == "wolf") {
				CurrentAnimationState = STATE_AWAKEN;
			}
		}

		void OnDestroy() {
			MicEventManager.SoundCapBegin -= OnSoundCapBegin;
			WolfEventManager.WolfListening -= OnListening;
		}

		IEnumerator AwakeCoolDown() {
			yield return new WaitForSeconds (.5f);
			CurrentAnimationState = STATE_SLEEPING;
		}
	}
}