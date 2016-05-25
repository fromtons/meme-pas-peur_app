using UnityEngine;
using System.Collections;

namespace MPP.Forest.Scene_06 {
	public static class WolfEventManager {

		public delegate void WolfEvent(WolfEventArgs eventArgs);

		public static event WolfEvent WolfChangeState;

		public static void TriggerWolfChangeState(WolfEventArgs eventArgs = null) {
			if (WolfChangeState != null)
				WolfChangeState (eventArgs);
		}
	}
}
