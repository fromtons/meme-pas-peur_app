﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using MPP.Util.UI;
using MPP.Events;

namespace MPP.Util {
	public class SceneLoader : MonoBehaviour {

		public string sceneToLoad;
		public bool disableMouseButtonDown = false;
		public bool heavyLoad = true;
		public float delay = 0f;
		public float inDuration = 0.6f;
		public float outDuration = 0.4f;

		BoxCollider2D collider;
		public GameObject prefabLoading;

		LoadingWrapper _loading;

		// Use this for initialization
		void Start () {
			if (prefabLoading != null) {
				_loading = Instantiate (prefabLoading).GetComponent<LoadingWrapper> ();
				_loading.InDuration = inDuration;
				_loading.OutDuration = outDuration;
			}
			collider = GetComponent<BoxCollider2D> ();
		}

		// Update is called once per frame
		void Update () {
			if (Input.GetMouseButtonDown(0) && !disableMouseButtonDown) {
				Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);
				if (hitCollider == collider) {
					StartCoroutine (DisplayLoadingScreen (sceneToLoad));
				}
			}
		}

		public void SetOrigin(RectTransform value) {
			_loading.loadingObject.SetOrigin (value);
		}

		IEnumerator DisplayLoadingScreen(string level) {
			AsyncOperation async = null;
			if (!heavyLoad)
				async = SceneManager.LoadSceneAsync (level);

			if (prefabLoading != null) {
				_loading.Launch ();

				if (!heavyLoad && async != null) {
					async.allowSceneActivation = false;
					_loading.Async = async;
				} else {
					// HEAVY LOADING
					_loading.LevelToLoad = level;
				}
			} else if(heavyLoad) {
				Debug.Log ("No prefab loading given !");
				async = SceneManager.LoadSceneAsync (level);
			}

			SceneEventManager.TriggerSceneLoad (new SceneEventArgs { Name = level });
			if (MixerManager.instance != null) {
				MixerManager.instance.FadeTo ("AmbientVol", -80f, 0.5f);
				MixerManager.instance.FadeTo ("VoicesVol", -80f, 0.5f);
			}

			yield return new WaitForSeconds(delay);

			if (async != null) {
				while (!async.isDone) {
					yield return null;
				}
			}
		}

		public void ManuallyLoadScene() {
			StartCoroutine(DisplayLoadingScreen(sceneToLoad));
		}
	}
}