using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MPP.Util.UI {

	[RequireComponent (typeof(RectTransform))]
	public class Floating : MonoBehaviour {

		RectTransform _rT;

		Vector2 _basePosition;
		public Vector2 transform;
		public float delta = 20;

		// Use this for initialization
		void Start () {
			_rT = this.GetComponent<RectTransform> ();
			_basePosition = _rT.anchoredPosition;
		}
		
		// Update is called once per frame
		void Update () {
			_rT.anchoredPosition = new Vector2(_basePosition.x + delta * Input.gyro.gravity.x, _basePosition.y + delta * Input.gyro.gravity.y);
		}
	}
}