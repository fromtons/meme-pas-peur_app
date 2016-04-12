using UnityEngine;
using System.Collections;

public class EnableObject : MonoBehaviour {

	public GameObject target;

	public void toggleTarget() {
		Debug.Log("TOGGLE");

		if(!target.activeSelf)
			target.SetActive (true);
		else 
			target.SetActive (false);
	}
}
