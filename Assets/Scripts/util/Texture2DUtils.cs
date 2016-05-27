using System;
using UnityEngine;
using ProtoTurtle.BitmapDrawing;

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

		public static Texture2D RotateLeft(Texture2D texture) {
			Texture2D newTexture = new Texture2D (texture.height, texture.width);

			for (int y = 0; y < texture.height; y++) {
				for (int x = 0; x < texture.width; x++) {
					BitmapDrawingExtensions.DrawPixel (newTexture, y, newTexture.height - x, BitmapDrawingExtensions.GetPixel (texture, x, y));
				}
			}

			newTexture.Apply ();
			return newTexture;
		}
	}
}

