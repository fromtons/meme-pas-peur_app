using UnityEngine;
using System.Collections;

namespace MPP.Inputs {
	public class MagnetoMeter : MonoBehaviour {
		void OnGUI() {
			Input.location.Start(); 
			Input.compass.enabled = true; 
			Input.gyro.enabled = true;

			GUILayout.Label("Magnetometer reading: " + Input.compass.rawVector.ToString());
		}
	}
}