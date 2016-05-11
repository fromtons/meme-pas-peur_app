using UnityEngine;
using System.Collections;

public class Scene_01_Manager : MonoBehaviour {

	public float delay = 13f;

	// Use this for initialization
	void Start () {
		StartCoroutine(DelayedHighlight());
	}
	
	IEnumerator DelayedHighlight() {
		yield return new WaitForSeconds(delay);
		OngletManager.instance.HighlightNextOnglet();
	}
}
