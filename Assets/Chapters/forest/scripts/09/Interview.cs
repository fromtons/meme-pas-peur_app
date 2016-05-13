using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MPP.Util;
using MPP.Events;

namespace MPP.Forest.Scene_09 {
	[RequireComponent (typeof(AudioSource))]
	public class Interview : MonoBehaviour {

		public GameObject question1Wrapper;
		public GameObject question2Wrapper;

		AudioSource audioSource;
		bool scared;
		string highlightMoment;

		TalkEventManager.TalkEvent onTalkEnded;

		// Use this for initialization
		void Start () {
			audioSource = this.GetComponent<AudioSource> ();

			question1Wrapper.SetActive (false);
			question2Wrapper.SetActive (false);

			onTalkEnded = new TalkEventManager.TalkEvent (OnTalkEnded);
			TalkEventManager.TalkEnded += onTalkEnded;
			TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID = "brother", AudioClipId = 0, Autoplay = false });
		}

		void OnTalkEnded(TalkEventArgs e) {
			if (e.ID == "piri") {
				switch (e.AudioClipId) {
				case 0:
					TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID = "brother", AudioClipId = 1, Autoplay = true });
					break;
				case 1:
					LaunchInterview ();
					break;
				case 2:
				case 3:
				case 4:
					question1Wrapper.SetActive (false);
					question2Wrapper.SetActive (true);
					break;
				case 5:
				case 6:
					if (highlightMoment == "forest")
						TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID = "piri", AudioClipId = 7, Autoplay = true });
					else if (highlightMoment == "bush")
						TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID = "piri", AudioClipId = 8, Autoplay = true });
					else
						TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID = "piri", AudioClipId = 9, Autoplay = true });
					break;
				case 7:
				case 8:
				case 9:
					OngletManager.instance.HighlightNextOnglet ();
					break;
				}
			} else if (e.ID == "brother") {
				switch (e.AudioClipId) {
				case 0:
					TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID = "piri", AudioClipId = 0, Autoplay = true });
					break;
				case 1:
					TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID = "piri", AudioClipId = 1, Autoplay = true });
					break;
				case 2:
				case 3:
					EndOfInterview ();
					break;
				}
			}
		}

		void LaunchInterview() {
			question1Wrapper.SetActive (true);
			question2Wrapper.SetActive (false);
		}

		public void AnswerQuestion1(string answer) {
			if (answer != "negative")
				scared = true;
			else
				scared = false;

			if (answer == "positive")
				TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID = "piri", AudioClipId = 2, Autoplay = true });
			else if (answer == "negative")
				TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID = "piri", AudioClipId = 3, Autoplay = true });
			else
				TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID = "piri", AudioClipId = 4, Autoplay = true });
		}

		public void AnswerQuestion2(string answer) {
			question1Wrapper.SetActive (false);
			question2Wrapper.SetActive (false);
			highlightMoment = answer;

			if (scared) {
				//Speak (brotherScaredClip);
				TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID = "brother", AudioClipId = 2, Autoplay = true });
			} else {
				//Speak (brotherBraveClip);
				TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID = "brother", AudioClipId = 3, Autoplay = true });
			}
		}

		void EndOfInterview() {
			if (scared)
				TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID = "piri", AudioClipId = 5, Autoplay = true }); // scared piri
			else
				TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID = "piri", AudioClipId = 6, Autoplay = true }); // brave piri
		}

		void OnDestroy() {
			TalkEventManager.TalkEnded -= onTalkEnded;
		}
	}
}