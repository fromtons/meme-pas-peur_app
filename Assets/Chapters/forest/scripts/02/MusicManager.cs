using UnityEngine;
using System.Collections;
using MPP.Events;

namespace MPP.Forest.Scene_02 {
	[RequireComponent (typeof(AudioSource))]
	public class MusicManager : MonoBehaviour {

		AudioSource _audioSource;
		public AudioClip newClip;

		SceneEventManager.SceneEvent onSceneLoad;

		// Use this for initialization
		void Start () {
			_audioSource = this.GetComponent<AudioSource> ();
			if(MixerManager.instance != null) MixerManager.instance.FadeTo("MusicVol", 0f, 0f);

			onSceneLoad = new SceneEventManager.SceneEvent (OnSceneLoad);
			SceneEventManager.SceneLoad += onSceneLoad;
		}

		public void ChangeClip() {
			StartCoroutine (OnChangeClip ());
		}

		IEnumerator OnChangeClip() {
			if(MixerManager.instance != null) MixerManager.instance.FadeTo("MusicVol", -80f, 0.5f);
			yield return new WaitForSeconds (0.5f);
			if(MixerManager.instance != null) MixerManager.instance.FadeTo("MusicVol", 0f, 0f);
			_audioSource.clip = newClip;
			_audioSource.time = 0;
			_audioSource.Play();
		}

		void OnSceneLoad(SceneEventArgs eventArgs) {
			if(MixerManager.instance != null) MixerManager.instance.FadeTo("MusicVol", -80f, 1f);
		}

		void OnDestroy() {
			SceneEventManager.SceneLoad -= onSceneLoad;
		}
	}
}