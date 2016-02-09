using UnityEngine;
using System.Collections;

public class Chapter1Manager : MonoBehaviour {

	public static int nbOfLuciolesToCheck = 3;
	public static int nbOfLuciolesChecked = 0;

	public static int introAnimationDuration = 26;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void checkLuciole() {
		GameObject mainCamera = GameObject.Find("MainCamera");
		GameObject background = GameObject.Find("background");
		GameObject hero = GameObject.Find ("piri");
		nbOfLuciolesChecked++;

		if (nbOfLuciolesChecked >= nbOfLuciolesToCheck) {
			//background.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
			BackgroundManager bgMngr = (BackgroundManager) background.GetComponent(typeof(BackgroundManager));
			bgMngr.ChangeColor (new Color(1f,1f,1f,1f));
			CameraManager cameraManager = (CameraManager) mainCamera.GetComponent(typeof(CameraManager));
			cameraManager.disableGreyscale();
			Piri piri = (Piri) hero.GetComponent (typeof(Piri));
			piri.state = Piri.STATE_NORMAL;
		}
	}
}
