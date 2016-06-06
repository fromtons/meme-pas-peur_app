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
					// Awake debouncer
					if(awakeCoolDown != null) StopCoroutine (awakeCoolDown);
					awakeCoolDown = StartCoroutine (AwakeCoolDown());
				}
			}
		}

		public float shakeFactor = 0f;

		AudioSource audio;

		MicEventManager.MicEvent onSoundCapBegin;

		void Start() {
			animator = this.GetComponent<Animator> ();
			audio = this.GetComponent<AudioSource> ();

			onSoundCapBegin = new MicEventManager.MicEvent (OnSoundCapBegin);
			MicEventManager.SoundCapBegin += onSoundCapBegin;
		}

		void Update() {
			shakeFactor = AudioUtils.GetAveragedVolume (audio, 256) * 5;
		}

		void OnSoundCapBegin(MicEventArgs eventArgs) {
			if (eventArgs.OriginID == "wolf") {
				CurrentAnimationState = STATE_AWAKEN;
			}
		}

		void OnDestroy() {
			MicEventManager.SoundCapBegin -= OnSoundCapBegin;
		}

		IEnumerator AwakeCoolDown() {
			yield return new WaitForSeconds (.5f);
			CurrentAnimationState = STATE_SLEEPING;
		}
	}
}