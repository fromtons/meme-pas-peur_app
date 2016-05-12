using UnityEngine;
using System.Collections;
using System;
using ProtoTurtle.BitmapDrawing;

public class SpectrumDebug : MonoBehaviour {
	private static Texture2D _staticRectTexture;
	private static Texture2D _newStaticRectTexture;
	private static GUIStyle _staticRectStyle;

	private float[] values;
	private float value;
	private static int AMPLIFICATION = 1000;

	public string ID;
	public int debugFrom = 0;
	public int debugTo = 100;
	public int dividers = 10;

	public int posX = 0;
	public int posY = 0;
	int sizeX = 256;
	int sizeY = 100;

	// Usable vars
	int newValue;
	float newValueMinusMin;
	float maxValueMinusMin;

	GraphDebugEventManager.GraphDebugEvent onUpdate;

	void Start()
	{
		_staticRectTexture = new Texture2D( sizeX, sizeY);
		_staticRectTexture.DrawFilledRectangle(new Rect(0,0,_staticRectTexture.width, _staticRectTexture.height), Color.black);

		_newStaticRectTexture = new Texture2D( sizeX, sizeY);
		_staticRectStyle = new GUIStyle();

		onUpdate = new GraphDebugEventManager.GraphDebugEvent (OnUpdate);
		GraphDebugEventManager.UpdateSpectrum += onUpdate;
	}

	// On VALUE update 
	void OnUpdate(GraphDebugEventArgs eventArgs) {
		if (eventArgs.ID == this.ID) {
			//this.value = (int) eventArgs.Value;
			this.values = eventArgs.Values;
		}
	}

	void OnGUI() {
		// Black background
		_staticRectTexture.DrawFilledRectangle(new Rect(0,0,_staticRectTexture.width, _staticRectTexture.height), Color.black);

		// Calculate data position
		if (values != null && values.Length > 0) {
			for (int i = 0; (i < values.Length && i < _staticRectTexture.width); i++) {
				value = values [i] * AMPLIFICATION;
				newValueMinusMin = value - debugFrom;
				maxValueMinusMin = debugTo - debugFrom;
				if (newValueMinusMin < 0)
					newValueMinusMin = 0;
				if (maxValueMinusMin < 0)
					maxValueMinusMin = 0;
				if (newValueMinusMin > maxValueMinusMin)
					newValueMinusMin = maxValueMinusMin;
				newValue = (int)Mathf.Round ((newValueMinusMin) * sizeY / maxValueMinusMin);

				// Draw current line
				_newStaticRectTexture.DrawFilledRectangle (new Rect (i, 0, 1, _staticRectTexture.height), Color.black);
				_newStaticRectTexture.DrawFilledRectangle (new Rect (i, (_staticRectTexture.height - newValue), 1, newValue), Color.red);
			}
		}

		// Draw dividers
		for(int i = 0; i < dividers; i++) {
			_newStaticRectTexture.DrawFilledRectangle(new Rect((int) (i*_staticRectTexture.width/dividers), 0, 1, _staticRectTexture.height), Color.grey);
		}

		_staticRectTexture = _newStaticRectTexture;
		_staticRectTexture.Apply();
		_staticRectStyle.normal.background = _staticRectTexture;

		GUI.Box( new Rect(posX,posY,sizeX,sizeY), GUIContent.none, _staticRectStyle );
	}

	void OnDestroy() {
		GraphDebugEventManager.Update -= onUpdate;
	}
}
