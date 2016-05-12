using System;
using UnityEngine;
using System.Collections;
using ProtoTurtle.BitmapDrawing;

public class GraphDebug : MonoBehaviour
{
	private static Texture2D _staticRectTexture;
	private static Texture2D _newStaticRectTexture;
	private static GUIStyle _staticRectStyle;
	private int value;

	public string ID;
	public int debugFrom = 0;
	public int debugTo = 100;
	public int dividers = 10;

	public int posX = 0;
	public int posY = 0;
	public int sizeX = 100;
	public int sizeY = 100;

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
		GraphDebugEventManager.Update += onUpdate;
	}

	// On VALUE update 
	void OnUpdate(GraphDebugEventArgs eventArgs) {
		if (eventArgs.ID == this.ID) {
			this.value = (int) eventArgs.Value;
		}
	}
		
	void OnGUI() {
		// Offsets to the left
		for(int y = 0; y < _newStaticRectTexture.height; y++) {
			for(int x = 1; x < _newStaticRectTexture.width; x++) {
				_newStaticRectTexture.SetPixel(x-1,y, _staticRectTexture.GetPixel(x,y));
			}
		}

		// Calculate data position
		newValueMinusMin = value - debugFrom;
		maxValueMinusMin = debugTo - debugFrom;
		if(newValueMinusMin < 0) newValueMinusMin = 0;
		if(maxValueMinusMin < 0) maxValueMinusMin = 0;
		if(newValueMinusMin > maxValueMinusMin) newValueMinusMin = maxValueMinusMin;
		newValue = (int) Mathf.Round((newValueMinusMin) * sizeY / maxValueMinusMin);

		// Draw current line
		_newStaticRectTexture.DrawFilledRectangle(new Rect(_staticRectTexture.width-1, 0, 1, _staticRectTexture.height), Color.black);
		_newStaticRectTexture.DrawFilledRectangle(new Rect(_staticRectTexture.width-1, (_staticRectTexture.height - newValue), 1, newValue), Color.red);

		for(int i = 0; i < dividers; i++) {
			_newStaticRectTexture.DrawFilledRectangle(new Rect(0, (int) (i*_staticRectTexture.height/dividers), _staticRectTexture.width, 1), Color.grey);
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