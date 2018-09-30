using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour {

// Attributes
	public static GameplayController instance;

	[SerializeField]
	private Text scoreText, coinText, lifeText, gameOverScoreText, gameOverCoinText;

	[SerializeField]
	private GameObject pausePanel, gameOverPanel, readyButton;

//	[SerializeField]
//	private Button readyButton;

// Methods
	void Awake () {
		MakeInstance ();
	}

	void Start () {
		Time.timeScale = 0f;
	}

	void MakeInstance () {
		if (instance == null) {
			instance = this;
		}
	}

	public void GameOverShowPanel (int finalScore, int finalCoinScore) {
		gameOverPanel.SetActive (true);
		gameOverScoreText.text = finalScore.ToString ();
		gameOverCoinText.text = finalCoinScore.ToString ();
		StartCoroutine (GameOverLoadMainMenu ());
	}

	IEnumerator GameOverLoadMainMenu () {
		yield return new WaitForSeconds (3f);
//		Application.LoadLevel ("MainMenu");
		SceneFader.instance.LoadLevel ("MainMenu");
	}

	public void PlayerDiedRestartTheGame () {
		StartCoroutine (PlayerDiedRestart ());
	}

	IEnumerator PlayerDiedRestart () {
		yield return new WaitForSeconds (1f);
//		Application.LoadLevel ("Gameplay");
		SceneFader.instance.LoadLevel ("Gameplay");
	}


	public void SetScore (int score) {
		scoreText.text = "x" + score;
	}

	public void SetCoinScore (int coinScore) {
		coinText.text = "x" + coinScore;
	}

	public void SetLifeScore (int lifeScore) {
		lifeText.text = "x" + lifeScore;
	}


	public void PauseTheGame () {
		Time.timeScale = 0f;
		pausePanel.SetActive (true);
	}

	public void ResumeTheGame () {
		Time.timeScale = 1f;
		pausePanel.SetActive (false);
	}

	public void QuitGame () {
		Time.timeScale = 1f;
//		Application.LoadLevel ("MainMenu");
		SceneFader.instance.LoadLevel ("MainMenu");
	}

	public void StartTheGame () {
		Time.timeScale = 1f;
		readyButton.SetActive (false);
	}

}
