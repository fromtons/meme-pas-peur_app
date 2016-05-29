using UnityEngine;
using System.Collections;

namespace MPP.Util.UI {
	public class GroupRightToLeftMask : GroupFade {

		public GameObject contentToMove;

		public override void Show(float duration) {
			base.Show(0f);

			Hashtable ht = new Hashtable ();
			ht.Add ("from", ((this.transform as RectTransform).parent.transform as RectTransform).sizeDelta.x);
			ht.Add ("to", 0f);
			ht.Add ("time", duration);
			ht.Add ("onupdate", "OnContentUpdate");
			ht.Add ("onupdatetarget", this.gameObject);
			ht.Add ("easetype", iTween.EaseType.easeOutExpo);
			iTween.ValueTo (this.gameObject, ht);
		}

		public override void Hide(float duration) {
			_hideFadeDelay = duration;

			Hashtable ht = new Hashtable ();
			ht.Add ("from", 0f);
			ht.Add ("to", -((this.transform as RectTransform).parent.transform as RectTransform).sizeDelta.x);
			ht.Add ("time", duration);
			ht.Add ("onupdate", "OnContentUpdate");
			ht.Add ("onupdatetarget", this.gameObject);
			ht.Add ("oncomplete", "OnHideComplete");
			ht.Add ("oncompletetarget", this.gameObject);
			ht.Add ("easetype", iTween.EaseType.easeInExpo);
			iTween.ValueTo (this.gameObject, ht);
		}

		void OnContentUpdate(float value) {
			this.transform.localPosition = new Vector2 (value, this.transform.localPosition.y);
			contentToMove.transform.localPosition = new Vector2 (-value, contentToMove.transform.localPosition.y);
		}
	}
}