using UnityEngine;
using System.Collections;

namespace MPP.Data {
	public class Profile  {

		public string name;
		public int age;
		public int difficulty;
		public Texture2D picture;

		public Profile() { }

		public Profile (string name) {
			this.name = name;
		}

		public Profile (string name, int age, int difficulty, Texture2D picture) {
			this.name = name;
			this.age = age;
			this.difficulty = difficulty;
			this.picture = picture;
		}

		public Profile(ProfileData data) {
			if (data.name != null)
				this.name = data.name;
			if (data.age != null)
				this.age = data.age;
			if (data.difficulty != null)
				this.difficulty = data.difficulty;
			if (data.picture != null)
				this.picture = data.picture;
		}
	}
}