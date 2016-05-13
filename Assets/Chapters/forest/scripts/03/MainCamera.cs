using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

namespace MPP.Forest.Scene_03 {
	public class MainCamera : MonoBehaviour {
		public float moveToX;
		public float moveToY;
		public float duration = 25f;

		// Use this for initialization
		void Start () {
			iTween.MoveTo(gameObject, iTween.Hash("position", new Vector3(moveToX,moveToY, -10f), "time", duration, "easetype", iTween.EaseType.easeInOutSine, "oncomplete", "OnIntroComplete"));
		}

		void OnIntroComplete() {
			Debug.Log ("intro complete");
		} 
	}
}