using UnityEngine;
using System.Collections;

public class Shaky : MonoBehaviour {

	public float intensity = 1f;
	public float timeCycle = .1f;

	public float delayRange = .5f;

	int sign;
	Vector3 initialRotation;

	Hashtable ht;

	bool firstShake = true;

	// Use this for initialization
	void Start () {
		sign = Random.value < .5 ? 1 : -1;
		initialRotation = this.transform.localEulerAngles;

		ht = new Hashtable ();

		Forward ();
	}

	void Forward() {
		ht.Clear ();
		ht.Add ("rotation", new Vector3 (initialRotation.x, initialRotation.y, initialRotation.z + intensity * sign));
		ht.Add ("time", timeCycle);
		if (firstShake) {
			ht.Add ("delay", delayRange * Random.value);
			firstShake = false;
		}
		ht.Add ("islocal", true);
		ht.Add ("oncomplete", "Backward");
		ht.Add ("oncompletetarget", this.gameObject);
		ht.Add ("easetype", "linear");
		iTween.RotateTo (this.gameObject, ht);
	}

	void Backward() {
		ht.Clear ();
		ht.Add ("rotation", new Vector3 (initialRotation.x, initialRotation.y, initialRotation.z - intensity * sign));
		ht.Add ("time", timeCycle);
		ht.Add ("islocal", true);
		ht.Add ("oncomplete", "Forward");
		ht.Add ("oncompletetarget", this.gameObject);
		ht.Add ("easetype", "linear");
		iTween.RotateTo (this.gameObject, ht);
	}
}
