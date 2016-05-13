using UnityEngine;
using System.Collections;

namespace MPP.Events {
	public static class TalkEventManager {

		public delegate void TalkEvent(TalkEventArgs eventArgs);

		public static event TalkEvent TalkBegin, TalkEnded, TalkSet, TalkStop;

		public static void TriggerTalkBegin(TalkEventArgs eventArgs = null) {
			if (TalkBegin != null)
				TalkBegin (eventArgs);
		}

		public static void TriggerTalkEnded(TalkEventArgs eventArgs = null) {
			if (TalkEnded != null)
				TalkEnded (eventArgs);
		}

		public static void TriggerTalkSet(TalkEventArgs eventArgs = null) {
			if (TalkSet != null)
				TalkSet (eventArgs);
		}

		public static void TriggerTalkStop(TalkEventArgs eventArgs = null) {
			if (TalkStop != null)
				TalkStop (eventArgs);
		}
	}
}
