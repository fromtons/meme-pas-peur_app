using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MPP.Menus.Advices {
	public class TabsManager : MonoBehaviour {

		public List<TabButton> tabs;

		// Use this for initialization
		void Start () {
			tabs [0].Active = true;
		}
		
		public void SetActive(int id) {
			foreach (TabButton tab in tabs) {
				tab.Active = false;
			}
			tabs [id].Active = true;
		}
	}
}