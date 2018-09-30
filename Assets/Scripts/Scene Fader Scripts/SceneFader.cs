using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFader : MonoBehaviour {

	public static SceneFader instance;

	[SerializeField]
	private GameObject panel;

	[SerializeField]
	private Animator anim;

	// Use this for initialization
	void Awake () {
		MakeSingleton ();
	}

	void MakeSingleton () {
		if (instance != null)
			Destroy (gameObject);
		else {
			instance = this;
			DontDestroyOnLoad (instance);
		}
	}

	public void LoadLevel (string level) {
		StartCoroutine (FadeInOut (level));
	}

	IEnumerator FadeInOut(string level) {
		panel.SetActive (true);
		anim.Play ("FadeIn");
		yield return StartCoroutine (MyCoroutine.WaitForRealSeconds (1f));

		Application.LoadLevel (level);
		anim.Play ("FadeOut");
		yield return StartCoroutine (MyCoroutine.WaitForRealSeconds (0.7f));

		panel.SetActive (false);
	}
}
