using UnityEngine;
using System.Collections;

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
}
