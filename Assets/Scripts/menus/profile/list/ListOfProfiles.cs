using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MPP.Data;

namespace MPP.Menus.Profile {
	public class ListOfProfiles : MonoBehaviour {

		public static ListOfProfiles instance;

		public ItemList listItemPrefab;

		List<MPP.Data.Profile> availableProfiles;
		List<ItemList> listItems;

		// Use this for initialization
		void Start () {

			if (instance == null)
				instance = this;
			else {
				Destroy (this);
				return;
			}

			ProfileManager.instance.GetProfiles ();
			availableProfiles = ProfileManager.instance.AvailableProfiles;

			PopulateList ();
		}

		void PopulateList() {
			listItems = new List<ItemList> ();
			foreach (MPP.Data.Profile p in availableProfiles) {
				ItemList newItem = (ItemList) Instantiate(listItemPrefab);
				newItem.Profile = p;
				newItem.transform.SetParent (this.transform);
				newItem.transform.localScale = new Vector3 (1f, 1f, 1f);
				listItems.Add(newItem);
			}
		}
	}
}