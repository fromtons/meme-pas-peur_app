using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MPP.Menus.Profile {
	public class ProfileHeadStep : MonoBehaviour {

		public GameObject toHide;
		public Text text;
		public RawImage progressLine;
		public Color progressLineDefaultColor;
		public Color progressLineCompletedColor;
		public Color textDefaultColor;
		public Color textCompletedColor;

		public string accroche;
		public string sousAccroche;

		RectTransform _rt;

		bool _current = false;
		public bool Current {
			get {
				return _current;
			} 
			set {
				_current = value;
				toHide.SetActive (!_current);
				if (_current) {
					
					text.color = textCompletedColor;
				} else {
					text.color = textDefaultColor;
				}
			}
		}
		bool _completedOnce;
		public bool CompletedOnce {
			set {
				_completedOnce = value;
				progressLine.color = progressLineCompletedColor;
			}
		}

		void OnEnable() {
			progressLine.color = progressLineDefaultColor;
			ShowProgressLine ();
		}

		public void HideProgressLine() {
			progressLine.gameObject.SetActive (false);
		}

		public void ShowProgressLine() {
			progressLine.gameObject.SetActive (true);
		}
	}
}