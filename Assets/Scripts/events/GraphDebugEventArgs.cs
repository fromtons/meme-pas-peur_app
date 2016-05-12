using UnityEngine;
using System;
using System.Collections;

public class GraphDebugEventArgs : EventArgs {
	public string ID { get; set; }
	public float Value { get; set; }
	public float[] Values { get; set; }
}
