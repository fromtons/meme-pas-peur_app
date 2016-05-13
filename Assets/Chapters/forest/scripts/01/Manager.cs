using UnityEngine;
using System.Collections;

namespace MPP.Forest.Scene_01 {
	
	public class Manager : MonoBehaviour {

		public float delay = 13f;

		// Use this for initialization
		void Start () {
			StartCoroutine(DelayedHighlight());
		}
		
		IEnumerator DelayedHighlight() {
			yield return new WaitForSeconds(delay);
			OngletManager.instance.HighlightNextOnglet();
		}
	}

}
