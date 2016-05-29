using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MPP.Util.UI {
	public class TransitionCircleMask : GroupFade {

		public Image circle;

		public RawImage topOffset;
		public RawImage rightOffset;
		public RawImage bottomOffset;
		public RawImage leftOffset;

		static float SCREEN_WIDTH;
		static float SCREEN_HEIGHT;
		static float SCREEN_DIAGONAL;
		static float OPENED_DIAMETER;
		static float CLOSED_DIAMETER;

		public RectTransform _circleRt;
		public RectTransform _topOffsetRt;
		public RectTransform _rightOffsetRt;
		public RectTransform _bottomOffsetRt;
		public RectTransform _leftOffsetRt;

		void Awake() {
			SCREEN_WIDTH = 768+1;
			SCREEN_HEIGHT = 1024+1;
			SCREEN_DIAGONAL = Mathf.Sqrt (Mathf.Pow(SCREEN_WIDTH,2) + Mathf.Pow(SCREEN_HEIGHT, 2));
			OPENED_DIAMETER = SCREEN_DIAGONAL * 2;
			CLOSED_DIAMETER = SCREEN_WIDTH / 10;
		}

		public override void Show(float duration) {
			if(_canvasGroup != null) _canvasGroup.alpha = 1f;
			iTween.Stop (gameObject);

			Hashtable ht = new Hashtable ();
			ht.Add ("from", OPENED_DIAMETER);
			ht.Add ("to", CLOSED_DIAMETER);
			ht.Add ("time", duration);
			ht.Add ("onupdate", "OnCircleUpdate");
			ht.Add ("onupdatetarget", this.gameObject);
			ht.Add ("easetype", iTween.EaseType.easeInOutSine);
			iTween.ValueTo (this.gameObject, ht);
		}

		public override void Hide(float duration) {
			if(_canvasGroup != null) _canvasGroup.alpha = 1f;
			iTween.Stop (gameObject);

			Hashtable ht = new Hashtable ();
			ht.Add ("from", CLOSED_DIAMETER);
			ht.Add ("to", OPENED_DIAMETER);
			ht.Add ("time", duration);
			ht.Add ("onupdate", "OnCircleUpdate");
			ht.Add ("onupdatetarget", this.gameObject);
			ht.Add ("easetype", iTween.EaseType.easeInOutSine);
			iTween.ValueTo (this.gameObject, ht);
		}

		public override void SetOrigin (RectTransform value)
		{
			base.SetOrigin (value);
			_circleRt.localPosition = new Vector3 (value.localPosition.x, _circleRt.localPosition.y, _circleRt.localPosition.z);;
		}

		void OnCircleUpdate(float value) {
			_circleRt.sizeDelta = new Vector2 (value, value);
		
			_topOffsetRt.sizeDelta = new Vector2 (_topOffsetRt.sizeDelta.x, SCREEN_HEIGHT/2 - _circleRt.localPosition.y - (_circleRt.sizeDelta.y/2));
			_bottomOffsetRt.sizeDelta = new Vector2 (_bottomOffsetRt.sizeDelta.x, SCREEN_HEIGHT/2 + _circleRt.localPosition.y - (_circleRt.sizeDelta.y/2));

			_rightOffsetRt.sizeDelta = new Vector2 (SCREEN_WIDTH/2 - _circleRt.localPosition.x - (_circleRt.sizeDelta.x/2), _rightOffsetRt.sizeDelta.y);
			_leftOffsetRt.sizeDelta = new Vector2 (SCREEN_WIDTH/2 + _circleRt.localPosition.x - (_circleRt.sizeDelta.x/2), _leftOffsetRt.sizeDelta.y);
		}
	}
}