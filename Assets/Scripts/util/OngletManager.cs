using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class OngletManager : MonoBehaviour {

	public int currentOnglet = 0;
	public List<string> scenesToLoad;
	public GameObject prefab;

	RectTransform rt;

	float canvasWidth;
	float ongletsSize;

	// Use this for initialization
	void Start () {
		rt = GetComponent<RectTransform> ();
		Debug.Log (rt.rect.width);
		canvasWidth = rt.rect.width;
		ongletsSize = canvasWidth / scenesToLoad.Count;

		for (int i = 0; i < scenesToLoad.Count; i++) {
			// Get the onglet
			GameObject onglet = (GameObject) Instantiate(prefab);
			RectTransform ongletRt = ((RectTransform)onglet.GetComponent<RectTransform> ());
			SceneLoader ongletSl = ((SceneLoader) onglet.GetComponent<SceneLoader> ());
			UnityEngine.UI.Text ongletText = onglet.GetComponent<Onglet> ().textObject.GetComponent<UnityEngine.UI.Text> ();
			RectTransform ongletTextRt = ((RectTransform)(ongletText.GetComponent<RectTransform> ()));

			// Move it along
			// Pastille
			onglet.transform.parent = this.transform;
			ongletRt.localScale = new Vector3 (1f, 1f, 1f); // Reset scale
			ongletRt.sizeDelta = new Vector2 (ongletsSize, ongletsSize); // Size calculation
			ongletRt.anchoredPosition = new Vector2 ((ongletsSize*i)+ongletsSize/2, 0); // Position calculation
			// Text
			ongletText.text = ""+(i+1);
			ongletTextRt.sizeDelta = new Vector2 (ongletsSize, ongletsSize / 2); // Size calculation
			ongletTextRt.anchoredPosition = new Vector2 (0, ongletsSize / 4); // Position calculation
			ongletSl.sceneToLoad = scenesToLoad [i];
			if (i != currentOnglet)
				onglet.GetComponent<Image> ().color = new Color (1f,1f,1f,.5f);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
