using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(AudioSource))]
public class Scene_09_Interview : MonoBehaviour {

	public GameObject question1Wrapper;
	public AudioClip question1Clip;

	public GameObject question2Wrapper;
	public AudioClip question2PositiveClip;
	public AudioClip question2NegativeClip;
	public AudioClip question2NeutralClip;

	public AudioClip brotherBraveClip;
	public AudioClip brotherScaredClip;

	public AudioClip piriBraveClip;
	public AudioClip piriScaredClip;

	public AudioClip explicationsForest;
	public AudioClip explicationsBush;
	public AudioClip explicationsAnimal;

	AudioSource audioSource;
	bool scared;
	string highlightMoment;

	// Use this for initialization
	void Start () {
		audioSource = this.GetComponent<AudioSource> ();

		StartCoroutine (LaunchInterview ());
	}

	IEnumerator LaunchInterview() {
		question1Wrapper.SetActive (false);
		question2Wrapper.SetActive (false);
		Speak (question1Clip); 

		yield return new WaitForSeconds (audioSource.clip.length);

		question1Wrapper.SetActive (true);
		question2Wrapper.SetActive (false);
	}

	public void AnswerQuestion1(string answer) {
		if (answer != "negative")
			scared = true;
		else
			scared = false;

		question1Wrapper.SetActive (false);
		question2Wrapper.SetActive (true);

		if (answer == "positive")
			Speak (question2PositiveClip);
		else if (answer == "negative")
			Speak (question2NegativeClip);
		else
			Speak (question2NeutralClip);
	}

	public void AnswerQuestion2(string answer) {
		question1Wrapper.SetActive (false);
		question2Wrapper.SetActive (false);
		highlightMoment = answer;

		if (scared) {
			Speak (brotherScaredClip);
		} else {
			Speak (brotherBraveClip);
		}

		StartCoroutine (EndOfInterview());
	}

	IEnumerator EndOfInterview() {
		yield return new WaitForSeconds (audioSource.clip.length);

		if (scared)
			Speak (piriScaredClip);
		else
			Speak (piriBraveClip);

		yield return new WaitForSeconds (audioSource.clip.length);
		if (highlightMoment == "forest")
			Speak (explicationsForest);
		else if (highlightMoment == "bush")
			Speak (explicationsBush);
		else
			Speak (explicationsAnimal);
	}

	void Speak (AudioClip clip) {
		audioSource.clip = clip;
		audioSource.time = 0f;
		audioSource.Play ();
	}
}
