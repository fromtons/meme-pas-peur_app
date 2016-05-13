using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

namespace MPP.Forest.Scene_04 {
	public class MainCamera : MonoBehaviour {

		public AudioClip ambientLight;
		AudioSource audioSource;
		ColorCorrectionCurves ccc;

		// Use this for initialization
		void Start () {
			ccc = (ColorCorrectionCurves) GetComponent(typeof(ColorCorrectionCurves));
			audioSource = GetComponents<AudioSource> () [0];
		}

		public void toggleLight() {
			disableGreyscale ();
			enableLightSounds();
		}

		private void enableLightSounds() {
			audioSource.clip = ambientLight;
			audioSource.time = 0;
			audioSource.Play ();
		}

		public void disableGreyscale() {
			Hashtable tweenParams = new Hashtable();
			tweenParams.Add("from", ccc.saturation);
			tweenParams.Add("to", 1f);
			tweenParams.Add("time", 3);
			tweenParams.Add("onupdate", "OnGreyscaleUpdated");

			iTween.ValueTo(gameObject, tweenParams);
		}

		private void OnGreyscaleUpdated(float saturation) {
			ccc.saturation = saturation;
		}
	}
}