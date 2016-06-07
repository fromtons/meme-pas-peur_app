using UnityEngine;
using System;
using System.Collections;

namespace MPP.Events {
	public class SceneEventArgs : EventArgs { 
		public int ID { get; set; }
		public string Name { get; set; }
	}
}
