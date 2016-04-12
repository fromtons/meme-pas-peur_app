using UnityEngine;
using System.Collections;

public class EnableObject : MonoBehaviour {

	public GameObject target;

	public void toggleTarget() {
		if(!target.activeSelf)
			target.SetActive (true);
		else 
			target.SetActive (false);
	}
}
