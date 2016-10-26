using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace SlingySlugs {
	public class GameManager : MonoBehaviour {

		public int activePlayers = 2; 
		public CameraController _cameraController; 

		//Gameplay states
		enum State
		{
			_stateWaitForTurn, 
			_stateAim, 
			_stateInAir, 
			_stateExplode, 
			_stateCheckDamages
		};

		//TEAM VALUES 
		public List<GameObject> _team1Slugs; 
		public List<GameObject> _team2Slugs; 

		private int _team1SlugAmount; 
		private int _team2SlugAmount; 
		private int _team1Health; 
		private int _team2Health; 

		private static GameManager _instance; 
		private static bool _isQuitting = false;
		private int currentPlayer = 0; 

		private bool _gameStarted;
        public bool CharacterTouched { set; get; } //if player activates a character 

		public static GameManager Instance {
			get {
				if (_instance == null && !_isQuitting) {
					_instance = new GameManager ();  //This is probably not necessary as we set the GameManager in Awake
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

				if (go.GetComponent<CharacterInfo> ().GetTeam() == 1) {
					_team1Slugs.Add (go); 
				} else {
					_team2Slugs.Add (go); 
				}

			}

			//Get team healths
			foreach (GameObject go in _team1Slugs) {
				_team1Health += go.GetComponent<CharacterInfo> ().Health; 
			}

			foreach (GameObject go in _team2Slugs) {
				_team2Health += go.GetComponent<CharacterInfo> ().Health; 
			}
				
			GUIManager.UpdateHealth (_team1Health, _team2Health); 

			_team1SlugAmount = _team1Slugs.Count; 
			_team2SlugAmount = _team2Slugs.Count; 

			// PLACEHOLDERPURKKA PLS REMOVE
			_guiManager.HideMessage ();
			NextPlayerMove ();
		
		}

		void Update () {

			UserInput (); 
			CheckGameState (); 
		
		}

		void StartGame(){
			GUIManager.HideMessage (); 
		}

		void UserInput(){

			if (Input.GetKeyDown (KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) {
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
				GUIManager.Paused (true); 
			} else {
				Time.timeScale = 1; 
				_paused = false; 
				GUIManager.HideMessage (); 
				GUIManager.Paused (false); 
			}
		}

		void CheckGameState(){

		}

		public void SetCameraTarget(Transform target){
			_cameraController.SetCameraTarget (target); 
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
			

		public void DecreaseTeamHealth(int teamNumber, int decreaseAmount){

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
					slug.GetComponent<CharacterInfo> ().IsActive = true; 
				}

				foreach (var slug in _team2Slugs) {
					slug.GetComponent<CharacterInfo> ().IsActive = false; 
				}

			} else {

				foreach (var slug in _team2Slugs) {
					slug.GetComponent<CharacterInfo> ().IsActive = true; 
				}

				foreach (var slug in _team1Slugs) {
					slug.GetComponent<CharacterInfo> ().IsActive = false; 
				}

			}
		}

		public void RestartLevel(){
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

		public void QuitGame(){

			PlayerPrefs.Save (); 
			Application.Quit (); 
		}

		public void ResumeGame(){
			Pause (); 
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

}