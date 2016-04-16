using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.ImageEffects;

[RequireComponent (typeof(BloomOptimized))]
public class Scene_08_Camera : MonoBehaviour {

	public RawImage mask;
	public float delayBeforeMask = 10f;
	BloomOptimized bloom;

	public GameObject beforeWrapper;
	public GameObject afterWrapper;

	// Use this for initialization
	void Start () {
		bloom = this.GetComponent<BloomOptimized> ();

		StartCoroutine (LaunchAnimation ());

		Hashtable ht = new Hashtable ();
		ht.Add("from", 2.5);
		ht.Add("to", bloom.intensity);
		ht.Add("time", 3);
		ht.Add("onupdate", "OnBloomUpdate");
		iTween.ValueTo (gameObject, ht);
	}

	IEnumerator LaunchAnimation() {
		yield return new WaitForSeconds (delayBeforeMask);
	
		Hashtable tweenParams = new Hashtable();
		tweenParams.Add("from", mask.color);
		tweenParams.Add("to", Color.black);
		tweenParams.Add("time", 1.5);
		tweenParams.Add("onupdate", "OnColorUpdated");
		tweenParams.Add ("oncomplete", "OnFadeComplete");
		tweenParams.Add ("oncompletetarget", this.gameObject);
		iTween.ValueTo(gameObject, tweenParams);
	}

	void OnFadeComplete() {
		beforeWrapper.SetActive(false);
		afterWrapper.SetActive(true);

		bloom.intensity = 0;

		Hashtable tweenParams = new Hashtable();
		tweenParams.Add("from", mask.color);
		tweenParams.Add("to", new Color(0f,0f,0f,0f));
		tweenParams.Add("time", 1.5);
		tweenParams.Add("delay", 0.5);
		tweenParams.Add("onupdate", "OnColorUpdated");
		tweenParams.Add ("oncomplete", "OnFadeComplete");
		iTween.ValueTo(gameObject, tweenParams);
	}

	void OnColorUpdated(Color color)
	{
		mask.color = color;
	}

	void OnBloomUpdate(float value) {
		bloom.intensity = value;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
