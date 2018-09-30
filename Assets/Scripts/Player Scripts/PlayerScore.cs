using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour {

// Members
	[SerializeField]
	private AudioClip coinClip, lifeClip;
	private CameraScript cameraScript;
	private Vector3 previousPosition;
	private bool countScore;

	public static int lifeCount, coinCount, scoreCount;

// Methods

	void Awake () {
		cameraScript = Camera.main.GetComponent<CameraScript> ();
	}

	void Start () {
		previousPosition = transform.position;
		countScore = true; 
	}
	// Update is called once per frame
	void Update () {
		CountScore ();
	}

	void CountScore () {
		if (countScore) {
			if (transform.position.y < previousPosition.y)
				scoreCount++;
		}
		previousPosition = transform.position;
		GameplayController.instance.SetScore (scoreCount);
	}

	void OnTriggerEnter2D(Collider2D target) {
		if (target.tag == "Coin") {
			coinCount++;
			scoreCount += 200;

			GameplayController.instance.SetScore (scoreCount);
			GameplayController.instance.SetCoinScore (coinCount);

			AudioSource.PlayClipAtPoint (coinClip, transform.position);
			target.gameObject.SetActive (false);
		} else if (target.tag == "Life") {
			lifeCount++;
			scoreCount += 300;

			GameplayController.instance.SetScore (scoreCount);
			GameplayController.instance.SetLifeScore (lifeCount);


			AudioSource.PlayClipAtPoint (lifeClip, transform.position);
			target.gameObject.SetActive (false);
		} else if (target.tag == "Bounds" || target.tag == "Deadly") {
			cameraScript.moveCamera = false;
			countScore = false;
			lifeCount--;

			transform.position = new Vector3 (500, 500, 0);
			GameManager.instance.CheckGameStatus (scoreCount, coinCount, lifeCount);

		}
	}
}
