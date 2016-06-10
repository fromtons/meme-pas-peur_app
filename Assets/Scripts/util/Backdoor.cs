using UnityEngine;
using System.Collections;
using MPP.Events;

namespace MPP.Utils {

	[RequireComponent (typeof(BoxCollider2D))]
	public class Backdoor : MonoBehaviour {

		public string ID;
		BoxCollider2D _collider;

		// Use this for initialization
		void Start () {
			_collider = this.GetComponent<BoxCollider2D> ();
		}
		
		// Update is called once per frame
		void Update () {

			if (Input.GetMouseButton (0)) {
				Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);
				if (hitCollider == _collider) {
					Debug.Log ("backdoor toggled");
					BackdoorEventManager.TriggerBackdoorToggle (new BackdoorEventArgs { ID = this.ID });
				}
			}
		
		}
	}
}