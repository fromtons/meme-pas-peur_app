using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MPP.Cavern {
	[RequireComponent(typeof(AudioSource))]
	public class StormManager : MonoBehaviour {
		
		AudioSource audioSource;
		
		public List<AudioClip> lightnings;

		// Use this for initialization
		void Start () {
			audioSource = this.GetComponent<AudioSource>();
		}
		
		void playSound(int soundToPlay) {
			audioSource.clip = lightnings[soundToPlay];
			audioSource.time = 0f;
			audioSource.Play();
		}
	}
}
