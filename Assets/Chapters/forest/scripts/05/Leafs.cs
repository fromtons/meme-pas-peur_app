﻿using UnityEngine;
using System.Collections;
using System.Reflection;
using MPP.Util;
using MPP.Events;

namespace MPP.Forest.Scene_05 {
	public class Leafs : MonoBehaviour {
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
					Eyes.CurrentAnimationState = Eyes.STATE_OPEN;
				else 
					Eyes.CurrentAnimationState = Eyes.STATE_CLOSE;

				if(value == STATE_OPEN_WIDE) insist = false;
			}
		}
			
		public Eyes Eyes;
		public Bunny Bunny;
		
		public AudioClip bushMove1;
		public AudioClip bushMove2;
		public AudioClip bushSpread;
		
		AudioSource audioSource;

		int insistCpt = 0;
		bool insist = true;
		bool hasClicked = false;

		TalkEventManager.TalkEvent onTalkEnded;
		MicEventManager.MicEvent onBlowBegin;
		BackdoorEventManager.BackdoorEvent onBackdoorToggle;

		// Use this for initialization
		void Start () {
			animator = GetComponent<Animator> ();
			audioSource = this.GetComponent<AudioSource>();

			onTalkEnded = new TalkEventManager.TalkEvent (OnTalkEnded);
			TalkEventManager.TalkEnded += onTalkEnded;

			onBlowBegin = new MicEventManager.MicEvent (OnBlowBegin);
			MicEventManager.BlowBegin += onBlowBegin;

			onBackdoorToggle = new BackdoorEventManager.BackdoorEvent (OnBackdoorToggle);
			BackdoorEventManager.BackdoorToggle += onBackdoorToggle;
		}

		void Update() {
			if (!hasClicked && Input.GetMouseButtonDown (0)) { 
				StartCoroutine (TimeWaiter ());

				hasClicked = true;
			}
		}

		IEnumerator TimeWaiter() {
			yield return new WaitForSeconds (.5f);
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

		void OnBackdoorToggle(BackdoorEventArgs e) {
			if (e.ID == "leafsBlow") {
				OnBlowBegin (null);
			}
		}

		IEnumerator Insist() {
			yield return new WaitForSeconds (6f);
			if(insist)
				TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID = "piri", AudioClipId = (insistCpt % 3)+3, Autoplay = true });
			insistCpt++;
		}

		void ToggleBunny() {
			Bunny.CurrentAnimationState = Bunny.STATE_VISIBLE;
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
		
		void OnBlowBegin(MicEventArgs eventArgs) {
			if (CurrentAnimationState == STATE_OPEN_SMALL) {
				CurrentAnimationState = STATE_OPEN_WIDE;
			}
		}

		void OnDestroy() {
			TalkEventManager.TalkEnded -= onTalkEnded;
			MicEventManager.BlowBegin -= onBlowBegin;
			BackdoorEventManager.BackdoorToggle -= onBackdoorToggle;
		}
	}
}