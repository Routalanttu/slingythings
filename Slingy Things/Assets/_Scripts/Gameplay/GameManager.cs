using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace SlingySlugs
{
	public class GameManager : MonoBehaviour
	{

		public CameraController _cameraController;

		//Gameplay states
		enum State
		{
			_stateWaitForTurn,
			_stateAim,
			_stateInAir,
			_stateExplode,
			_stateCheckDamages}

		;

		private int numberOfTeams;

		//TEAM VALUES
		public List<GameObject> _team1Slugs;
		public List<GameObject> _team2Slugs;
		public List<GameObject> _team3Slugs;
		public List<GameObject> _team4Slugs;

		public List<Rigidbody2D> _allSlugRigidbodies;


		private int _team1SlugAmount;
		private int _team2SlugAmount;
		private int _team3SlugAmount;
		private int _team4SlugAmount;

		private int _team1Health;
		private int _team2Health;
		private int _team3Health;
		private int _team4Health;

		private static GameManager _instance;
		private static bool _isQuitting = false;
		private int currentTeam = 0;

		private bool _gameStarted;

		private bool _slugSlunged; 
		private bool _allSlugsStill; 
		private float _stillTimer; 
		private bool _drowned; 

		public bool CharacterTouched { set; get; }
		//if player activates a character

		public static GameManager Instance {
			get {
				if (_instance == null && !_isQuitting) {
					_instance = new GameManager ();  //This is probably not necessary as we set the GameManager in Awake
				}
				return _instance; 
			}
		}

		private GUIManager _guiManager;

		public GUIManager GUIManager {
			get {
				if (_guiManager == null) {
					_guiManager = FindObjectOfType<GUIManager> (); 
				}

				return _guiManager; 
			}
		}

		private bool _paused = false;

		void Awake ()
		{

			if (_instance == null) {
				_instance = this; 
			} else if (_instance != this) {
				Destroy (this); 
			}
				
		}

		void Start ()
		{

			numberOfTeams = GameSessionController._instance._numberOfTeams; 

			_team1Slugs = new List<GameObject> (); 
			_team2Slugs = new List<GameObject> (); 
			_team3Slugs = new List<GameObject> (); 
			_team4Slugs = new List<GameObject> (); 

			GameObject[] gos = GameObject.FindGameObjectsWithTag ("Slug");

			foreach (GameObject go in gos) {

				Rigidbody2D rb2d = go.GetComponent<Rigidbody2D> (); 
				_allSlugRigidbodies.Add (rb2d); 

				if (go.GetComponent<CharacterInfo> ().GetTeam () == 1) {
					_team1Slugs.Add (go); 
				} else if (go.GetComponent<CharacterInfo> ().GetTeam () == 2) {
					_team2Slugs.Add (go); 
				} else if (go.GetComponent<CharacterInfo> ().GetTeam () == 3) {
					_team3Slugs.Add (go); 
				} else {
					_team4Slugs.Add (go); 
				}

			}

			//Get team healths
			foreach (GameObject go in _team1Slugs) {
				_team1Health += go.GetComponent<CharacterInfo> ().Health; 
			}

			foreach (GameObject go in _team2Slugs) {
				_team2Health += go.GetComponent<CharacterInfo> ().Health; 
			}

			foreach (GameObject go in _team3Slugs) {
				_team3Health += go.GetComponent<CharacterInfo> ().Health; 
			}

			foreach (GameObject go in _team3Slugs) {
				_team4Health += go.GetComponent<CharacterInfo> ().Health; 
			}
				
			GUIManager.UpdateHealth (_team1Health, _team2Health, _team3Health, _team4Health); 

			_team1SlugAmount = _team1Slugs.Count; 
			_team2SlugAmount = _team2Slugs.Count; 
			_team3SlugAmount = _team3Slugs.Count; 
			_team4SlugAmount = _team4Slugs.Count; 

			// PLACEHOLDERPURKKA PLS REMOVE
			_guiManager.HideMessage ();

			NextPlayerMove ();
		
		}

		void Update ()
		{

			//UserInput (); 
			CheckGameState (); 


		}

		void StartGame ()
		{
			GUIManager.HideMessage (); 
		}

		void UserInput ()
		{

			if (Input.GetKeyDown (KeyCode.P) || Input.GetKeyDown (KeyCode.Escape)) {
				Pause (); 
			} 

			if (Input.GetKeyDown (KeyCode.N)) {
				if (!_gameStarted) {
					StartGame (); 
				}
				NextPlayerMove (); 
			}
		}

		public void Pause ()
		{

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

		void CheckGameState ()
		{
			if (_slugSlunged || _drowned) {
			 
				int numberOfMovingSlugs = 0;  

				foreach (Rigidbody2D rb in _allSlugRigidbodies) {
					
					if (Mathf.Abs (rb.velocity.x) > 0.4f || Mathf.Abs (rb.velocity.y) > 0.4f) {
						numberOfMovingSlugs++; 
					}
				}

				if (numberOfMovingSlugs == 0 && !_allSlugsStill) {
					_allSlugsStill = true; 
					_stillTimer = 1;
				} else if (numberOfMovingSlugs != 0) {
					_allSlugsStill = false; 
				}

			}

			//WHEN ALL SLUGS ARE STILL, START TIMER 
			if (_allSlugsStill && (_slugSlunged || _drowned)) {
				_stillTimer -= Time.deltaTime; 
			}

			if ((_slugSlunged || _drowned) && _allSlugsStill && _stillTimer < 0) {
				_guiManager.StartBloom (); 
				Invoke ("NextPlayerMove", 1); 
				_allSlugsStill = false;
				_slugSlunged = false; 
				_drowned = false; 
				_stillTimer = 1; 

			}
		}

		public void SetCameraTarget (Transform target)
		{
			_cameraController.SetCameraTarget (target); 
		}

		public void KillSlug (int teamNumber, GameObject go)
		{

			_allSlugRigidbodies.Remove (go.GetComponent<Rigidbody2D> ()); 

			if (teamNumber == 1) {
				_team1SlugAmount--; 
				_team1Slugs.Remove (go);
			}

			if (teamNumber == 2) {
				_team2SlugAmount--; 
				_team2Slugs.Remove (go); 
			}

			if (teamNumber == 3) {
				_team3SlugAmount--; 
				_team3Slugs.Remove (go); 
			}

			if (teamNumber == 4) {
				_team4SlugAmount--; 
				_team4Slugs.Remove (go); 
			}

			go.GetComponent<CharacterInfo> ().DecreaseHealth (100);

			Destroy (go);

			//DO SOMETHING HERE
			if (_team2SlugAmount <= 0 && _team3SlugAmount <=0 && _team4SlugAmount <= 0) {
				GameOver (1);
			} else if (_team1SlugAmount <= 0 && _team3SlugAmount <=0 && _team4SlugAmount <= 0) {
				GameOver (2); 
			}else if (_team1SlugAmount <= 0 && _team2SlugAmount <=0 && _team4SlugAmount <= 0) {
				GameOver (3); 
			}else if (_team1SlugAmount <= 0 && _team2SlugAmount <=0 && _team3SlugAmount <= 0) {
				GameOver (4); 
			}

		}


		public void DecreaseTeamHealth (int teamNumber, int decreaseAmount)
		{

			if (teamNumber == 1) {
				_team1Health -= decreaseAmount; 
			} else if (teamNumber == 2) {
				_team2Health -= decreaseAmount; 
			} else if (teamNumber == 3) {
				_team3Health -= decreaseAmount; 
			} else if (teamNumber == 4) {
				_team4Health -= decreaseAmount; 
			}

			GUIManager.UpdateHealth (_team1Health, _team2Health, _team3Health, _team4Health); 

		}

		public void SlugSlunged(){
			//slung slug has exploded
			_slugSlunged = true; 
		}

		public void Drowned(){
			//slung slug has drowned
			_drowned = true; 
		}


		public void NextPlayerMove ()
		{
			Debug.Log ("NEXTPLAYERMOVE"); 
			if (numberOfTeams == 2) {
				if (currentTeam == 1) {
					currentTeam = 2; 
				} else {
					currentTeam = 1; 
				}
			}

			if (numberOfTeams == 3) {
				if (currentTeam == 1) {
					currentTeam = 2; 
				} else if (currentTeam == 2) {
					currentTeam = 3; 
				} else {
					currentTeam = 1; 
				}
			}

			if (numberOfTeams == 4) {
				if (currentTeam == 1) {
					currentTeam = 2; 
				} else if (currentTeam == 2) {
					currentTeam = 3; 
				} else if (currentTeam == 3) {
					currentTeam = 4; 
				} else {
					currentTeam = 1; 
				}
			}

			_guiManager.UpdateTurnText (currentTeam); 
			_guiManager.ChangeActiveTeam (currentTeam); 
			ActivateTeam(); 
		}

		public void DeactiveCircleColliders(){

			foreach (var slug in _team1Slugs) {
				slug.GetComponent<CircleCollider2D> ().enabled = false;
			}

			foreach (var slug in _team2Slugs) {
				slug.GetComponent<CircleCollider2D> ().enabled = false;
			}

			foreach (var slug in _team3Slugs) {
				slug.GetComponent<CircleCollider2D> ().enabled = false;
			}

			foreach (var slug in _team4Slugs) {
				slug.GetComponent<CircleCollider2D> ().enabled = false;
			}

		}

		public void ActivateTeam ()
		{

			if (currentTeam == 1) {

				foreach (var slug in _team1Slugs) {
					slug.GetComponent<CharacterInfo> ().IsActive = true; 
					//slug.GetComponent<CharacterInfo> ().ShowName (true); 
					slug.GetComponent<CircleCollider2D> ().enabled = true;
				}

				foreach (var slug in _team2Slugs) {
					slug.GetComponent<CharacterInfo> ().IsActive = false; 
					//slug.GetComponent<CharacterInfo> ().ShowName (false); 
					slug.GetComponent<CircleCollider2D> ().enabled = false;
				}

				foreach (var slug in _team3Slugs) {
					slug.GetComponent<CharacterInfo> ().IsActive = false; 
					//slug.GetComponent<CharacterInfo> ().ShowName (false);
					slug.GetComponent<CircleCollider2D> ().enabled = false;
				}

				foreach (var slug in _team4Slugs) {
					slug.GetComponent<CharacterInfo> ().IsActive = false; 
					//slug.GetComponent<CharacterInfo> ().ShowName (false);
					slug.GetComponent<CircleCollider2D> ().enabled = false;
				}

			} else if (currentTeam == 2) {

				foreach (var slug in _team2Slugs) {
					slug.GetComponent<CharacterInfo> ().IsActive = true;
					//slug.GetComponent<CharacterInfo> ().ShowName (true);
					slug.GetComponent<CircleCollider2D> ().enabled = true;
				}

				foreach (var slug in _team1Slugs) {
					slug.GetComponent<CharacterInfo> ().IsActive = false; 
					//slug.GetComponent<CharacterInfo> ().ShowName (false);
					slug.GetComponent<CircleCollider2D> ().enabled = false;
				}

				foreach (var slug in _team3Slugs) {
					slug.GetComponent<CharacterInfo> ().IsActive = false; 
					//slug.GetComponent<CharacterInfo> ().ShowName (false);
					slug.GetComponent<CircleCollider2D> ().enabled = false;
				}

				foreach (var slug in _team4Slugs) {
					slug.GetComponent<CharacterInfo> ().IsActive = false; 
					//slug.GetComponent<CharacterInfo> ().ShowName (false);
					slug.GetComponent<CircleCollider2D> ().enabled = false;
				}

			} else if (currentTeam == 3) {

				foreach (var slug in _team3Slugs) {
					slug.GetComponent<CharacterInfo> ().IsActive = true;
					//slug.GetComponent<CharacterInfo> ().ShowName (true);
					slug.GetComponent<CircleCollider2D> ().enabled = true;
				}

				foreach (var slug in _team1Slugs) {
					slug.GetComponent<CharacterInfo> ().IsActive = false; 
					//slug.GetComponent<CharacterInfo> ().ShowName (false);
					slug.GetComponent<CircleCollider2D> ().enabled = false;
				}

				foreach (var slug in _team2Slugs) {
					slug.GetComponent<CharacterInfo> ().IsActive = false; 
					//slug.GetComponent<CharacterInfo> ().ShowName (false);
					slug.GetComponent<CircleCollider2D> ().enabled = false;
				}

				foreach (var slug in _team4Slugs) {
					slug.GetComponent<CharacterInfo> ().IsActive = false; 
					//slug.GetComponent<CharacterInfo> ().ShowName (false);
					slug.GetComponent<CircleCollider2D> ().enabled = false;
				}

			} else {

				foreach (var slug in _team4Slugs) {
					slug.GetComponent<CharacterInfo> ().IsActive = true;
					//slug.GetComponent<CharacterInfo> ().ShowName (true);
					slug.GetComponent<CircleCollider2D> ().enabled = true;
				}

				foreach (var slug in _team1Slugs) {
					slug.GetComponent<CharacterInfo> ().IsActive = false; 
					//slug.GetComponent<CharacterInfo> ().ShowName (false);
					slug.GetComponent<CircleCollider2D> ().enabled = false;
				}

				foreach (var slug in _team2Slugs) {
					slug.GetComponent<CharacterInfo> ().IsActive = false; 
					//slug.GetComponent<CharacterInfo> ().ShowName (false);
					slug.GetComponent<CircleCollider2D> ().enabled = false;
				}

				foreach (var slug in _team3Slugs) {
					slug.GetComponent<CharacterInfo> ().IsActive = false; 
					//slug.GetComponent<CharacterInfo> ().ShowName (false);
					slug.GetComponent<CircleCollider2D> ().enabled = false;
				}

			}
		}

		public void RestartLevel ()
		{
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}

		public void QuitGame ()
		{

			PlayerPrefs.Save (); 
			Application.Quit (); 
		}

		public void ResumeGame ()
		{
			Pause (); 
		}


		private void OnApplicationQuit ()
		{
			_isQuitting = true; 
		}

		public void GameOver (int winningTeamNumber)
		{
			GUIManager.GameOver (winningTeamNumber);
			// Didn't want the TeamsAndAnimals scene loading immediately after the game ends so I decided to use a coroutine
			StartCoroutine (SceneLoad ());
		}



		IEnumerator SceneLoad ()
		{
			yield return new WaitForSecondsRealtime (3.0f);
			SceneManager.LoadScene ("Menu");
		}

		public void GoToMenu ()
		{
			Time.timeScale = 1; 
			SceneManager.LoadScene ("Menu");
		}

		public int GetCurrentActiveTeam ()
		{
			return currentTeam;
		}

	}

}