using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioRecorder : MonoBehaviour {

	string micName;
	AudioSource audio;

	// Use this for initialization
	void Start () {
		micName = Microphone.devices [0];
		audio = this.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Record() {
		if (!Microphone.IsRecording(micName))
			audio.clip = Microphone.Start (micName, true, 20, 44100);
		else
			Microphone.End (micName);
	}

	public void Replay() {
		if (!Microphone.IsRecording(micName)) {
			audio.Play ();
		}
	}
}
