using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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

		string _levelToLoad;
		public string LevelToLoad {
			set {
				_levelToLoad = value;
			}
		}

		bool _inEnded = false;

		void Start() {
			DontDestroyOnLoad (this.gameObject);

			Debug.Log ("Start loadingwrapper");
			loadingObject._canvasGroup.alpha = 0;

			gameObject.SetActive (false);
		}

		public void Launch() {
			gameObject.SetActive (true);
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

		void LoadHeavyAsync() {
			_async = SceneManager.LoadSceneAsync (_levelToLoad);
			_async.allowSceneActivation = true;
		}

		IEnumerator OnInEnd() {
			yield return new WaitForSeconds (_inDuration);
			if (_async != null)
				_async.allowSceneActivation = true;
			else if(_levelToLoad != null)
				LoadHeavyAsync ();
			_inEnded = true;
		}

		IEnumerator OnOutEnd() {
			yield return new WaitForSeconds (_outDuration);
			Destroy (this.gameObject);
		}
	}
}