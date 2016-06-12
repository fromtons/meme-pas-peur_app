using UnityEngine;
using System.Collections;
using MPP.Util;
using MPP.Events;

namespace MPP.Forest.Scene_06 {
	public class Piri : MonoBehaviour {

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
		public GameObject origin;

		public Wolf wolf;

		Vector3 initialScale;
		bool travelling = false; 
		bool goingBack = false;

		int insistCpt = 0;
		bool insist = true;

		TalkEventManager.TalkEvent onTalkEnded;
		WolfEventManager.WolfEvent onWolfStateChange;

		// Use this for initialization
		void Start () {
			animator = this.GetComponent<Animator> ();
			initialScale = this.transform.localScale;

			TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID="piri", AudioClipId=0, Autoplay=false });
			onTalkEnded = new TalkEventManager.TalkEvent (OnTalkEnded);
			TalkEventManager.TalkEnded += onTalkEnded;
			onWolfStateChange = new WolfEventManager.WolfEvent (OnWolfStateChange);
			WolfEventManager.WolfChangeState += onWolfStateChange;
		}

		void Update() {
			// If travelling, we get speed from the gyroscope, else if we are not going back, we force speed to 0
			if (travelling) {
				#if !UNITY_EDITOR
				speed = Input.gyro.gravity.x;
				#endif
			} else if(!goingBack) speed = 0;
			animator.SetFloat ("speed", speed*4);

			// Is moving or not management
			if ((speed < -0.1f || speed > 0.1f)) {
				insist = false; // The first time we move, insist will go from true to false, meaning the user understood how to move
				this.transform.Translate (new Vector3 (4f, 0f, 0f) * speed * Time.deltaTime);
				state = STATE_PROFILE_WALK;
			} else {
				state = STATE_IDLE;
			}

			// Orientation management
			// We don't go there until piri's travelling or goingback
			if (travelling || goingBack) {			
				// Orientation management
				if (speed < 0) {
					this.transform.localScale = new Vector3 (-initialScale.x, initialScale.y, initialScale.z);
				} else {
					this.transform.localScale = new Vector3 (initialScale.x, initialScale.y, initialScale.z);
				}
			}
		}

		void OnTalkEnded(TalkEventArgs e) {
			if (e.ID == "piri") {
				if (e.AudioClipId == 0)
					TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID = "piri", AudioClipId = 1, Autoplay = true });
				else if (e.AudioClipId != 4 && e.AudioClipId != 5) {
					StartCoroutine (Insist ());

					if (e.AudioClipId == 1) {
						WolfEventManager.TriggerWolfListening (new WolfEventArgs { Listening = true });
						travelling = true;
					}
				}
			}
		}

		void OnWolfStateChange(WolfEventArgs e) {
			if (travelling && e.State == Wolf.STATE_AWAKEN) {
				// Don't try to go back if we already are at origin position
				if (checkIfAlreadyAtOrigin())
					return;
			
				goBack ();
			} else {
				afterGoingBack ();
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
				travelling = false;
				OngletManager.instance.HighlightNextOnglet ();
				WolfEventManager.TriggerWolfListening (new WolfEventArgs { Listening = false });
			} else if (other.gameObject == origin) {
				if (goingBack) {
					afterGoingBack ();
				}
			} else if(other.gameObject.GetComponent<MPP.Utils.Backdoor>() == null) {
				TalkEventManager.TriggerTalkSet (new TalkEventArgs { ID="piri", AudioClipId=5, Autoplay=true });	
			}
		}

		void OnTriggerStay2D(Collider2D other) {
			if (other.gameObject == origin && goingBack) {
				afterGoingBack ();
			}
		}

		bool checkIfAlreadyAtOrigin() {
			Collider2D[] colliders = Physics2D.OverlapCircleAll (new Vector2(this.transform.position.x, this.transform.position.y), (this.GetComponent<SpriteRenderer>()).bounds.size.x);
			for (int i = 0; i < colliders.Length; i++) {
				if (colliders [i].gameObject == origin) {
					return true;
				}
			}
			return false;
		}

		void goBack() {
			travelling = false;
			goingBack = true;
			speed = -3;
		}

		void afterGoingBack() {
			travelling = true;
			goingBack = false;
			speed = 0;
		}

		void OnGUI () {
			if(false) {
				GUILayout.Label("Going back: " + goingBack);
				GUILayout.Label("Gyro gravity: " + Input.gyro.gravity.ToString());
			}
		}

		void OnDestroy() {
			TalkEventManager.TalkEnded -= onTalkEnded;
		}
	}
}