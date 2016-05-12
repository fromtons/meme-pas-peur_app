using UnityEngine;
using System.Collections;

public class SoundCap : MonoBehaviour {
	
	public string ID;
	public MicrophoneInput micIn;
	public float soundCap;
	
	bool wasAbove = false;
	
	// Update is called once per frame
	void Update () {
		if(CapCheck()) {
			if(!wasAbove) {
				MicEventManager.TriggerSoundCapBegin(new MicEventArgs { OriginID = this.ID});
				wasAbove = true;	
			}
						
			MicEventManager.TriggerSoundCap(new MicEventArgs { OriginID = this.ID});
		} else if(wasAbove) {
			MicEventManager.TriggerSoundCapEnd(new MicEventArgs { OriginID = this.ID});
			wasAbove = false;
		}
	}
	
	bool CapCheck() {   
		return (micIn.loudness > soundCap);
	}
}
