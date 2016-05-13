using UnityEngine;
using System.Collections;

namespace MPP.Drawing {
	public class ColorChanger : MonoBehaviour {

		public Color color;
		public DrawingZone drawingZone;

		public void OnClick() {
			drawingZone.color = color;
		}
	}
}