using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsController : MonoBehaviour {

	[SerializeField]
	private GameObject easySign, mediumSign, hardSign;

	// Use this for initialization
	void Start () {
		SetDifficulty ();
	}

	void SetInitialDifficulty (string difficulty) {
		switch (difficulty) {
		case "easy":
//			easySign.SetActive (true);
			mediumSign.SetActive (false);
			hardSign.SetActive (false);
			break;
		case "medium":
			easySign.SetActive (false);
//			mediumSign.SetActive (true);
			hardSign.SetActive (false);
			break;
		case "hard":
			easySign.SetActive (false);
			mediumSign.SetActive (false);
//			hardSign.SetActive (true);
			break;
		default:
			Debug.Log ("OptionsController");
			break;
		}
	}

	void SetDifficulty () {
		if (GamePreferences.GetEasyDifficultyState () == 1) {
			SetInitialDifficulty ("easy");
		} else if (GamePreferences.GetMediumDifficultyState () == 1) {
			SetInitialDifficulty ("medium");
		} else if (GamePreferences.GetHardDifficultyState () == 1) {
			SetInitialDifficulty ("hard");
		}
	}

	public void EasyDifficulty () {
		GamePreferences.SetEasyDifficultyState (1);
		GamePreferences.SetMediumDifficultyState (0);
		GamePreferences.SetHardDifficultyState (0);

		easySign.SetActive (true);
		mediumSign.SetActive (false);
		hardSign.SetActive (false);
	}

	public void MediumDifficulty () {
		GamePreferences.SetEasyDifficultyState (0);
		GamePreferences.SetMediumDifficultyState (1);
		GamePreferences.SetHardDifficultyState (0);

		easySign.SetActive (false);
		mediumSign.SetActive (true);
		hardSign.SetActive (false);
	}

	public void HardDifficulty () {
		GamePreferences.SetEasyDifficultyState (0);
		GamePreferences.SetMediumDifficultyState (0);
		GamePreferences.SetHardDifficultyState (1);

		easySign.SetActive (false);
		mediumSign.SetActive (false);
		hardSign.SetActive (true);
	}

	public void GoBackToMainMenu () {
		Application.LoadLevel ("MainMenu");
	}
		
}
