using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using MPP.Events;

[RequireComponent (typeof(AudioSource))]
public class MusicLoop : MonoBehaviour {

	AudioSource _audioSource;

	public static List<MusicLoop> instances;

	public string ID;

	public AudioClip openClip;
	public AudioClip loopClip;

	public bool autoPlay = true;

	public List<string> scenesAllowed;

	SceneEventManager.SceneEvent onSceneLoad;

	void Start () {
		if (instances == null)
			instances = new List<MusicLoop> ();

		foreach(MusicLoop musicLoop in instances) {
			if (musicLoop.ID == ID) {
				Destroy (this.gameObject);
				return;
			}
		}
		instances.Add (this);

		DontDestroyOnLoad (this.gameObject);

		_audioSource = this.GetComponent<AudioSource> ();

		onSceneLoad = new SceneEventManager.SceneEvent (OnSceneLoad);
		SceneEventManager.SceneLoad += onSceneLoad;

		Reset ();
	}

	void OnSceneLoad(SceneEventArgs eventArgs) {
		bool toDestroy = true;
		foreach (string scene in scenesAllowed) {
			if (scene == eventArgs.Name) {
				toDestroy = false;
			}
		}

		if (toDestroy)
			StartCoroutine (FadeOut (1f));
	}

	void OnDestroy() {
		instances.Remove (this);
		SceneEventManager.SceneLoad -= onSceneLoad;
	}

	void Reset() {
		if(MixerManager.instance != null)
			MixerManager.instance.FadeTo ("MusicVol", 0f, 0f);
		if (autoPlay) {
			PlayOpen ();
		}
	}

	void ChangeClip(AudioClip audioClip) {
		_audioSource.clip = audioClip;
	}

	void PlayOpen() {
		ChangeClip (this.openClip);
		_audioSource.loop = false;
		_audioSource.Play ();

		StartCoroutine (waitForClipEnd(_audioSource.clip.length, this.PlayLoop));
	}

	void PlayLoop() {
		ChangeClip (this.loopClip);
		_audioSource.loop = true;
		_audioSource.Play ();
	}

	IEnumerator waitForClipEnd(float duration, Action callback) {
		yield return new WaitForSeconds (duration);

		if (callback != null)
			callback ();
	}

	IEnumerator FadeOut(float duration) {
		MixerManager.instance.FadeTo ("MusicVol", -80f, duration);
		yield return new WaitForSeconds (duration);
		Destroy (this.gameObject);
	}
}
