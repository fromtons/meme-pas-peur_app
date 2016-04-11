using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayMovieTextureOnUI : MonoBehaviour 
{
	public RawImage rawImage;
	public GameObject snapshotPreview; 

	private RectTransform rawImageRT;
	private AspectRatioFitter rawImageARF;
	private WebCamTexture wct;

	private Sprite webcamSprite;

	void Start () 
	{
		wct = new WebCamTexture();

		rawImage.texture = wct;
		wct.Play();


		rawImageRT = (RectTransform) this.GetComponent<RectTransform>();
		rawImageARF = (AspectRatioFitter) this.GetComponent<AspectRatioFitter>();
	}

	private void Update()
	{
		if ( wct.width < 100 && rawImageARF && rawImageRT)
		{
			Debug.Log("Still waiting another frame for correct info...");
			return;
		}

		// change as user rotates iPhone or Android:

		int cwNeeded = wct.videoRotationAngle;
		// Unity helpfully returns the _clockwise_ twist needed
		// guess nobody at Unity noticed their product works in counterclockwise:
		int ccwNeeded = -cwNeeded;

		// IF the image needs to be mirrored, it seems that it
		// ALSO needs to be spun. Strange: but true.
		if ( wct.videoVerticallyMirrored ) ccwNeeded += 180;

		// you'll be using a UI RawImage, so simply spin the RectTransform
		rawImageRT.localEulerAngles = new Vector3(0f,0f,ccwNeeded);

		float videoRatio = (float)wct.width/(float)wct.height;

		// you'll be using an AspectRatioFitter on the Image, so simply set it
		rawImageARF.aspectRatio = videoRatio;

		// alert, the ONLY way to mirror a RAW image, is, the uvRect.
		// changing the scale is completely broken.
		if ( wct.videoVerticallyMirrored )
			rawImage.uvRect = new Rect(1,0,-1,1);  // means flip on vertical axis
		else
			rawImage.uvRect = new Rect(0,0,1,1);  // means no flip
	}

	public void SnapShot() {
		Texture2D snap = new Texture2D(wct.width, wct.height);
		snap.SetPixels(wct.GetPixels());

		if (wct.videoRotationAngle == 180) {
			snap = FlipTexture (snap, "h");
			snap = FlipTexture (snap, "v");
		}
		snap.Apply();

		webcamSprite = Sprite.Create (snap, new Rect (0, 0, snap.width, snap.height), new Vector2 (0.5f, 0.5f));

		((SpriteRenderer)snapshotPreview.GetComponent<SpriteRenderer> ()).sprite = webcamSprite;
	}

	Texture2D FlipTexture(Texture2D original, string mode){
		if (mode.Length <= 0)
			mode = "h";

		Texture2D flipped = new Texture2D(original.width,original.height);

		int xN = original.width;
		int yN = original.height;

		if (mode == "h") {
			for (int i = 0; i < xN; i++) {
				for (int j = 0; j < yN; j++) {
					flipped.SetPixel (xN - i - 1, j, original.GetPixel (i, j));
				}
			}
		} else {
			for (int i = 0; i < yN; i++) {
				for (int j = 0; j < xN; j++) {
					flipped.SetPixel (j, yN - i - 1, original.GetPixel (j, i));
				}
			}
		}
		flipped.Apply();

		return flipped;
	}
}
 