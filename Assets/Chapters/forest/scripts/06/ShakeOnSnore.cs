using UnityEngine;
using System.Collections;
using MPP.Util;

namespace MPP.Forest.Scene_06 {

	[RequireComponent (typeof(Shaky))]
	public class ShakeOnSnore : MonoBehaviour {

		public Wolf wolfToListen;
		Shaky shakyScript;

		// Use this for initialization
		void Start () {
			shakyScript = GetComponent<Shaky> ();
		}
		
		// Update is called once per frame
		void Update () {
			shakyScript.intensity = wolfToListen.shakeFactor;
		}
	}
}