using UnityEngine;
using System.Collections;

namespace MPP.Events {
	public static class BackdoorEventManager {

		public delegate void BackdoorEvent(BackdoorEventArgs eventArgs);

		public static event BackdoorEvent BackdoorToggle;

		public static void TriggerBackdoorToggle(BackdoorEventArgs eventArgs = null) {
			if (BackdoorToggle != null)
				BackdoorToggle (eventArgs);           
		}
	}
}