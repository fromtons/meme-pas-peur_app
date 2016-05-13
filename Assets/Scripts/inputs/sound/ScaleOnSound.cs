using UnityEngine;
using System.Collections;

namespace MPP.Inputs._Sound {
	public class ScaleOnSound : MonoBehaviour {

		public MicrophoneInput micIn;
		public float minValue = 0.5f;
		public float maxValue = 10f;

		public float outputMinValue = 0.04f;
		public float outputMaxValue = 0.2f;


		float valueToReach;
		float currentValue;
		float factor = 0.05f;

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {

			Debug.Log (valueToReach);
			Debug.Log (currentValue);

			float minimisedValue = micIn.loudness - minValue;
			if (minimisedValue < 0)
				minimisedValue = 0;
			else if (minimisedValue > (maxValue-minValue))
				minimisedValue = maxValue-minValue;

			valueToReach = outputMinValue + ((outputMaxValue-outputMinValue) * minimisedValue / (maxValue - minValue));
		

			float diff = Mathf.Abs(valueToReach - currentValue);
			float BITE = diff / outputMaxValue;

			currentValue+= factor * Mathf.Sign(valueToReach-currentValue) * BITE;
			if (currentValue > outputMaxValue)
				currentValue = outputMaxValue;
			else if (currentValue < outputMinValue)
				currentValue = outputMinValue;

			this.transform.localScale = new Vector3(currentValue,currentValue,currentValue);
		}
	}
}
 