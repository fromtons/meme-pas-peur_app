using UnityEngine;
using System.Collections;

namespace MPP.Util {
	public class EnableObject : MonoBehaviour {

		public GameObject target;

		public void toggleTarget() {
			if(!target.activeSelf)
				target.SetActive (true);
			else 
				target.SetActive (false);
		}
	}
}