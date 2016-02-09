using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour {

	public string sceneToLoad;

	GameObject loadingScreen;

	// Use this for initialization
	void Start () {
		loadingScreen = GameObject.Find("loading");
		loadingScreen.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		if ((Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended) || (Input.anyKey)) {
			StartCoroutine(DisplayLoadingScreen(sceneToLoad));
		}
	}

	IEnumerator DisplayLoadingScreen(string level) {
		if(loadingScreen) loadingScreen.SetActive(true);

		AsyncOperation async = SceneManager.LoadSceneAsync (level);
		while (!async.isDone) {
			yield return null;
		}
	}
}