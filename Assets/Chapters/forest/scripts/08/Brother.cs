using UnityEngine;
using System.Collections;

namespace MPP.Forest.Scene_08 {
	public class Brother : MonoBehaviour {

		protected Animator animator;

		protected const uint STATE_IDLE = 0;
		protected const uint STATE_WITH_BABY_OWL = 1;

		protected uint _currentAnimationState = STATE_IDLE;
		public uint CurrentAnimationState {
			get {
				return _currentAnimationState;
			}

			set {
				animator.SetInteger ("state", (int)value);
				_currentAnimationState = value;
			}
		}

		public float delay = 3f;

		TalkEventManager.TalkEvent onTalkBegin;

		// Use this for initialization
		void Start () {
			animator = this.GetComponent<Animator> ();

			TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID = "brother_before", AudioClipId = 0, Autoplay = false });
			onTalkBegin = new TalkEventManager.TalkEvent (OnTalkBegin);
			TalkEventManager.TalkBegin += onTalkBegin;
		}

		void OnTalkBegin(TalkEventArgs e) {
			if (e.ID == "brother_before") {
				CurrentAnimationState = STATE_WITH_BABY_OWL;		
			}
		}

		void OnDestroy () {
			TalkEventManager.TalkBegin -= onTalkBegin;
		}
	}
}
