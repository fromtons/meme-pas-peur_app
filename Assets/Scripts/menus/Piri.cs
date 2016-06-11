using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MPP.Menus {
	public class Piri : MonoBehaviour {

		public static int STATE_IDLE = 0;
		public static int STATE_BOUH = 1;
		public static int STATE_PROUT = 2;
		public static int STATE_RIGOLE = 3;
		public static int STATE_CIAO = 4;

		AudioSource _audioSource;
		Animator _animator;
	
		int _state;
		public int State {
			set {
				_state = value;
				_animator.SetInteger ("state", _state);

				if (_audioSource != null && _state == STATE_PROUT) {
					_audioSource.Play();
				}

				if(value != 0)
					StartCoroutine (CoolDown());
			}
		}

		public List<int> activeStates;

		public int defaultState = 0;

		// Use this for initialization
		void Start () {
			activeStates = new List<int> ();
			activeStates.Add (STATE_PROUT);
			activeStates.Add (STATE_RIGOLE);
			//activeStates.Add (STATE_CIAO);

			_animator = this.GetComponent<Animator> ();
			_audioSource = this.GetComponent<AudioSource> ();

			State = defaultState;
		}

		public void OnClick() {
			State = activeStates[Random.Range (0, activeStates.Count)];
		}

		IEnumerator CoolDown() {
			yield return new WaitForSeconds (.5f);
			State = 0;
		}
	}
}