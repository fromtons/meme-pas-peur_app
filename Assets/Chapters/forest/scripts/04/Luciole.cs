using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace MPP.Forest.Scene_04 {
	public class Luciole : MonoBehaviour {

		public string pathToFollow = "";
		public int pathDuration = 5;
		public Sprite lightenedSprite;

		private bool lightened = false;
		private SpriteRenderer spriteRenderer;
		private BoxCollider2D collider;

		AudioSource audioSource;
		public AudioClip audioClip;

		// Use this for initialization
		void Start () {
			spriteRenderer = GetComponent<SpriteRenderer>();
			collider = GetComponent<BoxCollider2D> ();
			audioSource = GetComponent<AudioSource> ();

			this.FollowPath();	
		}

		void FollowPath() {
			iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(pathToFollow), "time", pathDuration, "easetype", iTween.EaseType.linear, "looptype", "loop"));	
		}
		
		// Update is called once per frame
		void Update () {
			if (Input.GetMouseButtonDown(0) && !lightened) {
				Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);
				if (hitCollider == collider) {
					spriteRenderer.sprite = lightenedSprite;
					lightened = true;

					audioSource.PlayOneShot(audioClip);


					Manager.checkLuciole();
				}
			}
		} 
	}
}