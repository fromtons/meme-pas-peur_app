using UnityEngine;
using System.Collections;
using MPP.Inputs._Sound;
using MPP.Events;

namespace MPP.Util {
	public class MouthManager : MonoBehaviour {

		public float speedTalk = .2f;
		public float closeScaleY = 0.01f;

		bool keepTalking = false;

		AudioSource _audioSource;
		public AudioSource AudioSource {
			set {
				_audioSource = value;
				close ();

				_syncOnAudioSource = true;
			}
		}

		public bool ultraSensitive = false;

		bool _syncOnAudioSource = false;
		float _avgVolume = 0f;
		float _newScale = 0f;

		Vector3 initialScale;

		// Use this for initialization
		void Start () {
			initialScale = this.transform.localScale;

			close ();
		}

		void Update() {
			if (_syncOnAudioSource) {
				_avgVolume = AudioUtils.GetAveragedVolume (_audioSource, 256) * 100;

				_newScale = (_avgVolume-(ultraSensitive ? 5 : 10)) / 5;
				if (_newScale > initialScale.y)
					_newScale = initialScale.y;
				else if (_newScale <= closeScaleY)
					_newScale = closeScaleY;

				if (this.transform.localScale.y <= initialScale.y)
					this.transform.localScale = new Vector3 (this.transform.localScale.x, this.transform.localScale.y + ((_newScale - this.transform.localScale.y)/2f), this.transform.localScale.z);

				GraphDebugEventManager.TriggerUpdate (new GraphDebugEventArgs { ID = "avg", Value = _avgVolume });
			}
		}

		void close() {
			iTween.Stop (this.gameObject);
			Hashtable ht = new Hashtable ();
			ht.Add("scale", new Vector3(initialScale.x, closeScaleY, initialScale.x));
			ht.Add("time", speedTalk);
			iTween.ScaleTo(this.gameObject, ht);
		}
	}
}