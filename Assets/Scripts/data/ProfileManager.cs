using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MPP.Data {
	public class ProfileManager : MonoBehaviour {
		
		public static ProfileManager instance;
		Profile _currentProfile;

		public Profile CurrentProfile {
			get {
				return _currentProfile;
			}
			set {
				_currentProfile = value;
				Save ();
			}
		}

		// Use this for initialization
		void Start () {
			// Singleton
			if (instance == null) {
				instance = this;
			} else {
				Destroy (this);
			}
		}

		public void Save() {
			if (CurrentProfile == null)
				return;

			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Create(Application.persistentDataPath + "/"+CurrentProfile.name+".dat");

			ProfileData data = new ProfileData ();
			data.Fill (CurrentProfile);

			bf.Serialize(file, data);
			file.Close();
		}
		
		public void Load(string fileName) {
			if (File.Exists (Application.persistentDataPath + "/" + fileName)) {
				BinaryFormatter bf = new BinaryFormatter ();
				FileStream file = File.Open (Application.persistentDataPath + "/" + fileName, FileMode.Open);
				ProfileData data = (ProfileData)bf.Deserialize (file);
				file.Close ();

				CurrentProfile = new Profile (data.name, data.age, data.difficulty, data.picture);
			} else {
				Debug.LogError ("File doesn't exists");
			}
		}
	}

	[Serializable]
	class ProfileData {
		public string name;
		public int age;
		public int difficulty;
		string _picturePath;

		public Texture2D picture {
			get {
				byte[] jpg = File.ReadAllBytes (_picturePath);
				Texture2D texture = new Texture2D (1, 1);
				texture.LoadImage (jpg);
				return texture;
			}
			set {
				byte[] jpg = value.EncodeToJPG();
				_picturePath = Application.persistentDataPath + "/" + name + ".jpg";
				File.WriteAllBytes (_picturePath, jpg);
			}
		}

		public void Fill(Profile profile) {
			if(profile.name != null) this.name = profile.name;
			if(profile.age != null) this.age = profile.age;
			if(profile.difficulty != null) this.difficulty = profile.difficulty;
			if(profile.picture != null) this.picture = profile.picture;
		}
	}
}
