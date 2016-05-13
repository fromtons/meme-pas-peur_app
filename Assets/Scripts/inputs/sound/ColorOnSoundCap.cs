using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MPP.Inputs._Sound {
	public class ColorOnSoundCap : MonoBehaviour {

		public MicrophoneInput micIn;
		RawImage rawImage;

		public Color colorOn = Color.red;
		public Color colorOff = Color.grey;

		public float treshold;

		// Use this for initialization
		void Start () {
			rawImage = this.GetComponent<RawImage> ();
		}
		
		// Update is called once per frame
		void Update () {
			rawImage.color = micIn.loudness > treshold ? colorOn : colorOff;
		}
	}
}