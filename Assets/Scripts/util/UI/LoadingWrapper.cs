using UnityEngine;
using System.Collections;

namespace MPP.Util.UI {
	public class LoadingWrapper : MonoBehaviour {

		public GroupFade loadingObject;

		float _inDuration;
		float _outDuration;
		public float InDuration { set { _inDuration = value; }}
		public float OutDuration { set { _outDuration = value; }}

		AsyncOperation _async;
		public AsyncOperation Async {
			set { _async = value; }
		}

		bool _inEnded = false;

		void Start() {
			DontDestroyOnLoad (this.gameObject);

			loadingObject._canvasGroup.alpha = 0;
			loadingObject.Show (_inDuration);
			StartCoroutine (OnInEnd ());
		}

		void Update() {
			if (_inEnded && _async != null && _async.isDone) {
				_async = null;

				loadingObject.Hide (_outDuration);
				StartCoroutine (OnOutEnd());
			}
		}

		IEnumerator OnInEnd() {
			yield return new WaitForSeconds (_inDuration);
			_inEnded = true;
			if(_async != null) _async.allowSceneActivation = true;
		}

		IEnumerator OnOutEnd() {
			yield return new WaitForSeconds (_outDuration);
			Destroy (this.gameObject);
		}
	}
}