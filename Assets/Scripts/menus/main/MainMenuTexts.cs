using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using MPP.Data;

namespace MPP.Menus.MainMenu {
	public class MainMenuTexts : MonoBehaviour {

		public Text title;

		// Use this for initialization
		void Start () {
			title.text = "Salut " + ProfileManager.instance.CurrentProfile.name + " !";
		}
	}
}
