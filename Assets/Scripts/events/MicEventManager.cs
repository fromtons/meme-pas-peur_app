using UnityEngine;
using System.Collections;

public static class MicEventManager {

	public delegate void MicEvent(MicEventArgs eventArgs);

	public static event MicEvent SoundCap, SoundCapBegin, SoundCapEnd, Blow, BlowBegin, BlowEnd;

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

	public static void TriggerBlowBegin(MicEventArgs eventArgs = null) {
		if (BlowBegin != null)
			BlowBegin (eventArgs);           

		Debug.Log ("blow_begin");
	}

	public static void TriggerBlow(MicEventArgs eventArgs = null) {
		if (Blow != null)
			Blow (eventArgs);         

		Debug.Log ("blow");
	}

	public static void TriggerBlowEnd(MicEventArgs eventArgs = null) {
		if (BlowEnd != null)
			BlowEnd (eventArgs);

		Debug.Log ("blow_end");
	}
}
