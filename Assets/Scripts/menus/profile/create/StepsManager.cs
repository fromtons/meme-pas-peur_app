using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace MPP.Menus.Profile {
	public class StepsManager : MonoBehaviour {

		public GameObject headStepsWrapper;
		public GameObject bodyStepsWrapper;

		public Text accrocheText;
		public Text sousAccrocheText;

		public MPP.Util.SceneLoader sceneLoaderBack;

		static int SCREEN_WIDTH = 768;
		static int SCREEN_HEIGHT  = 1024;

		int _currentStep = 0;
		int _nbOfSteps = 0;
		bool inTransition = false;

		Vector2 originalPos;

		List<GameObject> headSteps;
		List<GameObject> bodySteps;

		RectTransform wrapperRt;

		// Use this for initialization
		void Start () {

			headSteps = new List<GameObject> ();
			bodySteps = new List<GameObject> ();

			wrapperRt = bodyStepsWrapper.GetComponent<RectTransform> ();
			originalPos = wrapperRt.anchoredPosition;

			// Adds head and body steps
			if(headStepsWrapper != null) foreach (RectTransform rt  in headStepsWrapper.transform) headSteps.Add (rt.gameObject);
			foreach (RectTransform rt  in bodyStepsWrapper.transform) {
				if(rt.gameObject.name != "Image")
					bodySteps.Add (rt.gameObject);
			}
			if (headStepsWrapper != null && headSteps.Count != bodySteps.Count)
				Debug.LogError ("The number of head steps and body steps is not the same");

			_nbOfSteps = bodySteps.Count;

			Init ();
		}

		void Init() {
			if (bodySteps.Count <= 0)
				return;

			GoToStep (0);

			if(headStepsWrapper != null)
				(headSteps [headSteps.Count-1].GetComponent<ProfileHeadStep> ()).HideProgressLine ();
		}

		public void GoBack () {
			if (_currentStep > 0)
				GoToStep (_currentStep - 1);
			else {
				if (sceneLoaderBack != null)
					sceneLoaderBack.ManuallyLoadScene ();
			}
		}
		
		public void GoToStep(int newStep) {
			if (inTransition)
				return;

			Hashtable ht = new Hashtable ();
			ht.Add ("from", -SCREEN_WIDTH*_currentStep);
			ht.Add ("to", -SCREEN_WIDTH*newStep);
			ht.Add ("time", 1f);
			ht.Add ("onupdate", "OnHorizontalPositionUpdate");
			ht.Add ("onupdatetarget", this.gameObject);
			ht.Add ("oncomplete", "OnTransitionComplete");
			ht.Add ("oncompletetarget", this.gameObject);
			ht.Add ("easetype", iTween.EaseType.easeOutExpo);
			iTween.ValueTo (wrapperRt.gameObject, ht);

			if (headStepsWrapper != null) {
				if (_currentStep < newStep)
					(headSteps [_currentStep].GetComponent<ProfileHeadStep> ()).CompletedOnce = true;
		
				(headSteps [_currentStep].GetComponent<ProfileHeadStep> ()).Current = false;
				(headSteps [newStep].GetComponent<ProfileHeadStep> ()).Current = true;

				if(accrocheText != null) accrocheText.text = (headSteps [newStep].GetComponent<ProfileHeadStep> ()).accroche;
				if(sousAccrocheText != null) sousAccrocheText.text = (headSteps [newStep].GetComponent<ProfileHeadStep> ()).sousAccroche;
			}

			_currentStep = newStep;
			inTransition = true;
		}

		void OnHorizontalPositionUpdate(float value) {
			wrapperRt.anchoredPosition = new Vector2 (value, wrapperRt.anchoredPosition.y);
		}

		public void BringToFront() {
			Hashtable ht = new Hashtable ();
			ht.Add ("from", 0f);
			ht.Add ("to", wrapperRt.sizeDelta.y);
			ht.Add ("time", 1f);
			ht.Add ("onupdate", "OnVerticalPositionUpdate");
			ht.Add ("onupdatetarget", this.gameObject);
			ht.Add ("easetype", iTween.EaseType.easeOutExpo);
			iTween.ValueTo (wrapperRt.gameObject, ht);
		}
			
		public void BringToOrigin() {
			Hashtable ht = new Hashtable ();
			ht.Add ("from", wrapperRt.anchoredPosition.y);
			ht.Add ("to", originalPos.y);
			ht.Add ("time", 1f);
			ht.Add ("onupdate", "OnVerticalPositionUpdate");
			ht.Add ("onupdatetarget", this.gameObject);
			ht.Add ("easetype", iTween.EaseType.easeOutExpo);
			iTween.ValueTo (wrapperRt.gameObject, ht);
		}

		void OnVerticalPositionUpdate(float value) {
			wrapperRt.anchoredPosition = new Vector2 (wrapperRt.anchoredPosition.x, value);
		}

		void OnTransitionComplete() {
			inTransition = false;
		}
	}
}