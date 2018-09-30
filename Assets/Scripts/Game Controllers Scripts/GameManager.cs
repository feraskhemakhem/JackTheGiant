using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {

// Attributes

	public static GameManager instance;

	[HideInInspector]
	public bool gameStartedFromMainMenu, gameRestartedAfterPlayerDied;

	[HideInInspector]
	public int score, coinScore, lifeScore;

// Methods
	void Awake () {
		MakeSingleton ();
	}

	void Start () {
		InitialiseVariables ();
	}

	/// alternative to OnLevelWasLoaded
	void OnEnable () {
		SceneManager.sceneLoaded += LevelFinishedLoading;
	}

	void OnDisable () {
		SceneManager.sceneLoaded -= LevelFinishedLoading;
	}
	///

	void LevelFinishedLoading (Scene scene, LoadSceneMode mode) {
		if (scene.name == "Gameplay") {
		
			if (gameRestartedAfterPlayerDied) {
			
				GameplayController.instance.SetScore (score);
				GameplayController.instance.SetCoinScore (coinScore);
				GameplayController.instance.SetLifeScore (lifeScore);

				PlayerScore.scoreCount = score;
				PlayerScore.coinCount = coinScore;
				PlayerScore.lifeCount = lifeScore;

			} else if (gameStartedFromMainMenu) {

				PlayerScore.scoreCount = -1;
				PlayerScore.coinCount = 0;
				PlayerScore.lifeCount = 2;

				GameplayController.instance.SetScore (0);
				GameplayController.instance.SetCoinScore (0);
				GameplayController.instance.SetLifeScore (2);
			}
		} 
	}

	void InitialiseVariables () {

		if (!PlayerPrefs.HasKey ("Game Initialised")) {

			GamePreferences.SetEasyDifficultyState (0);
			GamePreferences.SetEasyDifficultyHighScore (0);
			GamePreferences.SetEasyDifficultyCoinScore (0);

			GamePreferences.SetMediumDifficultyState (1);
			GamePreferences.SetMediumDifficultyHighScore (0);
			GamePreferences.SetMediumDifficultyCoinScore (0);

			GamePreferences.SetHardDifficultyState (0);
			GamePreferences.SetHardDifficultyHighScore (0);
			GamePreferences.SetHardDifficultyCoinScore (0);

			GamePreferences.SetMusicState (0);

			PlayerPrefs.SetInt ("Game Initialised", 123);
		}


	}
	
	void MakeSingleton () {
		if (instance != null) {
			Destroy (gameObject);
		}
		else {
			instance = this;
			DontDestroyOnLoad (gameObject);
		}
	}

	public void CheckGameStatus (int score, int coinScore, int lifeScore) {
		
		if (lifeScore < 0) {
		
			if (GamePreferences.GetEasyDifficultyState () == 1) {
			
				if (GamePreferences.GetEasyDifficultyHighScore () < score)
					GamePreferences.SetEasyDifficultyHighScore (score);
				if (GamePreferences.GetEasyDifficultyCoinScore () < coinScore)
					GamePreferences.SetEasyDifficultyCoinScore (coinScore);
			
			} else if (GamePreferences.GetMediumDifficultyState () == 1) {

				if (GamePreferences.GetMediumDifficultyHighScore () < score)
					GamePreferences.SetMediumDifficultyHighScore (score);
				if (GamePreferences.GetMediumDifficultyCoinScore () < coinScore)
					GamePreferences.SetMediumDifficultyCoinScore (coinScore);

			} else if (GamePreferences.GetHardDifficultyState () == 1) {

				if (GamePreferences.GetHardDifficultyHighScore () < score)
					GamePreferences.SetHardDifficultyHighScore (score);
				if (GamePreferences.GetHardDifficultyCoinScore () < coinScore)
					GamePreferences.SetHardDifficultyCoinScore (coinScore);

			}

			gameStartedFromMainMenu = false;
			gameRestartedAfterPlayerDied = false;

			GameplayController.instance.GameOverShowPanel (score, coinScore);

		} else {
		
			this.score = score;
			this.coinScore = coinScore;
			this.lifeScore = lifeScore;

			gameStartedFromMainMenu = false;
			gameRestartedAfterPlayerDied = true;

			GameplayController.instance.PlayerDiedRestartTheGame ();

		}


	}

}
