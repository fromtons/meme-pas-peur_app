using UnityEngine;
using System.Collections;

namespace MPP.Events {
	public static class SceneEventManager {

		public delegate void SceneEvent(SceneEventArgs eventArgs);

		public static event SceneEvent SceneLoad, SceneLoaded;

		public static void TriggerSceneLoad(SceneEventArgs eventArgs = null) {
			if (SceneLoad != null)
				SceneLoad (eventArgs);           

			Debug.Log ("Scene Load : " + eventArgs.Name);
		}

		public static void TriggerSceneLoaded(SceneEventArgs eventArgs = null) {
			if (SceneLoaded != null)
				SceneLoaded (eventArgs);

			Debug.Log ("Scene Loaded : "+eventArgs.Name);
		}
	}
}