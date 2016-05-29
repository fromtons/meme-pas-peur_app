using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MPP.Util.UI {

	[RequireComponent (typeof(CanvasGroup))]
	public class GroupFade : MonoBehaviour {

		public CanvasGroup _canvasGroup;
		protected float _hideFadeDelay = 0f;

		// Use this for initialization
		void Start () {
			if(_canvasGroup == null)
				_canvasGroup = this.GetComponent<CanvasGroup> ();
		}

		void OnUpdate(float value) {
			_canvasGroup.alpha = value;
		}
		
		public virtual void Show(float duration) {
			this.gameObject.SetActive (true);
			iTween.Stop (gameObject);

			Hashtable ht = new Hashtable ();
			ht.Add ("from", 0f);
			ht.Add ("to", 1f);
			ht.Add ("time", duration);
			ht.Add ("onupdate", "OnUpdate");
			ht.Add ("onupdatetarget", this.gameObject);
			ht.Add ("easetype", iTween.EaseType.easeInOutSine);
			iTween.ValueTo (this.gameObject, ht);
		}

		public virtual void Hide(float duration) {
			iTween.Stop (gameObject);

			Hashtable ht = new Hashtable ();
			ht.Add ("from", 1f);
			ht.Add ("to", 0f);
			ht.Add ("time", duration);
			ht.Add ("delay", _hideFadeDelay);
			ht.Add ("onupdate", "OnUpdate");
			ht.Add ("onupdatetarget", this.gameObject);
			ht.Add ("oncomplete", "OnHideComplete");
			ht.Add ("oncompletetarget", this.gameObject);
			ht.Add ("easetype", iTween.EaseType.easeInOutSine);
			iTween.ValueTo (this.gameObject, ht);
		}

		void OnHideComplete() {
			this.gameObject.SetActive (false);
		}
	}
}