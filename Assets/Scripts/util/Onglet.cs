using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Onglet : MonoBehaviour {

	public GameObject textObject;

	public Color backgroundDefault;
	public Color backgroundCurrent;
	public Color backgroundHighlight;

	public Color textDefault;
	public Color textCurrent;

	public static uint STATE_DEFAULT = 0;
	public static uint STATE_CURRENT = 1;
	public static uint STATE_HIGHLIGHT = 2;

	uint _currentState = STATE_DEFAULT;
	public uint CurrentState {
		get {
			return _currentState;
		}

		set {
			_currentState = value;

			if (value == STATE_DEFAULT) {
				background.color = backgroundDefault;
				textObject.GetComponent<Text>().color = textDefault;
			} else if (value == STATE_CURRENT) {
				background.color = backgroundCurrent;
				textObject.GetComponent<Text>().color = textCurrent;
			} else if (value == STATE_HIGHLIGHT) {
				background.color = backgroundHighlight;
				textObject.GetComponent<Text>().color = textDefault;
			}
		}
	}

	public Image background;
}
