using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TransparentDrawingZone : DrawingZone {    
    // Use this for initialization
	protected void Start () {
		rectTransform = (RectTransform)this.GetComponent<RectTransform> ();
		rawImage = (RawImage) this.GetComponent<RawImage> ();
		Reset ();

		GetZoneInfos ();
	}
    
    protected void Reset() {
		drawableTexture = new Texture2D (Mathf.RoundToInt(rectTransform.rect.width), Mathf.RoundToInt(rectTransform.rect.height), TextureFormat.RGBA32, false);
        
        Color fillColor = new Color(0f,0f,0f,0f); 
        Color[] fillColorArray =  drawableTexture.GetPixels();
        for(int i = 0; i < fillColorArray.Length; ++i)
        {
            fillColorArray[i] = fillColor;
        }
        
        drawableTexture.SetPixels( fillColorArray );
        drawableTexture.Apply();
        
		rawImage.texture = drawableTexture;
	}
}