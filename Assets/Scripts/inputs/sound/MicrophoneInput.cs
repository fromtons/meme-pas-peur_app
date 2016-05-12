using UnityEngine;
using System.Collections;
using ProtoTurtle.BitmapDrawing;

[RequireComponent(typeof(AudioSource))]
public class MicrophoneInput : MonoBehaviour {
	public float sensitivity = 100f;
	public float loudness = 0;
	
	float deviceFactor = 100f;
	bool deviceFactorActive = true;
	
	bool debug = true;

	AudioSource audio;

	void Start() {

		if(deviceFactorActive) { 
			#if UNITY_EDITOR
				deviceFactor=70f;
			#elif UNITY_IOS 
				deviceFactor=35f;
			#endif
		}
		
		audio = this.GetComponent<AudioSource> ();
		audio.clip = Microphone.Start(Microphone.devices[0], true, 	1, 44100);
		audio.loop = true; // Set the AudioClip to loop
		//audio.mute = true; // Mute the sound, we don't want the player to hear it
		while (!(Microphone.GetPosition(Microphone.devices[0]) > 0)){} // Wait until the recording has started
		audio.Play(); // Play the audio source!
	}

	void Update(){
		loudness = GetAveragedVolume() * (sensitivity * (sensitivity/deviceFactor));
		GraphDebugEventManager.TriggerUpdate (new GraphDebugEventArgs { ID = "mic", Value = loudness });
	}

	float GetAveragedVolume()
	{ 
		float[] data = new float[256];
		float a = 0;
		audio.GetOutputData(data,0);
		foreach(float s in data)
		{
			a += Mathf.Abs(s);
		}
		return a/256;
	}
}