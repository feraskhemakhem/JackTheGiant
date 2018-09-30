using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour {

// Variables
	[SerializeField]
	private GameObject[] clouds, collectables;

	private float distanceBetweenClouds = 3f;
	private float minX, maxX, lastCloudPositionY;
	private int controlX;

	private GameObject player;

// Functions
	void Awake () {
		controlX = 0;
		SetMinAndMaxX ();
		CreateClouds ();
		player = GameObject.Find ("Player");

		for (int i = 0; i < collectables.Length; i++) {
			collectables [i].SetActive (false);		
		}
	}

	void Start () {
		PositionPlayer ();
	}


	void SetMinAndMaxX () {
		Vector3 bounds = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0));

		maxX = bounds.x - 0.5f;
		minX = -maxX;
	}


	void Shuffle (GameObject[] array) {
		for (int i = 0; i < array.Length; i++) {
			GameObject temp = array[i];
			int random = Random.Range(i, array.Length);
			array[i] = array[random];
			array[random] = temp;
		}

	}


	void CreateClouds () {
		
		Shuffle (clouds);

		float positionY = 0f;

		for (int i = 0; i < clouds.Length; i++) {
			Vector3 temp = clouds [i].transform.position;
			temp.y = positionY;

			switch (controlX) {

			case 0:
				temp.x = Random.Range (0.0f, maxX);
				controlX = 1; 
				break;
			case 1:
				temp.x = Random.Range (0.0f, minX);
				controlX = 2; 
				break;
			case 2:
				temp.x = Random.Range (1.0f, maxX);
				controlX = 3; 
				break;
			case 3:
				temp.x = Random.Range (-1.0f, minX);
				controlX = 0; 
				break;
			default:
				Debug.Log ("Oops./n");
				break;
			}
				 
			lastCloudPositionY = positionY;
			clouds[i].transform.position = temp;  
			positionY -= distanceBetweenClouds;
		}
	}


	void PositionPlayer () {
		
		GameObject[] darkClouds = GameObject.FindGameObjectsWithTag ("Deadly");
		GameObject[] cloudsInGame = GameObject.FindGameObjectsWithTag ("Cloud");

		for (int i = 0; i < darkClouds.Length; i++) {

			if (darkClouds [i].transform.position.y == 0f) {
				Vector3 t = darkClouds [i].transform.position;
				darkClouds [i].transform.position = new Vector3 (cloudsInGame [0].transform.position.x, 
																 cloudsInGame [0].transform.position.y, 
																 cloudsInGame [0].transform.position.z);
				cloudsInGame [0].transform.position = t;
			}
		}

		Vector3 temp = cloudsInGame [0].transform.position;

		for (int i = 1; i < cloudsInGame.Length; i++) {
			if (temp.y < cloudsInGame [i].transform.position.y)
				temp = cloudsInGame [i].transform.position;
		}
			
		temp.y += 0.8f;

		player.transform.position = temp;
	}


	void OnTriggerEnter2D (Collider2D target) {
	
		if (target.tag == "Cloud" || target.tag == "Deadly") {

			if (target.transform.position.y == lastCloudPositionY) {
				Shuffle (clouds);
				Shuffle (collectables);

				Vector3 temp = target.transform.position;

				for (int i = 0; i < clouds.Length; i++) {
					if (!clouds [i].activeInHierarchy) {
						switch (controlX) {

						case 0:
							temp.x = Random.Range (0.0f, maxX);
							controlX = 1; 
							break;
						case 1:
							temp.x = Random.Range (0.0f, minX);
							controlX = 2; 
							break;
						case 2:
							temp.x = Random.Range (1.0f, maxX);
							controlX = 3; 
							break;
						case 3:
							temp.x = Random.Range (-1.0f, minX);
							controlX = 0; 
							break;
						default:
							Debug.Log ("CloudSpawner!");
							break;
						}
					
						temp.y -= distanceBetweenClouds;
						lastCloudPositionY = temp.y;
						clouds [i].transform.position = temp;
						clouds [i].SetActive (true);

						int random = Random.Range (0, collectables.Length);

						if (clouds [i].tag != "Deadly") { 

								if (!collectables [random].activeInHierarchy) {
								
									Vector3 temp2 = clouds [i].transform.position;
									temp2.y += 0.7f;

									if (collectables [random].tag == "Life") {

										if (PlayerScore .lifeCount < 2) {
											collectables [random].transform.position = temp2;
											collectables [random].SetActive (true);
										}

									} else {
										collectables [random].transform.position = temp2;
										collectables [random].SetActive (true);
								}
							}
						}

					}
				}
			}
		}
	}

}
