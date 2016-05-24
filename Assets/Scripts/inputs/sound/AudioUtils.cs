using UnityEngine;
using System;
using System.Collections;
using ProtoTurtle.BitmapDrawing;
using MPP.Events;

namespace MPP.Inputs._Sound
{
	public class AudioUtils
	{
		public static float GetAveragedVolume(AudioSource audio, int samples) {
			float[] data = new float[samples];
			float a = 0;
			audio.GetOutputData(data,0);
			foreach(float s in data)
			{
				a += Mathf.Abs(s);
			}
			return a/samples;
		}

		public static float[] GetSpectrum(AudioSource audio, int samples, float[] spectrumArray) {
			if(spectrumArray == null || spectrumArray.Length <= 0) spectrumArray = new float[samples];
			audio.GetSpectrumData (spectrumArray, 0, FFTWindow.Rectangular);
			return spectrumArray;
		}

		public static float GetAvgFrequencies(AudioSource audio, float[] spectrumArray, float freq_amp) {
			if(spectrumArray == null || spectrumArray.Length <= 0) spectrumArray = GetSpectrum (audio, 256, null);
			if(freq_amp == null || freq_amp == 0) freq_amp = 1;

			float freq_avg = 0;
			for (int i = 0; i < spectrumArray.Length; i++) {
				freq_avg += spectrumArray [i] * freq_amp;
			}
			freq_avg /= spectrumArray.Length;

			return freq_avg;
		}
	}
}

