using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class CameraManager : MonoBehaviour {

	ColorCorrectionCurves ccc;
	public float moveToX;
	public float moveToY;

	// Use this for initialization
	void Start () {
		ccc = (ColorCorrectionCurves) GetComponent(typeof(ColorCorrectionCurves));

		iTween.MoveTo(gameObject, iTween.Hash("position", new Vector3(moveToX,moveToY, -2f), "time", Chapter1Manager.introAnimationDuration, "easetype", iTween.EaseType.easeInOutSine));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void disableGreyscale() {
		Hashtable tweenParams = new Hashtable();
		tweenParams.Add("from", ccc.saturation);
		tweenParams.Add("to", 1f);
		tweenParams.Add("time", 3);
		tweenParams.Add("onupdate", "OnGreyscaleUpdated");

		iTween.ValueTo(gameObject, tweenParams);
	}

	private void OnGreyscaleUpdated(float saturation) {
		ccc.saturation = saturation;
	}
}
