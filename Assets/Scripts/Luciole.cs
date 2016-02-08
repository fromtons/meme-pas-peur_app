using UnityEngine;
using System.Collections;

public class Luciole : MonoBehaviour {

	public string pathToFollow = "";
	public int pathDuration = 5;

	// Use this for initialization
	void Start () {
		this.FollowPath();	
	}

	void FollowPath() {
		iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(pathToFollow), "time", pathDuration, "easetype", iTween.EaseType.linear, "looptype", "loop"));	
	}
	
	// Update is called once per frame
	void Update () {

	}
}
