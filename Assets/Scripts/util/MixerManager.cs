using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class MixerManager : MonoBehaviour {

	public static MixerManager instance;
	public AudioMixer audioMixer;

	// Use this for initialization
	void Awake () {
		if (instance == null)
			instance = this;
		else {
			Destroy (this.gameObject);
		}

		DontDestroyOnLoad (this.gameObject);
	}
	
	public void FadeTo(string channel, float value, float duration) {
		iTween.Stop (gameObject);

		float fromValue = 0f;
		audioMixer.GetFloat (channel, out fromValue);

		Hashtable ht = new Hashtable ();
		ht.Add ("from", fromValue);
		ht.Add ("to", value);
		ht.Add ("time", duration);
		ht.Add ("onupdate", "Set"+channel);
		ht.Add ("easetype", iTween.EaseType.easeInExpo);
		iTween.ValueTo(gameObject, ht);
	}

	public void SetMasterVol(float value) {
		audioMixer.SetFloat("MasterVol", value);
	}

	public void SetMusicVol(float value) {
		audioMixer.SetFloat("MusicVol", value);
	}
}
