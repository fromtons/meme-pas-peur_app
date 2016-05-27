using System;
using UnityEngine;

namespace MPP.Util
{
	public static class Texture2DUtils
	{
		public static Texture2D CropSquare(Texture2D texture) {
			bool landscape = texture.width > texture.height;
			int size = landscape ? texture.height : texture.width;

			Texture2D newTexture = new Texture2D (size, size);

			int minY = 0;
			int minX = 0;
			if (landscape) {
				minX = (texture.width / 2) - (size / 2);
			} else {
				minY = (texture.height / 2) - (size / 2);
			}

			int newY = 0;
			int newX = 0;
			for (int y = minY; y < (minY+size); y++) {
				newX = 0;
				for (int x = minX; x < (minX+size); x++) {
					newTexture.SetPixel(newX, newY, texture.GetPixel(x,y));
					newX++;
				}
				newY++;
			}
			newTexture.Apply ();

			return newTexture;
		}
	}
}

