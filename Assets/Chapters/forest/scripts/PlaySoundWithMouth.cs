using UnityEngine;
using System.Collections;

[RequireComponent(typeof (AudioSource))]
public class PlaySoundWithMouth : MonoBehaviour {

	public MouthManager mouthManager;
	public AudioClip audioClip;
	public float delay = 0f;

	AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = this.GetComponent<AudioSource> ();

		StartCoroutine(SayHello());
	}

	IEnumerator SayHello() {
		yield return new WaitForSeconds(delay);
		mouthManager.toggle ();

		audioSource.clip = audioClip;
		audioSource.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!audioSource.isPlaying)
			mouthManager.stop ();
	}
}
