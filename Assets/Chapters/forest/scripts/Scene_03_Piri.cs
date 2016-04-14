using UnityEngine;
using System.Collections;

public class Scene_03_Piri : MonoBehaviour {

	protected Animator animator; 

	public float moveToX;
	public float moveToY;
	public int duration = 25;

	public static int STATE_IDLE = 0;
	public static int STATE_PROFILE_WALK = 1;
	public static int STATE_SAD = 2;
	public static int STATE_PROFILE_WALK_SAD = 3;
	int _state = STATE_IDLE;
	public int state {
		set {
			_state = value;
			animator.SetInteger("state", value);
		}
	}

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();

		state = STATE_PROFILE_WALK_SAD;

		Hashtable ht = new Hashtable ();
		ht.Add ("position", new Vector3(moveToX,moveToY, 0f));
		ht.Add ("time", duration);
		ht.Add ("easetype", iTween.EaseType.linear);
		ht.Add ("oncomplete", "OnAnimationComplete");
		ht.Add ("oncompletetarget", this.gameObject);
		iTween.MoveTo(gameObject, ht);
	}


	void OnAnimationComplete() {
		state = STATE_SAD;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
