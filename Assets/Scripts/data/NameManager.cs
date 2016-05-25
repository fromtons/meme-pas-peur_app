using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MPP.Data {
	public class NameManager : MonoBehaviour {

		public SpriteRenderer spriteToSave;
		InputField inputField;

		// Use this for initialization
		void Start () {
			inputField = (InputField)this.GetComponent<InputField> ();

			this.Load ();
		}

		public void Save() {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Create(Application.persistentDataPath + "/ProfileData.dat");

			ProfileData data = new ProfileData ();
			data.name = inputField.text;
			data.picture = spriteToSave.sprite.texture;

			bf.Serialize(file, data);
			file.Close();
		}
		
		public void Load() {
			if (File.Exists (Application.persistentDataPath + "/ProfileData.dat")) {
				BinaryFormatter bf = new BinaryFormatter ();
				FileStream file = File.Open(Application.persistentDataPath + "/ProfileData.dat", FileMode.Open);
				ProfileData data = (ProfileData) bf.Deserialize(file);
				file.Close();

				spriteToSave.sprite = Sprite.Create (data.picture, new Rect (0, 0, data.picture.width, data.picture.height), new Vector2 (0.5f, 0.5f));
				inputField.text = data.name;
			}
		}
	}

	[Serializable]
	class ProfileData {
		public string name;
		public int age;
		public string picturePath;

		public Texture2D picture {
			get {
				byte[] jpg = File.ReadAllBytes (picturePath);
				Texture2D texture = new Texture2D (1, 1);
				texture.LoadImage (jpg);
				return texture;
			}
			set {
				byte[] jpg = value.EncodeToJPG();
				picturePath = Application.persistentDataPath + "/" + name + ".jpg";
				File.WriteAllBytes (picturePath, jpg);
			}
		}
	}
}
