using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MPP.Forest.Scene_04 {
	public class Manager : MonoBehaviour {

		public static int nbOfLuciolesToCheck = 3;
		public static int nbOfLuciolesChecked = 0;
		public static int introAnimationDuration = 25;

		public MusicLoop musicLoop;

		public GameObject background;
		public GameObject foreground;

		LuciolesEventManager.LuciolesEvent onLuciolesCheck;
		LuciolesEventManager.LuciolesEvent onLuciolesLightened;

		// Use this for initialization
		void Start () {
			nbOfLuciolesChecked = 0;

			onLuciolesCheck = new LuciolesEventManager.LuciolesEvent (OnLuciolesCheck);
			LuciolesEventManager.LuciolesCheck += onLuciolesCheck;

			onLuciolesLightened = new LuciolesEventManager.LuciolesEvent (OnLuciolesLightened);
			LuciolesEventManager.LuciolesLightened += onLuciolesLightened;
		}

		void OnLuciolesCheck() {
			nbOfLuciolesChecked++;
			if (nbOfLuciolesChecked >= nbOfLuciolesToCheck) 
				LuciolesEventManager.TriggerLuciolesLightened ();
		}

		void OnLuciolesLightened() {
			// Lightens the background (may not be useful in the future, we'll see)
			BackgroundManager bgMngr = (BackgroundManager) background.GetComponent(typeof(BackgroundManager));
			bgMngr.ChangeColor (new Color(1f,1f,1f,1f));
			BackgroundManager fgMngr = (BackgroundManager) foreground.GetComponent(typeof(BackgroundManager));
			fgMngr.ChangeColor (new Color(1f,1f,1f,1f));

			// Notifies the camera manager in order to disable some filters
			MainCamera cameraManager = (MainCamera) this.GetComponent(typeof(MainCamera));
			cameraManager.toggleLight();

			StartCoroutine (ChangeMusic());
		}

		IEnumerator ChangeMusic() {
			if (MixerManager.instance != null)
				MixerManager.instance.FadeTo ("MusicVol", -80f, 0.5f);

			yield return new WaitForSeconds (0.5f);
			musicLoop.Stop ();
			MixerManager.instance.FadeTo ("MusicVol", 0f, 0f);
			this.GetComponent<MainCamera> ().enableLightSounds ();
		}

		void OnDestroy() {
			LuciolesEventManager.LuciolesCheck -= onLuciolesCheck;
			LuciolesEventManager.LuciolesLightened -= onLuciolesLightened;
		}
	}
}
