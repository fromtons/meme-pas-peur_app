using UnityEngine;
using System.Collections;

public class Scene_10_Piri : MonoBehaviour {

	// Use this for initialization
	void Start () {
		TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID = "piri", AudioClipId = 0, Autoplay = false });
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
