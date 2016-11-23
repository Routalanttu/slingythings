using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

namespace SlingySlugs{

	public class GUIManager : MonoBehaviour {

		public GameObject _team1HealthObject;
		public GameObject _team2HealthObject;
		public GameObject _team3HealthObject;
		public GameObject _team4HealthObject;

		public Text _message; 
		public Text _team1NameText; 
		public Text _team2NameText; 
		public Text _team3NameText; 
		public Text _team4NameText; 

		public Slider _team1HealthSlider; 
		public Slider _team2HealthSlider; 
		public Slider _team3HealthSlider; 
		public Slider _team4HealthSlider; 

		public Image _team1HealthFill; 
		public Image _team2HealthFill; 
		public Image _team3HealthFill; 
		public Image _team4HealthFill; 

		public GameObject _pauseMenu; 
		public Text _turnText; 

		void Awake(){

			if (_message == null) {
				Debug.LogError ("GUIManager - _message missing"); 
			}

			if (_team1NameText == null) {
				Debug.LogError ("GUIManager - _Team1name missing"); 
			}

			if (_team2NameText == null) {
				Debug.LogError ("GUIManager - _team2name missing"); 
			}

			if (_team3NameText == null) {
				Debug.LogError ("GUIManager - _team3name missing"); 
			}

			if (_team4NameText == null) {
				Debug.LogError ("GUIManager - _team4name missing"); 
			}

			if (_pauseMenu == null) {
				Debug.LogError ("pausemenu missing "); 
			}
				

			SetMenuValues (); 
			_pauseMenu.SetActive(false); 


		}

		// Use this for initialization
		void Start () {

			_team1NameText.text = GameSessionController._instance._teams [0]._teamName; 
			_team2NameText.text = GameSessionController._instance._teams [1]._teamName; 
			_team3NameText.text = GameSessionController._instance._teams [2]._teamName; 
			_team4NameText.text = GameSessionController._instance._teams [3]._teamName; 

			_team1NameText.color = GameSessionController._instance._teams [0]._teamUnityColor;
			_team2NameText.color = GameSessionController._instance._teams [1]._teamUnityColor;
			_team3NameText.color = GameSessionController._instance._teams [2]._teamUnityColor;
			_team4NameText.color = GameSessionController._instance._teams [3]._teamUnityColor;

			_team1HealthFill.color = GameSessionController._instance._teams [0]._teamUnityColor;
			_team2HealthFill.color = GameSessionController._instance._teams [1]._teamUnityColor;
			_team3HealthFill.color = GameSessionController._instance._teams [2]._teamUnityColor;
			_team4HealthFill.color = GameSessionController._instance._teams [3]._teamUnityColor;

			if (GameSessionController._instance._numberOfTeams == 2) {

				_team3HealthObject.SetActive (false); 
				_team4HealthObject.SetActive (false); 
				 
			}else if (GameSessionController._instance._numberOfTeams == 3) {
				_team4HealthObject.SetActive (false); 
			}


		}
		
		// Update is called once per frame
		void Update () {


		
		}

		public void ShowMessage(string message){
			_message.text = message; 
			_message.gameObject.SetActive (true); 
		}

		public void HideMessage(){
			_message.gameObject.SetActive (false); 
		}

		public void GameOver(int winningTeamNumber){

			if (winningTeamNumber == 1) {
				// Tiimin nimi tän tilalle ja teksti sen väriseksi
				ShowMessage ("Blue team wins!!"); 
			} else if (winningTeamNumber == 2) {
				// Tiimin nimi tän tilalle ja teksti sen väriseksi
				ShowMessage ("Red team wins!!"); 
			}

		}

		public void UpdateHealth(int team1health, int team2health, int team3health, int team4health){

			_team1HealthSlider.value = (float)team1health / 600f; 
			_team2HealthSlider.value = (float)team2health / 600f; 
			_team3HealthSlider.value = (float)team3health / 600f; 
			_team4HealthSlider.value = (float)team4health / 600f; 

		}

		public void UpdateTurnText(int activeTeam){
			_turnText.text = "It's team " + GameSessionController._instance._teams [activeTeam - 1]._teamName + "'s turn"; 
		}

		public void Paused(bool paused){
			//ShowMessage ("Paused"); 
			if (paused) {
				_pauseMenu.SetActive (true); 
			} else {
				_pauseMenu.SetActive (false); 
			}

		}

		void SetMenuValues(){

		}
			
	}
}
