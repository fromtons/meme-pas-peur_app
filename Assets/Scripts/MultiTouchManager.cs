using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MultiTouchManager : MonoBehaviour {

	public GameObject prefab;
	public LayerMask touchInputMask;

	private Dictionary<int, GameObject> prefabInstances;

	// Use this for initialization
	void Start () {
		prefabInstances = new Dictionary<int, GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Touch touch in Input.touches) {
			switch (touch.phase) {
				case TouchPhase.Began: // TOUCH BEGAN
				
					// Instatiate a prefab and add it to our dictionnary
					prefabInstances [touch.fingerId] = Instantiate (prefab);

					// Generates random color and apply it
					Color color = new Color ();
					color.r = Random.Range (0f, 1f);
					color.g = Random.Range (0f, 1f);
					color.b = Random.Range (0f, 1f);
					color.a = 1f;
					((SpriteRenderer)prefabInstances [touch.fingerId].GetComponent<SpriteRenderer> ()).color = color;

					break;
				case TouchPhase.Stationary:
				case TouchPhase.Moved: // TOUCH MOVE
					// Move the prefad to stay under the finger	
					prefabInstances [touch.fingerId].transform.position = Camera.main.ScreenToWorldPoint (new Vector3 (touch.position.x, touch.position.y, 90f));
					break;
				case TouchPhase.Ended:
				case TouchPhase.Canceled: // TOUCH ENDS
					// Destroy the prefab 
					Destroy (prefabInstances [touch.fingerId]);
					// Make sure it's not in the dictionnary anymore
					prefabInstances.Remove(touch.fingerId);
					break;
			}
		}
	}
}
