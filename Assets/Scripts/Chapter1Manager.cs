using UnityEngine;
using System.Collections;

public class Chapter1Manager : MonoBehaviour {

	public static int nbOfLuciolesToCheck = 3;
	public static int nbOfLuciolesChecked = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void checkLuciole() {
		GameObject mainCamera = GameObject.Find("MainCamera");
		GameObject background = GameObject.Find("background");
		nbOfLuciolesChecked++;

		if (nbOfLuciolesChecked >= nbOfLuciolesToCheck) {
			//background.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
			BackgroundManager bgMngr = (BackgroundManager) background.GetComponent(typeof(BackgroundManager));
			bgMngr.ChangeColor (new Color(1f,1f,1f,1f));
			CameraManager cameraManager = (CameraManager) mainCamera.GetComponent(typeof(CameraManager));
			cameraManager.disableGreyscale();
		}
	}
}
