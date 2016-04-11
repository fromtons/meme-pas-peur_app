using UnityEngine;
using System.Collections;

public class BackgroundManager : MonoBehaviour {

	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ChangeColor(Color newColor) {
		Hashtable tweenParams = new Hashtable();
		tweenParams.Add("from", spriteRenderer.color);
		tweenParams.Add("to", newColor);
		tweenParams.Add("time", 3);
		tweenParams.Add("onupdate", "OnColorUpdated");

		iTween.ValueTo(gameObject, tweenParams);
	}

	private void OnColorUpdated(Color color)
	{
		spriteRenderer.color = color;
	}
}
