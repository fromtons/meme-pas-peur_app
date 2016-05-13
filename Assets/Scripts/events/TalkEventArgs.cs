using UnityEngine;
using System;
using System.Collections;

namespace MPP.Events {
	public class TalkEventArgs : EventArgs {
		public string ID 		{ get; set; }
		public int AudioClipId 	{ get; set; }
		public bool Autoplay 	{ get; set; }
		public float Delay		{ get; set; }
	}
}
