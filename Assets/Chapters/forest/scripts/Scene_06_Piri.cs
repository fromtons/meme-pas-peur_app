using UnityEngine;
using System.Collections;

public class Scene_06_Piri : MonoBehaviour {

	protected Animator animator; 

	public static int STATE_IDLE = 0;
	public static int STATE_PROFILE_WALK = 1;
	public static int STATE_SAD = 2;
	public static int STATE_PROFILE_WALK_SAD = 3;
	//int _state = STATE_IDLE;
	public int state {
		set {
			//_state = value;
			animator.SetInteger("state", value);
		}
	}

	public float speed = 0f;
	public GameObject target;

	public Scene_06_Wolf wolf;

	Vector3 initialScale;
	float gyroSign;
	bool freeze = true;

	int insistCpt = 0;
	bool insist = true;

	TalkEventManager.TalkEvent onTalkEnded;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();
		initialScale = this.transform.localScale;

		TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID="piri", AudioClipId=0, Autoplay=false });
		onTalkEnded = new TalkEventManager.TalkEvent (OnTalkEnded);
		TalkEventManager.TalkEnded += onTalkEnded;
	}

	void Update() {
		#if !UNITY_EDITOR
			gyroSign = Input.gyro.attitude.z > 0f ? -1f : 1f;
			speed = Input.gyro.attitude.x * gyroSign;
		#endif
		animator.SetFloat ("speed", speed*4);

		// Is moving or not management
		if (!freeze && wolf.CurrentAnimationState != Scene_06_Wolf.STATE_AWAKEN && (speed < -0.1f || speed > 0.1f)) {
			insist = false;
			this.transform.Translate (new Vector3 (4f, 0f, 0f) * speed * Time.deltaTime);
			state = STATE_PROFILE_WALK;

		} else {
			state = STATE_IDLE;
		}

		// Orientation management
		if (speed < 0) {
			this.transform.localScale = new Vector3 (-initialScale.x, initialScale.y, initialScale.z);
		} else {
			this.transform.localScale = new Vector3 (initialScale.x, initialScale.y, initialScale.z);
		}
	}

	void OnTalkEnded(TalkEventArgs e) {
		if (e.ID == "piri") {
			if (e.AudioClipId == 0)
				TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID = "piri", AudioClipId = 1, Autoplay = true });
			else if (e.AudioClipId != 4) {
				StartCoroutine (Insist ());
			} else {
				freeze = false;
			}

			if (e.AudioClipId == 1) {
				freeze = false;
			}
		}
	}

	IEnumerator Insist() {
		yield return new WaitForSeconds (5f);
		if(insist)
			TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID="piri", AudioClipId= (insistCpt%2)+2, Autoplay=true });
		insistCpt++;
	}

	void OnTriggerEnter2D(Collider2D other) {        
		if (other.gameObject == target) {
			freeze = true;
			OngletManager.instance.HighlightNextOnglet ();
		} else {
			TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID="piri", AudioClipId=5, Autoplay=true });	
		}
	}

	void OnGUI () {
		GUILayout.Label("Gyro reading: " + Input.gyro.attitude.ToString());
	}

	void OnDestroy() {
		TalkEventManager.TalkEnded -= onTalkEnded;
	}
}
