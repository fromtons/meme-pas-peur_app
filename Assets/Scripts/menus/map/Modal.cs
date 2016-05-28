using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using MPP.Util;
using MPP.Util.UI;

namespace MPP.Menus.Map {
	public class Modal : MonoBehaviour {

		public GameObject wrapper;
		public GameObject outerWrapper;
		public GameObject innerWrapper;
		public Button closeButton;
		public SceneLoader launchButton;

		public Image cover;
		public Text description;

		// TODO
		public Text nbOfReads;
		public Text lastReadDate;

		public GroupFade _animation;

		void Awake() {
			if(_animation == null)
				_animation = this.GetComponent<GroupFade> ();
		}

		public void Fill(MapItem data) {
			cover.sprite = data.picture;
			description.text = data.description;
			launchButton.sceneToLoad = data.sceneToLoad;
			nbOfReads.text = data.nbOfReads;
			lastReadDate.text = data.lastReadDate;
		}

		public void Show() {
			_animation.Show (0.6f);
		}

		public void Hide() {
			_animation.Hide (0.4f);
		}
	}
}