﻿using UnityEngine;
using System.Collections;

public class MouthManager : MonoBehaviour {

	public float speedTalk = .2f;

	bool keepTalking = false;

	Vector3 initialScale;

	// Use this for initialization
	void Start () {
		initialScale = this.transform.localScale;

		toggle ();
	}

	void open() {
		Hashtable ht = new Hashtable ();
		ht.Add("scale", new Vector3(initialScale.x, initialScale.y, initialScale.x));
		ht.Add("time", speedTalk);
		ht.Add("oncomplete", "close");
		ht.Add("oncompletetarget", this.gameObject);
		iTween.ScaleTo(this.gameObject, ht);
	}

	void close() {
		Hashtable ht = new Hashtable ();
		ht.Add("scale", new Vector3(initialScale.x, 0.01f, initialScale.x));
		ht.Add("time", speedTalk);
		if(keepTalking)
			ht.Add("oncomplete", "open");
		ht.Add("oncompletetarget", this.gameObject);
		iTween.ScaleTo(this.gameObject, ht);
	}

	public void toggle() {
		if (keepTalking)
			this.stop ();
		else
			this.play ();
	}

	public void play() {
		keepTalking = true;
		open ();
	}

	public void stop() {
		keepTalking = false;
		close ();
	}
}