using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using MPP.Data;

public class CreateProfile : MonoBehaviour {

	public Text nameInput;
	public RawImage pictureInput;

	Profile _profile;
	public Profile Profile {
		get {
			return _profile;
		}
	}

	public Text recapNameText;
	public Text recapAgeText;
	public Text recapDifficultyText;
	public RawImage recapPicture;

	public List<GameObject> ageIcons;
	public List<GameObject> difficultyIcons;

	void Start() {
		_profile = new Profile ();
	}

	void ChangeAgeIcon(int icon) {
		foreach(GameObject ageIcon in ageIcons) {
			ageIcon.SetActive(false);
		}
		ageIcons[icon].SetActive(true);
	}

	void ChangeDiffIcon(int icon) {
		foreach(GameObject difficultyIcon in difficultyIcons) {
			difficultyIcon.SetActive(false);
		}
		difficultyIcons[icon].SetActive(true);
	}

	public void SetName() {
		// Model
		_profile.name = nameInput.text;
		// View
		recapNameText.text = _profile.name;
	}
	
	public void SetAge(int age) {
		// Model
		_profile.age = age;
		// View
		switch (age) {
			case 3:
				recapAgeText.text = "Moins de 4";
				ChangeAgeIcon(0);
				break;
			case 4:
			case 5:
			case 6:
				recapAgeText.text = _profile.age+"";
				ChangeAgeIcon(age - 3);
				break;
			case 7:
				ChangeAgeIcon(4);
				recapAgeText.text = "Plus de 6";
				break;
		}
	}

	public void SetDifficulty(int diff) {
		// Model
		_profile.difficulty = diff;
		// View
		ChangeDiffIcon(diff);
		switch (diff) {
			case 0:
			recapDifficultyText.text = "Farouche";
				break;
			case 1:
			recapDifficultyText.text = "Sur ses gardes";
				break;
			case 2:
			recapDifficultyText.text = "Téméraire";
				break;
		}
	}

	public void SetPicture() {
		// TODO - Crop a square format at center
		// Model
		_profile.picture = MPP.Util.Texture2DUtils.CropSquare(pictureInput.texture as Texture2D);
		// View
		recapPicture.texture = _profile.picture;
		Save ();
	}

	public void Save() {
		ProfileManager.instance.CurrentProfile = this._profile;
	}
}
