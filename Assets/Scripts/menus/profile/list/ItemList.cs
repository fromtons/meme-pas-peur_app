using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using MPP.Util;

namespace MPP.Menus.Profile {

	[RequireComponent (typeof(Button))]
	[RequireComponent (typeof(SceneLoader))]
	public class ItemList : MonoBehaviour {
		public Text name;
		public RawImage picture;

		MPP.Data.Profile _profile;
		public MPP.Data.Profile Profile {
			set {
				_profile = value;
				name.text = _profile.name;
				picture.texture = _profile.picture;
			}
		}

		Button _button;
		SceneLoader _sceneLoader;

		void Start() {
			_sceneLoader = this.GetComponent<SceneLoader> ();
			_button = this.GetComponent<Button> ();
			_button.onClick.AddListener (OnClick);
		}

		void OnClick() {
			MPP.Data.ProfileManager.instance.CurrentProfile = _profile;
			_sceneLoader.ManuallyLoadScene ();
		}
	}
}