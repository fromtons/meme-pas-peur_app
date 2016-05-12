using UnityEngine;
using System.Collections;

public static class MicEventManager {

	public delegate void MicEvent(MicEventArgs eventArgs);

	public static event MicEvent SoundCap, SoundCapBegin, SoundCapEnd;

    public static void TriggerSoundCapBegin(MicEventArgs eventArgs = null) {
		if (SoundCapBegin != null)
			SoundCapBegin (eventArgs);           
	}

	public static void TriggerSoundCap(MicEventArgs eventArgs = null) {
		if (SoundCap != null)
			SoundCap (eventArgs);            
	}
    
    public static void TriggerSoundCapEnd(MicEventArgs eventArgs = null) {
		if (SoundCapEnd != null)
			SoundCapEnd (eventArgs);                
	}
}
