using UnityEngine;
using System.Collections;

namespace MPP.Forest.Scene_04 {
	public static class LuciolesEventManager {

		public delegate void LuciolesEvent();

		public static event LuciolesEvent LuciolesLightened, LuciolesCheck;

		public static void TriggerLuciolesLightened() {
			if (LuciolesLightened != null)
				LuciolesLightened ();
		}

		public static void TriggerLuciolesCheck() {
			if (LuciolesCheck != null)
				LuciolesCheck ();
		}
	}
}
