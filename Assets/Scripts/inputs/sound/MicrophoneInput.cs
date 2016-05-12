using UnityEngine;
using System.Collections;
using ProtoTurtle.BitmapDrawing;

[RequireComponent(typeof(AudioSource))]
public class MicrophoneInput : MonoBehaviour {
	public float sensitivity = 100f;
	public static float FREQ_AMP = 1000f;
	public float loudness = 0;

	static float DEVICE_FACTOR_EDITOR = 70f;
	static float DEVICE_FACTOR_IOS = 35f;
	static float DEVICE_FACTOR_DEFAULT = 100f;
	static float DEVICE_FACTOR;
	static bool DEVICE_FACTOR_ACTIVE = true;

	AudioSource audio;

	float[] spectrum;
	public float freq_avg;

	void Start() {

		spectrum = new float[256];

		if(DEVICE_FACTOR_ACTIVE) { 
			#if UNITY_EDITOR
				DEVICE_FACTOR=DEVICE_FACTOR_EDITOR;
			#elif UNITY_IOS 
				DEVICE_FACTOR=DEVICE_FACTOR_IOS;
			#else 
				DEVICE_FACTOR=DEVICE_FACTOR_DEFAULT;
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
		loudness = GetAveragedVolume() * (sensitivity * (sensitivity/DEVICE_FACTOR));

		// Loudness debug
		GraphDebugEventManager.TriggerUpdate (new GraphDebugEventArgs { ID = "mic", Value = loudness });
	}

	public float[] GetSpectrum() {
		// Gets the sound spectrum.
		audio.GetSpectrumData (spectrum, 0, FFTWindow.Rectangular);

		// Spectrum debug
		GraphDebugEventManager.TriggerUpdateSpectrum (new GraphDebugEventArgs { ID = "spectrum", Values = spectrum });

		return spectrum;
	}

	public float GetAvgFrequencies(bool refreshSpectrum) {
		if(refreshSpectrum) GetSpectrum ();

		freq_avg = 0;
		for (int i = 0; i < spectrum.Length; i++) {
			freq_avg += spectrum [i] * FREQ_AMP;
		}
		freq_avg /= spectrum.Length;

		// Spectrum avg debug
		GraphDebugEventManager.TriggerUpdate (new GraphDebugEventArgs { ID = "avg", Value = freq_avg });
		return freq_avg;
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