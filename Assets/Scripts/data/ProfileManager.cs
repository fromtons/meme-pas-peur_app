using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
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
				Debug.Log ("New current profile is now : " + _currentProfile.name);
				Save ();
			}
		}

		List<Profile> _availableProfiles;
		public List<Profile> AvailableProfiles {
			get { return _availableProfiles; }
		}


		// Use this for initialization
		void OnEnable () {
			// Singleton
			if (instance == null) {
				instance = this;
				DontDestroyOnLoad (this.gameObject);
			} else {
				Destroy (this.gameObject);
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

		public void SetCurrentProfile(string profileName) {
			CurrentProfile = LoadProfile (profileName);
		}
		
		public Profile LoadProfile (string fileName) {
			if (File.Exists (Application.persistentDataPath + "/" + fileName)) {
				BinaryFormatter bf = new BinaryFormatter ();
				FileStream file = File.Open (Application.persistentDataPath + "/" + fileName, FileMode.Open);
				ProfileData data = (ProfileData)bf.Deserialize (file);
				file.Close ();

				return new Profile (data);
			} else {
				Debug.LogError ("File doesn't exists");
				return null;
			}
		}

		public void GetProfiles() {
			_availableProfiles = new List<Profile> ();
			DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath + "/");
			FileInfo[] info = dir.GetFiles("*.dat");
			foreach (FileInfo f in info) 
			{ 
				_availableProfiles.Add (LoadProfile(f.Name));
			}
		}
	}

	[Serializable]
	public class ProfileData {
		public string name;
		public int age;
		public int difficulty;
		string _picturePath;

		public Texture2D picture {
			get {
				if (_picturePath == null || _picturePath.Length <= 0 || !File.Exists (_picturePath))
					return null;
				
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
