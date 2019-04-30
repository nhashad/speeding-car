using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

	static AudioSource mainTracks;
	public AudioSource mainTracks_;

	static public Rigidbody rigidbody;
	public float traSpeed;

	public  Text scoreText;

	static Camera camera1stPerson;
	public Camera camera1st;
	public Camera camera3rd;
	static Camera camera3rdPerson;

	public Transform obstacle;
	public Transform energycoin;
	public Transform radarlaser;

	static Transform car;
	static Transform coin;
	static Transform radar;

	public GameObject titlecanvas;
	public GameObject pausecanvas;
	public GameObject gameovercanvas;
	public GameObject scorecanvas;
	public GameObject gamecontrolscanvas;

	static GameObject titlescreen;
	static GameObject pausescreen;
	static GameObject gameoverscreen;
	static GameObject scorescreen;
	static GameObject gamecontrolscreen;

	static AudioSource powerupAudio;
	static AudioSource jumpingAudio;

	static int score;
	int speedScore;

	static bool StartGame;
	static bool Gameover;
	static bool Paused;
	static bool restarted;
	static bool muted;
	static bool scoreChange;
	static bool street1;
	static bool street2;
	bool nextAfterRestart;

	static float distanceTravelled;
	static Vector3 lastPosition;
	static Vector3 initialPosition;

	static Vector3 initialPosStreet1;
	static Vector3 initialPosStreet2;
	static GameObject streetOne;
	static GameObject streetTwo;

	// Use this for initialization
	void Start () {

		mainTracks = mainTracks_;
		camera1stPerson = camera1st;
		camera3rdPerson = camera3rd;

		car = obstacle;
		coin = energycoin;
		radar = radarlaser;

		titlescreen = titlecanvas;
		pausescreen = pausecanvas;
		gameoverscreen = gameovercanvas;
		scorescreen = scorecanvas;
		gamecontrolscreen = gamecontrolscanvas;

		distanceTravelled = 0;
		traSpeed = 20;
		speedScore = 1;
		score = 0;
		muted = false;

		camera1stPerson.enabled = true;
		camera3rdPerson.enabled = false;
		scoreChange = false;
		Gameover = false;
		StartGame = false;
		Paused = false;
		street2 = false;
		street1 = true;
		restarted = false;
		nextAfterRestart = false;

		powerupAudio = GetComponents<AudioSource> () [1];
		jumpingAudio = GetComponents<AudioSource> () [0];

		streetOne = GameObject.FindGameObjectWithTag ("Street1");
		initialPosStreet1 = streetOne.transform.position;
		streetTwo = GameObject.FindGameObjectWithTag ("Street2");
		initialPosStreet2 = streetTwo.transform.position;

		rigidbody = GetComponent<Rigidbody> ();

		lastPosition = transform.position;
		initialPosition = transform.position;

		scoreText.text = "Score: " + score;

	}
		
		
	// Update is called once per frame
	void Update () {

		if (nextAfterRestart) {
			restarted = false;
			nextAfterRestart = false;
		}
		if (restarted) {
			restartGame ();
		}
		
		if (!Gameover && !Paused && !restarted && StartGame) {
			if (scoreChange) {
				scoreText.text = "Score: " + score;
				scoreChange = false;

				int ratio = (int)(score / 50);
				speedScore = 1 + ratio;
			}

			// Infinite street
			distanceTravelled += (transform.position.z - lastPosition.z);
			lastPosition = transform.position;
			if (distanceTravelled >= 257 & street1) {
				street1 = false;
				street2 = true;
				streetOne.transform.Translate (0, 0, 500);
				distanceTravelled = 0;
				GenerateGameobjsOnAStreet (streetOne, false);
			} else if (distanceTravelled >= 243 & street2){
				street1 = true;
				street2 = false;
				streetTwo.transform.Translate (0, 0, 500);
				distanceTravelled = 0;
				GenerateGameobjsOnAStreet (streetTwo, false);
			}

			// Constant movement of the player forward
			float t =  speedScore * traSpeed * Time.deltaTime;
			transform.Translate (0, 0, t);


			// destroyal of game objs behind 
			foreach(GameObject o in GameObject.FindGameObjectsWithTag("Gameobjs")) {
				if (o.transform.position.z < transform.position.z - 30)
					GameObject.Destroy (o);
			}

			// movements
			if ((Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.LeftArrow) || Input.acceleration.x < -0.2) && transform.position.x > -30 && !(transform.position.y > initialPosition.y+1))
				transform.Translate(-30, 0, 0);

			if ((Input.GetKeyDown (KeyCode.D) || Input.GetKeyDown (KeyCode.RightArrow)|| Input.acceleration.x > 0.2) && transform.position.x < 20 && !(transform.position.y > initialPosition.y+1))
				transform.Translate(30, 0, 0);

			if (Input.GetKeyUp (KeyCode.Space)&& !(transform.position.y > 6)) {
				if (!muted)
					GetComponents<AudioSource>()[0].Play ();
				rigidbody.AddForce(0, 15, 0, ForceMode.Impulse);
			}
		}

		// switch between cameras
		if (Input.GetKeyUp(KeyCode.C)){
			switchCams ();
		}

		// (un)pausing
		if (Input.GetKeyDown (KeyCode.Escape)) {
			pause ();
		}

	}

	static public void start(){
		if (!StartGame) {
			StartGame = true;
			GenerateGameobjsOnAStreet (streetOne, true);
			GenerateGameobjsOnAStreet (streetTwo, false);
			if (!muted) {
				AudioSource gametrack = mainTracks.GetComponents<AudioSource> () [0];
				gametrack.loop = true;
				AudioSource slowtrack = mainTracks.GetComponents<AudioSource> () [1];
				slowtrack.Stop ();
				gametrack.Play ();
			}
			titlescreen.SetActive(false);
			scorescreen.SetActive(true);
			gamecontrolscreen.SetActive(true);

		}
	}

	static public void restart(){
		restarted = true;
	}

	public void muteToggled(bool on){
		AudioSource slowtrack = mainTracks.GetComponents<AudioSource> () [1];

		if (on) {
			slowtrack.Stop ();
		} else {
			slowtrack.Play ();
		}
		muted = on;
	}

	static public void quit(){

		foreach(GameObject o in GameObject.FindGameObjectsWithTag("Gameobjs")) {
			GameObject.Destroy (o);
		}

		lastPosition = initialPosition;
		rigidbody.transform.position = initialPosition;
		streetOne.transform.position = initialPosStreet1;
		streetTwo.transform.position = initialPosStreet2;

		street1 = true;
		street2 = false;

		distanceTravelled = 0;
		score = 0;
		scoreChange = true;

		Paused = false;
		Gameover = false;
		StartGame = false;

		scorescreen.SetActive(false);
		gamecontrolscreen.SetActive(false);
		pausescreen.SetActive (false);
		gameoverscreen.SetActive (false);

		titlescreen.SetActive (true);
	}

	void restartGame()
	{
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("Gameobjs")) {
			GameObject.Destroy (o);
		}

		lastPosition = initialPosition;
		rigidbody.transform.position = initialPosition;
		streetOne.transform.position = initialPosStreet1;
		streetTwo.transform.position = initialPosStreet2;

		street1 = true;
		street2 = false;
		GenerateGameobjsOnAStreet (streetOne, true);
		GenerateGameobjsOnAStreet (streetTwo, false);

		if (!muted) {
			AudioSource gametrack = mainTracks.GetComponents<AudioSource> () [0];
			gametrack.loop = true;
			AudioSource slowtrack = mainTracks.GetComponents<AudioSource> () [1];
			slowtrack.Stop ();
			gametrack.Play ();
		}
		distanceTravelled = 0;
		score = 0;
		scoreChange = true;

		Paused = false;
		Gameover = false;

		scorescreen.SetActive(true);
		gamecontrolscreen.SetActive(true);

		pausescreen.SetActive (false);
		gameoverscreen.SetActive (false);
		nextAfterRestart = true;
	}


	static public void ChangeScore(int diff){
		score += diff;
		if (score < 0)
			score = 0;
		scoreChange = true;
	}

	static public void GameIsover(){
		if (!muted) {
			AudioSource gametrack = mainTracks.GetComponents<AudioSource> () [0];
			AudioSource gameovertrack = mainTracks.GetComponents<AudioSource> () [1];
			gameovertrack.loop = true;
			gametrack.Stop ();
			gameovertrack.Play ();
		}
		Gameover = true;
		gamecontrolscreen.SetActive(false);
		gameoverscreen.SetActive(true);

	}

	static public bool IsMuted(){
		return muted;
	}

	static public bool IsGameOver(){
		return Gameover;
	}

	static public bool IsPaused(){
		return Paused;
	}

	static void GenerateGameobjsOnAStreet (GameObject Street, bool isFirst){
		int	laneC; 
		int	laneO;

		int numC = Random.Range (1,4);
		int numO = Random.Range (1,4);
		int numR = Random.Range (1,4);

		float distCoins = 10;
		float distCars = 20;
		float distRadars = 20;
		int lowerExtreme;
		if (isFirst)
			lowerExtreme = 105;
		else
			lowerExtreme = 120;
		
		float coins = Random.Range(Street.transform.position.z - lowerExtreme, Street.transform.position.z + 120);
		float c;
		for (int i = 0; i < numC; i++) {
			c = Random.Range(Street.transform.position.z - lowerExtreme, Street.transform.position.z + 120);
			if (Mathf.Abs (coins - c) >= distCoins) {
				laneC = Random.Range(1, 3);	
				if (laneC == 1)
					Instantiate(coin, new Vector3(-35, coin.transform.position.y, coins), coin.transform.rotation );
				else if (laneC == 2)
					Instantiate(coin, new Vector3(-5, coin.transform.position.y, coins), coin.transform.rotation );
				else
					Instantiate(coin, new Vector3(25, coin.transform.position.y, coins), coin.transform.rotation );

				coins = c;
			}
		}

		float obstacle = Random.Range(Street.transform.position.z - lowerExtreme, Street.transform.position.z + 120);
		float o;
		for (int i = 0; i < numO; i++) {
			o = Random.Range(Street.transform.position.z - lowerExtreme, Street.transform.position.z + 120);
			if (Mathf.Abs (obstacle - o) >= distCars) {
				laneO = Random.Range (1, 3);
				if (laneO == 1)
					Instantiate (car, new Vector3 (-35, car.transform.position.y, obstacle), Quaternion.identity);
				else if (laneO == 2)
					Instantiate (car, new Vector3 (-5, car.transform.position.y, obstacle), Quaternion.identity);
				else
					Instantiate (car, new Vector3 (25, car.transform.position.y, obstacle), Quaternion.identity);

				obstacle = o;
			}

		}

		float radarlaser = Random.Range(Street.transform.position.z - lowerExtreme, Street.transform.position.z + 120);
		float r;
		for (int i = 0; i < numR; i++) {
			r = Random.Range(Street.transform.position.z - lowerExtreme, Street.transform.position.z + 120);
			if (Mathf.Abs (radarlaser - r) >= distRadars) {
				Instantiate (radar, new Vector3 (radar.transform.position.x, radar.transform.position.y, radarlaser), radar.transform.rotation);
				radarlaser = r;
			}
		} 
	}

	static public void playScoreUp(){
		if (!muted)
			powerupAudio.Play ();
	}

	static public void jump(){
		if (!(rigidbody.transform.position.y > 6) && !Gameover && !Paused && StartGame) {
			if (!muted)
				jumpingAudio.Play ();
			rigidbody.AddForce(0, 15, 0, ForceMode.Impulse);
		}
	}

	static public void switchCams(){
		if (StartGame) {
			if (camera1stPerson.enabled) {
				camera1stPerson.enabled = false;
				camera3rdPerson.enabled = true;
			} else {
				camera1stPerson.enabled = true;
				camera3rdPerson.enabled = false;
			}
		}
	}

	static public void pause(){
		if (!Gameover && StartGame) {
			if (Paused) {
				Paused = false;
				if (!muted) {
					AudioSource gametrack = mainTracks.GetComponents<AudioSource> () [0];
					gametrack.loop = true;
					AudioSource pausetrack = mainTracks.GetComponents<AudioSource> () [1];
					pausetrack.Stop ();
					gametrack.Play ();
				}
				gamecontrolscreen.SetActive(true);
				pausescreen.SetActive(false);
			} else {
				Paused = true;
				if (!muted) {
					AudioSource gametrack = mainTracks.GetComponents<AudioSource> () [0];
					AudioSource pausetrack = mainTracks.GetComponents<AudioSource> () [1];
					pausetrack.loop = true;
					gametrack.Stop ();
					pausetrack.Play ();
				}
				gamecontrolscreen.SetActive(false);
				pausescreen.SetActive(true);
			}
		}

	}

}
