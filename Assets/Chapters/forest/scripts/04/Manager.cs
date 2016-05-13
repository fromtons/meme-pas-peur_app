using UnityEngine;
using System.Collections;

namespace MPP.Forest.Scene_04 {
	public class Manager : MonoBehaviour {

		public static int nbOfLuciolesToCheck = 3;
		public static int nbOfLuciolesChecked = 0;
		public static int introAnimationDuration = 25;

		// Use this for initialization
		void Start () {
			nbOfLuciolesChecked = 0;
		}

		public static void checkLuciole() {

			// TODO - Improve these links
			GameObject mainCamera = GameObject.Find("MainCamera");
			GameObject background = GameObject.Find("background");
			GameObject foreground = GameObject.Find("forest_first_plan");

			GameObject hero = GameObject.Find ("piri");

			nbOfLuciolesChecked++;

			if (nbOfLuciolesChecked >= nbOfLuciolesToCheck) {

				// Lightens the background (may not be useful in the future, we'll see)
				BackgroundManager bgMngr = (BackgroundManager) background.GetComponent(typeof(BackgroundManager));
				bgMngr.ChangeColor (new Color(1f,1f,1f,1f));
				BackgroundManager fgMngr = (BackgroundManager) foreground.GetComponent(typeof(BackgroundManager));
				fgMngr.ChangeColor (new Color(1f,1f,1f,1f));

				// Notifies the camera manager in order to disable some filters
				MainCamera cameraManager = (MainCamera) mainCamera.GetComponent(typeof(MainCamera));
				cameraManager.toggleLight();
			
				// Changes piri's state
				((Piri) hero.GetComponent(typeof(Piri))).OnLightenedLucioles();
			}
		}
	}
}
