using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MPP.Menus.Advices {
	public class TabButton : MonoBehaviour {

		public GameObject content;
		public Color backgroundActive;
		Color _backgroundInactive;
		public Color textActive;
		Color _textInactive;

		public Text text;
		Image _image;

		bool _active = false;
		public bool Active {
			get { return _active; } 
			set { 
				_active = value; 
				content.SetActive (_active);

				if (_active) {
					_image.color = backgroundActive;
					text.color = textActive;
				} else {
					_image.color = _backgroundInactive;
					text.color = _textInactive;
				}
			}
		}

		// Use this for initialization
		void Awake () {
			_image = this.GetComponent<Image> ();
			_backgroundInactive = _image.color;
			_textInactive = text.color;
		}
	}
}