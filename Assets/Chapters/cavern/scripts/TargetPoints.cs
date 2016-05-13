using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MPP.Cavern {
	public class TargetPoints : MonoBehaviour {
		
		BoxCollider2D[] boxColliders;
		List<bool> boxCollidersTouched;

		bool allCollidersTouched = false;

		// Use this for initialization
		void Start () {
			boxColliders = this.GetComponents<BoxCollider2D>();
			
			boxCollidersTouched = new List<bool>();
			foreach(BoxCollider2D boxCollider in boxColliders) {
				boxCollidersTouched.Add(false);
			}
		}
		
		// Update is called once per frame
		void Update () {
			if(!allCollidersTouched) { 
				int cpt = 0;
				foreach(BoxCollider2D boxCollider in boxColliders) {
					if(!boxColliders[cpt]) {
						if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
							// TODO
						}
					}
					cpt++;
				}
			}
		}
	}
}
