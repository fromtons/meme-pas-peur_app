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
	public RawImage recapAgeIcon;
	public RawImage recapDifficultyIcon;

	public List<Texture2D> ageIcons;
	public List<Texture2D> difficultyIcons;

	void Start() {
		_profile = new Profile ();
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
			recapAgeIcon.texture = ageIcons [0];
				break;
			case 4:
			case 5:
			case 6:
				recapAgeText.text = _profile.age+"";
				recapAgeIcon.texture = ageIcons [age - 3];
				break;
			case 7:
				recapAgeIcon.texture = ageIcons [4];
				recapAgeText.text = "Plus de 6";
				break;
		}
	}

	public void SetDifficulty(int diff) {
		// Model
		_profile.difficulty = diff;
		// View
		recapDifficultyIcon.texture = difficultyIcons [diff];
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
		_profile.picture = pictureInput.texture as Texture2D;
		// View
		recapPicture.texture = _profile.picture;
	}

	public void Save() {
		ProfileManager.instance.CurrentProfile = this._profile;
	}
}
