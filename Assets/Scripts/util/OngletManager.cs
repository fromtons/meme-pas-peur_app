using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace MPP.Util {
	public class OngletManager : MonoBehaviour {

		public static OngletManager instance = null;

		public int currentOnglet = 0;
		public List<string> scenesToLoad;
		public GameObject prefab;
		public float highlightDelay = 4f; 

		RectTransform rt;

		float canvasWidth;
		float ongletsSize;

		Onglet nextOnglet;
		AudioSource audioSource;

		void Awake() {
			audioSource = this.GetComponent<AudioSource> ();

			if (!instance)
				instance = this;
			else
				Destroy (gameObject);
		}

		// Use this for initialization
		void Start () {
			rt = GetComponent<RectTransform> ();
			canvasWidth = rt.rect.width;
			ongletsSize = canvasWidth / scenesToLoad.Count;

			for (int i = 0; i < scenesToLoad.Count; i++) {
				// Get the onglet
				GameObject onglet = (GameObject) Instantiate(prefab);
				Onglet ongletScript = (Onglet)onglet.GetComponent<Onglet> ();
				RectTransform ongletRt = ((RectTransform)onglet.GetComponent<RectTransform> ());
				SceneLoader ongletSl = ((SceneLoader) onglet.GetComponent<SceneLoader> ());
				UnityEngine.UI.Text ongletText = onglet.GetComponent<Onglet> ().textObject.GetComponent<UnityEngine.UI.Text> ();
				RectTransform ongletTextRt = ((RectTransform)(ongletText.GetComponent<RectTransform> ()));

				// Move it along
				// Pastille
				onglet.transform.SetParent(this.transform);
				ongletRt.localScale = new Vector3 (1f, 1f, 1f); // Reset scale
				ongletRt.sizeDelta = new Vector2 (ongletsSize, ongletsSize); // Size calculation
				ongletRt.anchoredPosition = new Vector2 ((ongletsSize*i)+ongletsSize/2, 0); // Position calculation
				// Text
				ongletText.text = ""+(i+1);
				ongletTextRt.sizeDelta = new Vector2 (ongletsSize, ongletsSize / 2); // Size calculation
				ongletTextRt.anchoredPosition = new Vector2 (0, ongletsSize / 4); // Position calculation
				ongletSl.sceneToLoad = scenesToLoad [i];

				if (i == currentOnglet)
					ongletScript.CurrentState = Onglet.STATE_CURRENT;
				else 
					ongletScript.CurrentState = Onglet.STATE_DEFAULT;	

				if (i == currentOnglet + 1)
					nextOnglet = ongletScript;
			}
		}

		public void HighlightNextOnglet() {
			StartCoroutine(RealHighlightNextOnglet());	
		}
		
		IEnumerator RealHighlightNextOnglet() {
			
			yield return new WaitForSeconds(highlightDelay);
			
			audioSource.time = 0f;
			audioSource.Play ();
			nextOnglet.GetComponent<Onglet> ().CurrentState = Onglet.STATE_HIGHLIGHT;
		}
	}
}