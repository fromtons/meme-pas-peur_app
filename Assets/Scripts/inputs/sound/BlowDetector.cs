using UnityEngine;
using System.Collections;
using MPP.Events;

namespace MPP.Inputs._Sound {
	public class BlowDetector : MonoBehaviour {

		public static BlowDetector instance = null;
		public MicrophoneInput micIn;

		static float TRESHOLD_EDITOR = 35f;
		static float TRESHOLD_IOS = 20f;
		static float TRESHOLD_DEFAULT = 20f;

		static float TRESHOLD;

		bool wasAbove = false;

		// Use this for initialization
		void Start () {

			#if UNITY_EDITOR
				TRESHOLD = TRESHOLD_EDITOR;
			#elif UNITY_IOS
				TRESHOLD = TRESHOLD_IOS;
			#else
				TRESHOLD = TRESHOLD_DEFAULT;
			#endif


			if (!instance)
				instance = this;
			else
				Destroy (gameObject);
		}
		
		// Update is called once per frame
		void Update () {
			if(BlowCheck()) {
				if(!wasAbove) {
					MicEventManager.TriggerBlowBegin(new MicEventArgs { });
					wasAbove = true;	
				}

				MicEventManager.TriggerBlow(new MicEventArgs { });
			} else if(wasAbove) {
				MicEventManager.TriggerBlowEnd(new MicEventArgs { });
				wasAbove = false;
			}
		}

		bool BlowCheck() {   
			return (micIn.GetAvgFrequencies(true) > TRESHOLD);
		}
	}
}