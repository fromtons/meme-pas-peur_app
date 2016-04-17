using UnityEngine;
using System.Collections;

public class Scene_04_Piri : MonoBehaviour {

	protected Animator animator; 

	public float moveToX;
	public float moveToY;
	public int duration = 3;

	public static int STATE_IDLE = 0;
	public static int STATE_PROFILE_WALK = 1;
	public static int STATE_SAD = 2;
	public static int STATE_PROFILE_WALK_SAD = 3;
	//int _state = STATE_IDLE;
	public int state {
		set {
			//_state = value;
			animator.SetInteger("state", value);

			if (STATE_SAD == value || STATE_IDLE == value)
				this.GetComponent<Talker> ().mouthManager.gameObject.SetActive (true);
			else 
				this.GetComponent<Talker> ().mouthManager.gameObject.SetActive (false);
		}
	}

	Vector3 initialScale;

	int insistCpt=0;
	bool insist = true;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
		initialScale = this.gameObject.transform.localScale;

		state = STATE_PROFILE_WALK_SAD;

		this.gameObject.transform.localScale = new Vector3 (-initialScale.x, initialScale.y, initialScale.z);;

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
		this.gameObject.transform.localScale = new Vector3 (initialScale.x, initialScale.y, initialScale.z);;

		TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID = "piri", AudioClipId = 0, Autoplay = false });
		TalkEventManager.TalkEnded += new TalkEventManager.TalkEvent (OnTalkEnded);
	}

	void OnTalkEnded(TalkEventArgs eventArgs) {
		if (eventArgs.ID == "piri") {
			if (eventArgs.AudioClipId != 1) {
				StartCoroutine (Insist ());
			} else {
				OngletManager.instance.HighlightNextOnglet ();
			} 
		}
	}

	IEnumerator Insist() {
		yield return new WaitForSeconds (8f);
		if(insist)
			TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID = "piri", AudioClipId = ((insistCpt % 2)+2), Autoplay = true });
		insistCpt++;
	}

	public void OnLightenedLucioles() {
		insist = false;
		state = STATE_IDLE;
		TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID = "piri", AudioClipId = 1, Autoplay = true });
	}

	// Update is called once per frame
	void Update () {

	}
}
