using UnityEngine;
using System.Collections;
using ProtoTurtle.BitmapDrawing;

[RequireComponent(typeof(AudioSource))]
public class MicrophoneInput : MonoBehaviour {
	public float sensitivity = 100f;
	public float loudness = 0;
	
	float deviceFactor = 100f;
	bool deviceFactorActive = true;
	
	bool debug = true;

	AudioSource audio;

	void Start() {

		if(deviceFactorActive) { 
			#if UNITY_EDITOR
				deviceFactor=70f;
			#elif UNITY_IOS 
				deviceFactor=35f;
			#endif
		}
		
		audio = this.GetComponent<AudioSource> ();
		audio.clip = Microphone.Start(Microphone.devices[0], true, 	1, 44100);
		audio.loop = true; // Set the AudioClip to loop
		//audio.mute = true; // Mute the sound, we don't want the player to hear it
		while (!(Microphone.GetPosition(Microphone.devices[0]) > 0)){} // Wait until the recording has started
		audio.Play(); // Play the audio source!
	}

	void Update(){
		loudness = GetAveragedVolume() * (sensitivity * (sensitivity/deviceFactor));
	}

	float GetAveragedVolume()
	{ 
		float[] data = new float[256];
		float a = 0;
		audio.GetOutputData(data,0);
		foreach(float s in data)
		{
			a += Mathf.Abs(s);
		}
		return a/256;
	}
	
	// TODO - Externalise this graph debugger
	
	private static Texture2D _staticRectTexture;
	private static Texture2D _newStaticRectTexture;
    private static GUIStyle _staticRectStyle;
	
	int debugFrom = 0;
	int debugTo = 100;
	int dividers = 10;
	
	int newValue;
	
	float newValueMinusMin;
	float maxValueMinusMin;
	
	void OnGUI() {
		if(debug) {
			// Init
			if( _staticRectTexture == null ) {
				_staticRectTexture = new Texture2D( 100, 100);
				_staticRectTexture.DrawFilledRectangle(new Rect(0,0,_staticRectTexture.width, _staticRectTexture.height), Color.black);
			}
			if( _newStaticRectTexture == null ) _newStaticRectTexture = new Texture2D( 100, 100);
			if( _staticRectStyle == null ) _staticRectStyle = new GUIStyle();
			
			// Offsets to the left
			for(int y = 0; y < _newStaticRectTexture.height; y++) {
				for(int x = 1; x < _newStaticRectTexture.width; x++) {
					_newStaticRectTexture.SetPixel(x-1,y, _staticRectTexture.GetPixel(x,y));
				}
			}
			
			// Calculate position
			newValueMinusMin = loudness - debugFrom;
			maxValueMinusMin = debugTo - debugFrom;
			if(newValueMinusMin < 0) newValueMinusMin = 0;
			if(maxValueMinusMin < 0) maxValueMinusMin = 0;
			if(newValueMinusMin > maxValueMinusMin) newValueMinusMin = maxValueMinusMin;
			newValue = (int) Mathf.Round((newValueMinusMin) * 100 / maxValueMinusMin);
			
			// Draw current line
			_newStaticRectTexture.DrawFilledRectangle(new Rect(_staticRectTexture.width-1, 0, 1, _staticRectTexture.height), Color.black);
			_newStaticRectTexture.DrawFilledRectangle(new Rect(_staticRectTexture.width-1, (_staticRectTexture.height - newValue), 1, newValue), Color.red);
			
			for(int i = 0; i < dividers; i++) {
				_newStaticRectTexture.DrawFilledRectangle(new Rect(0, (int) (i*_staticRectTexture.height/dividers), _staticRectTexture.width, 1), Color.grey);
			}
			
			_staticRectTexture = _newStaticRectTexture;
        	_staticRectTexture.Apply();
        	_staticRectStyle.normal.background = _staticRectTexture;
 
        	GUI.Box( new Rect(0,0,100,100), GUIContent.none, _staticRectStyle );
		}
	}
}