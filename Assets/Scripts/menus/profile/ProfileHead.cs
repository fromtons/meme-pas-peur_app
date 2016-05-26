using UnityEngine;
using System.Collections;

namespace MPP.Menus.Profile {
	public class ProfileHead : MonoBehaviour {

		Vector2 originPos;
		Vector2 originSize;

		bool _inTransition = false;

		// Use this for initialization
		void Start () {
			originPos = ((RectTransform)this.transform).anchoredPosition;
			originSize = ((RectTransform)this.transform).sizeDelta;
		}
		
		public void Show() {
			if (_inTransition)
				return;

			Hashtable ht = new Hashtable ();
			ht.Add ("from", ((RectTransform)this.transform).anchoredPosition.y);
			ht.Add ("to", originPos.y);
			ht.Add ("time", 1f);
			ht.Add ("onupdate", "OnVerticalPositionUpdate");
			ht.Add ("onupdatetarget", this.gameObject);
			ht.Add ("oncomplete", "OnTransitionComplete");
			ht.Add ("oncompletetarget", this.gameObject);
			ht.Add ("easetype", iTween.EaseType.easeOutExpo);
			iTween.ValueTo (this.gameObject, ht);

			_inTransition = true;
		}

		public void Hide() {
			if (_inTransition)
				return;

			Hashtable ht = new Hashtable ();
			ht.Add ("from", originPos.y);
			ht.Add ("to", originPos.y+originSize.y);
			ht.Add ("time", 1f);
			ht.Add ("onupdate", "OnVerticalPositionUpdate");
			ht.Add ("onupdatetarget", this.gameObject);
			ht.Add ("oncomplete", "OnTransitionComplete");
			ht.Add ("oncompletetarget", this.gameObject);
			ht.Add ("easetype", iTween.EaseType.easeOutExpo);
			iTween.ValueTo (this.gameObject, ht);

			_inTransition = true;
		}

		void OnVerticalPositionUpdate(float value) {
			((RectTransform) this.transform).anchoredPosition = new Vector2 (((RectTransform) this.transform).anchoredPosition.x, value);
		}

		void OnTransitionComplete() {
			_inTransition = false;
		}
	}
}