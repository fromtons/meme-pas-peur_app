using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour {

	public float delay = 0f;
	public string sceneToLoad;

	bool listening = false;

	// Use this for initialization
	void Start () {
		StartCoroutine(startListening());
	}

	// Update is called once per frame
	void Update () {
		if(listening && Input.anyKey) {
			Application.LoadLevel(sceneToLoad);
		}

		if (listening && Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended) {
			Application.LoadLevel(sceneToLoad);
		}
	}

	IEnumerator startListening() {       
		yield return new WaitForSeconds(delay);
		listening = true;
	}
}