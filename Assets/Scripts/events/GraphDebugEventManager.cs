using UnityEngine;
using System.Collections;

public static class GraphDebugEventManager {

	public delegate void GraphDebugEvent(GraphDebugEventArgs eventArgs);

	public static event GraphDebugEvent Update, UpdateSpectrum;

	public static void TriggerUpdate(GraphDebugEventArgs eventArgs = null) {
		if (Update != null)
			Update (eventArgs);           
	}

	public static void TriggerUpdateSpectrum(GraphDebugEventArgs eventArgs = null) {
		if (UpdateSpectrum != null)
			UpdateSpectrum (eventArgs);           
	}

}
