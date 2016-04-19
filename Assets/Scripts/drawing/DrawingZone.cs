using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using ProtoTurtle.BitmapDrawing;

public class DrawingZone : MonoBehaviour {

	public Color color;
	public float drawingResolution = 2;
	public int radius = 10;

	protected RawImage rawImage;
	protected RectTransform rectTransform;

	protected Texture2D drawableTexture;

	protected Vector3[] corners;
	protected Rect drawableZone;
	protected Vector2 lastMousePos;
	protected bool isDrawing = false;

	// Use this for initialization
	protected void Start () {
		rectTransform = (RectTransform)this.GetComponent<RectTransform> ();
		rawImage = (RawImage) this.GetComponent<RawImage> ();
		Reset ();

		GetZoneInfos ();
	}
	
	// Update is called once per frame
	void Update () {
		// Mouse pressed
		if (Input.GetMouseButton (0)) {
			Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);

			// On our drawable zone
			if (drawableZone.Contains (mouseWorldPosition)) {
				Vector3 mousePosOnDrawableZone = new Vector3 (mouseWorldPosition.x - drawableZone.position.x, Mathf.Abs (mouseWorldPosition.y - (drawableZone.position.y + drawableZone.height)), 0f);
				Vector2 mousePosOnTexture = new Vector2 (mousePosOnDrawableZone.x * rectTransform.rect.width / drawableZone.width, mousePosOnDrawableZone.y * rectTransform.rect.height / drawableZone.height);

				if (isDrawing) {
					float cpt = 2;
					float dt = DistanceBetweenPoints (lastMousePos, mousePosOnTexture);
					while(cpt < 400 && cpt < dt) {
						Vector2 itvlPt = CalculateInBetweenPoint (lastMousePos, mousePosOnTexture, cpt);
						if(itvlPt!=Vector2.zero) drawableTexture.DrawFilledCircle (Mathf.RoundToInt (itvlPt.x), Mathf.RoundToInt (itvlPt.y), radius, color);
						cpt += 2;
					}
					Debug.Log (cpt);
				}
				drawableTexture.DrawFilledCircle (Mathf.RoundToInt (mousePosOnTexture.x), Mathf.RoundToInt (mousePosOnTexture.y), radius, color);

				drawableTexture.Apply ();
				rawImage.texture = drawableTexture;

				lastMousePos = mousePosOnTexture;
				isDrawing = true;
			}
		} else if (isDrawing)
			isDrawing = false;
	}

	public void Reset() {
		drawableTexture = new Texture2D (Mathf.RoundToInt(rectTransform.rect.width), Mathf.RoundToInt(rectTransform.rect.height), TextureFormat.RGB24, false);
		drawableTexture.Apply ();
		rawImage.texture = drawableTexture;
	}

	protected void GetZoneInfos() {
		// GameObject coordinates and boundaries for mouse use
		corners = new Vector3[4];
		rawImage.rectTransform.GetWorldCorners(corners);
		drawableZone = new Rect(corners[0], corners[2]-corners[0]);
	}

	Vector2 CalculateInBetweenPoint(Vector2 pointA, Vector2 pointB, float distanceFromA) {
		float distanceBetweenTwoPoints = Mathf.Sqrt (Mathf.Pow(pointB.x - pointA.x,2) + Mathf.Pow(pointB.y - pointA.y, 2));

		if (distanceFromA > distanceBetweenTwoPoints)
			return Vector2.zero;

		float x, y;
		float T = distanceFromA / distanceBetweenTwoPoints;

		x = (1 - T) * pointA.x + T * pointB.x;
		y = (1 - T) * pointA.y + T * pointB.y;

		return new Vector2 (x, y);
	}

	float DistanceBetweenPoints(Vector2 pointA, Vector2 pointB) {
		return (Mathf.Sqrt (Mathf.Pow(pointB.x - pointA.x,2) + Mathf.Pow(pointB.y - pointA.y, 2)));
	}

	/*List<Vector2> InterpolatePoints(Vector2 pointA, Vector2 pointB, float itvl) {
		List<Vector2> points = new List<Vector2> ();
		float distanceBetweenTwoPoints = Mathf.Sqrt (Mathf.Pow(pointB.x - pointA.x,2) + Mathf.Pow(pointB.y - pointA.y, 2));
		float currentPos=0;

		while (currentPos <= distanceBetweenTwoPoints) {
			points.Add (CalculateInBetweenPoint(pointA,pointB,currentPos));
			currentPos += itvl;
		}

		return points;
	}*/
}
