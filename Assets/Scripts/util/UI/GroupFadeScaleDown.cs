using UnityEngine;
using System.Collections;

namespace MPP.Util.UI {
	public class GroupFadeScaleDown : GroupFade {

		public override void Show(float duration) {
			base.Show(duration);

			this.gameObject.transform.localScale = new Vector3 (1.2f,1.2f,1.2f);
			iTween.ScaleTo(gameObject, iTween.Hash("scale", new Vector3(1f,1f,1f), "time", duration, "easetype", iTween.EaseType.easeOutExpo));
		}

		public override void Hide(float duration) {
			base.Hide(duration);

			this.gameObject.transform.localScale = new Vector3 (1f,1f,1f);
			iTween.ScaleTo(gameObject, iTween.Hash("scale", new Vector3(1.2f,1.2f,1.2f), "time", duration, "easetype", iTween.EaseType.easeInExpo));
		}
	}
}