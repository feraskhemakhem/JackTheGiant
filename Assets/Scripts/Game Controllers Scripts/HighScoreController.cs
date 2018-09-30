using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreController : MonoBehaviour {

	[SerializeField]
	private Text scoreText, coinText;

	// Use this for initialization
	void Start () {
		SetScoreBasedOnDifficulty ();
	}

	void SetScore (int score, int coinScore) {
		scoreText.text = score.ToString ();
		coinText.text = coinScore.ToString ();
	}

	void SetScoreBasedOnDifficulty () {
		if (GamePreferences.GetEasyDifficultyState () == 1) {
			SetScore (GamePreferences.GetEasyDifficultyHighScore (), GamePreferences.GetEasyDifficultyCoinScore ());
		} else if (GamePreferences.GetMediumDifficultyState () == 1) {
			SetScore (GamePreferences.GetMediumDifficultyHighScore (), GamePreferences.GetMediumDifficultyCoinScore ());
		} else if (GamePreferences.GetHardDifficultyState () == 1) {
			SetScore (GamePreferences.GetHardDifficultyHighScore (), GamePreferences.GetHardDifficultyCoinScore ());
		} else
			Debug.Log ("HighScoreController");
	}
		
	
	public void GoBackToMainMenu () {
		Application.LoadLevel ("MainMenu");
	}
}
