using UnityEngine;
using System;
using System.Collections;

namespace MPP.Events {
	public class GraphDebugEventArgs : EventArgs {
		public string ID { get; set; }
		public float Value { get; set; }
		public float[] Values { get; set; }
	}
}
