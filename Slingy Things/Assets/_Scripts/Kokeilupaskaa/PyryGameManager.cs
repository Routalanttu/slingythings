using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PyryGameManager : MonoBehaviour {

	public int activePlayers = 2; 
	public camFollow _camFollow; 

	//TEAM VALUES 
	public List<GameObject> _team1Slugs; 
	public List<GameObject> _team2Slugs; 

	private int _team1SlugAmount; 
	private int _team2SlugAmount; 
	private int _team1Health; 
	private int _team2Health; 

	private static PyryGameManager _instance; 
	private static bool _isQuitting = false;
	private int currentPlayer = 0; 

	private bool _gameStarted; 

	public static PyryGameManager Instance {
		get {
			if (_instance == null && !_isQuitting) {
				_instance = new PyryGameManager ();  //This is probably not necessary as we set the GameManager in Awake
			}
			return _instance; 
		}
	}

	private GUIManager _guiManager;

	public GUIManager GUIManager
	{
		get
		{
			if(_guiManager == null)
			{
				_guiManager = FindObjectOfType<GUIManager>(); 
			}

			return _guiManager; 
		}
	}

	private bool _paused = false; 

	void Awake() {

		if (_instance == null) {
			_instance = this; 
		} else if (_instance != this) {
			Destroy (this); 
		}

	}

	void Start () {

		_team1Slugs = new List<GameObject>(); 
		_team2Slugs = new List<GameObject>(); 

		GameObject[] gos = GameObject.FindGameObjectsWithTag("Slug");

		foreach (GameObject go in gos) {

			if ((int) go.GetComponent<Slug> ().teamSelect == 1) {
				_team1Slugs.Add (go); 
			} else {
				_team2Slugs.Add (go); 
			}

		}

		//Get team healths
		foreach (GameObject go in _team1Slugs) {
			_team1Health += go.GetComponent<SlugHealth> ()._slugHealth; 
		}

		foreach (GameObject go in _team2Slugs) {
			_team2Health += go.GetComponent<SlugHealth> ()._slugHealth; 
		}

		GUIManager.UpdateHealth (_team1Health, _team2Health); 

		_team1SlugAmount = _team1Slugs.Count; 
		_team2SlugAmount = _team2Slugs.Count; 

	}

	void Update () {

		UserInput (); 
		CheckGameState (); 

	}

	void StartGame(){
		GUIManager.HideMessage (); 
	}

	void UserInput(){

		if (Input.GetKeyDown (KeyCode.P)) {
			Pause (); 
		} 

		if (Input.GetKeyDown (KeyCode.N)) {
			if (!_gameStarted) {
				StartGame (); 
			}
			NextPlayerMove (); 
		}
	}

	public void Pause(){

		if (!_paused) {
			Time.timeScale = 0; 
			_paused = true; 
			GUIManager.Paused (); 
		} else {
			Time.timeScale = 1; 
			_paused = false; 
			GUIManager.HideMessage (); 
		}
	}

	void CheckGameState(){

	}

	public void SetCameraTarget(Transform target){
		_camFollow.SetCameraTarget (target); 
	}

	public void KillSlug(int teamNumber, GameObject go){

		if (teamNumber== 1) {
			_team1SlugAmount--; 
			_team1Slugs.Remove (go); 
		}

		if (teamNumber == 2) {
			_team2SlugAmount--; 
			_team2Slugs.Remove (go); 
		}

		if (_team1SlugAmount <= 0) {
			GameOver (2);
		} else if (_team2SlugAmount <= 0) {
			GameOver (1); 
		}

	}


	public void DecreaseHealth(int teamNumber, int decreaseAmount){

		if (teamNumber == 1) {
			_team1Health -= decreaseAmount; 
		}else if( teamNumber == 2){
			_team2Health -= decreaseAmount; 
		}

		GUIManager.UpdateHealth(_team1Health, _team2Health); 

	}


	public void NextPlayerMove(){

		if (currentPlayer == 1) {
			currentPlayer = 2; 
		} else {
			currentPlayer = 1; 
		}

		ActivateTeam(); 
	}

	public void ActivateTeam(){

		if (currentPlayer == 1) {

			foreach (var slug in _team1Slugs) {
				slug.GetComponent<Slug> ().IsActive = true; 
			}

			foreach (var slug in _team2Slugs) {
				slug.GetComponent<Slug> ().IsActive = false; 
			}

		} else {

			foreach (var slug in _team2Slugs) {
				slug.GetComponent<Slug> ().IsActive = true; 
			}

			foreach (var slug in _team1Slugs) {
				slug.GetComponent<Slug> ().IsActive = false; 
			}

		}
	}


	private void OnApplicationQuit(){
		_isQuitting = true; 
	}

	public void GameOver(int winningTeamNumber){
		Pause (); 
		GUIManager.GameOver (winningTeamNumber);
		// Didn't want the TeamsAndAnimals scene loading immediately after the game ends so I decided to use a coroutine
		StartCoroutine(SceneLoad());
	}

	IEnumerator SceneLoad () {
		yield return new WaitForSecondsRealtime(3.0f);
		SceneManager.LoadScene("TeamsAndAnimals");
	}





}
