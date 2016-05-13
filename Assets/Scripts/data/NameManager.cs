using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MPP.Data {
	public class NameManager : MonoBehaviour {

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

			bf.Serialize(file, data);
			file.Close();
		}
		
		public void Load() {
			if (File.Exists (Application.persistentDataPath + "/ProfileData.dat")) {
				BinaryFormatter bf = new BinaryFormatter ();
				FileStream file = File.Open(Application.persistentDataPath + "/ProfileData.dat", FileMode.Open);
				ProfileData data = (ProfileData) bf.Deserialize(file);
				file.Close();

				inputField.text = data.name;
			}
		}
	}

	[Serializable]
	class ProfileData {
		public string name;
	}
}
