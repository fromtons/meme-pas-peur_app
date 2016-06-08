using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using MPP.Events;

public class MusicLoop : MonoBehaviour {

	AudioSource _audioSource;
	AudioSource _audioSourceLoop;

	public AudioMixerGroup audioMixerGroup;
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

		_audioSource = this.gameObject.AddComponent<AudioSource> ();
		_audioSource.outputAudioMixerGroup = audioMixerGroup;
		_audioSource.loop = false;
		_audioSource.clip = this.openClip;

		_audioSourceLoop = this.gameObject.AddComponent<AudioSource> ();
		_audioSourceLoop.outputAudioMixerGroup = audioMixerGroup;
		_audioSourceLoop.loop = true;
		_audioSourceLoop.clip = this.loopClip;

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

	public void Reset() {
		_audioSource.clip = this.openClip;
		_audioSourceLoop.clip = this.loopClip;

		if(MixerManager.instance != null)
			MixerManager.instance.FadeTo ("MusicVol", 0f, .2f);
		if (autoPlay) {
			PlayOpen ();
		}
	}

	public void Stop() {
		_audioSource.clip = null;
		_audioSourceLoop.clip = null;
		_audioSource.Stop();
		_audioSourceLoop.Stop();
	}

	void PlayOpen() {
		_audioSource.time = 0;
		_audioSource.Play ();

		_audioSourceLoop.Stop ();

		StartCoroutine (waitForClipEnd(_audioSource.clip.length, this.PlayLoop));
	}

	void PlayLoop() {
		_audioSourceLoop.time = 0;
		_audioSourceLoop.Play ();

		_audioSource.Stop ();
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
