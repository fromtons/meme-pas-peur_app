using UnityEngine;
using System.Collections;

public class Scene_06_Piri : MonoBehaviour {

	protected Animator animator; 

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

	public float speed = 0f;
	public GameObject target;

	Vector3 initialScale;
	float gyroSign;
	bool freeze = false;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();
		initialScale = this.transform.localScale;
	}

	void Update() {
		#if !UNITY_EDITOR
			gyroSign = Input.gyro.attitude.z > 0f ? -1f : 1f;
			speed = Input.gyro.attitude.x * gyroSign;
		#endif
		animator.SetFloat ("speed", speed*4);

		// Is moving or not management
		if (!freeze && (speed < -0.1f || speed > 0.1f)) {
			this.transform.Translate (new Vector3(4f,0f,0f) * speed * Time.deltaTime);
			state = STATE_PROFILE_WALK;
		} else
			state = STATE_IDLE;

		// Orientation management
		if (speed < 0) {
			this.transform.localScale = new Vector3 (-initialScale.x, initialScale.y, initialScale.z);
		} else {
			this.transform.localScale = new Vector3 (initialScale.x, initialScale.y, initialScale.z);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {        
		if (other.gameObject == target)
			freeze = true;      
	}

	void OnGUI () {
		GUILayout.Label("Gyro reading: " + Input.gyro.attitude.ToString());
	}
}
