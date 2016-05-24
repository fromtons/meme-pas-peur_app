using UnityEngine;
using System.Collections;
using MPP.Events;
using MPP.Inputs._Sound;

namespace MPP.Forest.Scene_06 {
	public class Wolf : MonoBehaviour {
		protected Animator animator;

		public static uint STATE_SLEEPING = 0;
		public static uint STATE_AWAKEN = 1;

		protected uint _currentAnimationState = STATE_SLEEPING;
		public uint CurrentAnimationState {
			get {
				return _currentAnimationState;
			}

			set {
				animator.SetInteger ("state", (int)value);
				_currentAnimationState = value;

				nbOfFramesSinceAwakened = 0;
			}
		}

		public float shakeFactor = 0f;

		AudioSource audio;
		int nbOfFramesSinceAwakened = 0;

		MicEventManager.MicEvent onSoundCapBegin;

		void Start() {
			animator = this.GetComponent<Animator> ();
			audio = this.GetComponent<AudioSource> ();

			onSoundCapBegin = new MicEventManager.MicEvent (OnSoundCapBegin);
			MicEventManager.SoundCapBegin += onSoundCapBegin;
		}

		void Update() {
			if (CurrentAnimationState == STATE_AWAKEN) {
				if(nbOfFramesSinceAwakened>0)
					CurrentAnimationState = STATE_SLEEPING;

				nbOfFramesSinceAwakened++;
			}

			shakeFactor = AudioUtils.GetAveragedVolume (audio, 256) * 5;
		}

		void OnSoundCapBegin(MicEventArgs eventArgs) {
			if (eventArgs.OriginID == "wolf") {
				CurrentAnimationState = STATE_AWAKEN;
			}
		}
	}
}