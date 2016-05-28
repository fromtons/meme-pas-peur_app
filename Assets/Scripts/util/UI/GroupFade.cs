using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MPP.Util.UI {

	[RequireComponent (typeof(CanvasGroup))]
	public class GroupFade : MonoBehaviour {

		CanvasGroup _canvasGroup;

		// Use this for initialization
		void Start () {
			_canvasGroup = this.GetComponent<CanvasGroup> ();
		}

		void OnUpdate(float value) {
			_canvasGroup.alpha = value;
		}
		
		public void Show(float duration) {
			Hashtable ht = new Hashtable ();
			ht.Add ("from", 0f);
			ht.Add ("to", 1f);
			ht.Add ("time", duration);
			ht.Add ("onupdate", "OnUpdate");
			ht.Add ("onupdatetarget", this.gameObject);
			ht.Add ("easetype", iTween.EaseType.easeInOutExpo);
			iTween.ValueTo (this.gameObject, ht);
		}

		public void Hide(float duration) {
			Hashtable ht = new Hashtable ();
			ht.Add ("from", 1f);
			ht.Add ("to", 0f);
			ht.Add ("time", duration);
			ht.Add ("onupdate", "OnUpdate");
			ht.Add ("onupdatetarget", this.gameObject);
			ht.Add ("easetype", iTween.EaseType.easeInOutExpo);
			iTween.ValueTo (this.gameObject, ht);
		}
	}
}