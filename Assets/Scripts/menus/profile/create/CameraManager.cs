using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using MPP.Util;

namespace MPP.Menus.Profile {
	public class CameraManager : MonoBehaviour {

		public WebcamRawImage wRI;
		public RawImage snapshot;
		public Button pictureButton;
		public Button cancelButton;

		public Button confirmButton;

		Texture2D picture;

		// Use this for initialization
		void Start () {
			Cancel ();
		}	

		public void Capture() {
			picture = Texture2DUtils.CropSquare (wRI.SnapShot ());
			snapshot.texture = picture;

			pictureButton.gameObject.SetActive (false);
			cancelButton.gameObject.SetActive (true);
			snapshot.gameObject.SetActive (true);
			confirmButton.gameObject.SetActive(true);
		}

		public void Cancel() {
			pictureButton.gameObject.SetActive (true);
			cancelButton.gameObject.SetActive (false);
			snapshot.gameObject.SetActive (false);
			picture = null;
			confirmButton.gameObject.SetActive (false);
		}
	}
}