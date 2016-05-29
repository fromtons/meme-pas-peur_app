using UnityEngine;
using System.Collections;

namespace MPP.Util.UI {
	public class GroupRightToLeftMask : GroupFade {
		public GameObject contentToMove;
	
		public override void Show(float duration) {
			base.Show(0f);
		
			Debug.Log ("[Show] screen width: " + Screen.width);

			Hashtable ht = new Hashtable ();
			ht.Add ("from", Screen.width); // TODO - Find another way than Screen.width to get width of our mask
			ht.Add ("to", 0f);
			ht.Add ("time", duration);
			ht.Add ("onupdate", "OnContentUpdate");
			ht.Add ("onupdatetarget", this.gameObject);
			ht.Add ("easetype", iTween.EaseType.easeOutExpo);
			iTween.ValueTo (this.gameObject, ht);
		}

		public override void Hide(float duration) {
			_hideFadeDelay = duration;

			Debug.Log ("[Hide] screen width: " + Screen.width);

			Hashtable ht = new Hashtable ();
			ht.Add ("from", 0f);
			ht.Add ("to", -Screen.width); // TODO - Find another way than Screen.width to get width of our mask
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