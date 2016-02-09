using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour {

	public string sceneToLoad;
	public bool disableMouseButtonDown = false;

	BoxCollider2D collider;
	public GameObject loadingScreen; 

	// Use this for initialization
	void Start () {

		Debug.Log ("init sceneloader");

		if(loadingScreen) loadingScreen.SetActive (false);
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

	IEnumerator DisplayLoadingScreen(string level) {
		if(loadingScreen) loadingScreen.SetActive(true);

		AsyncOperation async = SceneManager.LoadSceneAsync (level);
		while (!async.isDone) {
			yield return null;
		}
	}

	public void ManuallyLoadScene() {
		Debug.Log ("try to load a scene");
		StartCoroutine(DisplayLoadingScreen(sceneToLoad));
	}
}