using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MPP.Menus.Map {

	[RequireComponent (typeof(Button))]
	public class MapItem : MonoBehaviour {

		public Modal modal;
		public string sceneToLoad;

		[TextArea(3,10)]
		public string description;
		public Sprite picture;

		// TODO 
		public string nbOfReads;
		public string lastReadDate;

		Button _button;

		// Use this for initialization
		void Start () {
			_button = this.GetComponent<Button> ();
			_button.onClick.AddListener (OnClick);
		}

		void OnClick() {
			// TODO - Open modal with good data
			modal.Fill(this);
			modal.Show ();
		}
	}
}