using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class GUIManager : MonoBehaviour {

	public Text _message; 
	public Text _team1HealthText; 
	public Text _team2HealthText; 
	public Slider _team1HealthSlider; 
	public Slider _team2HealthSlider; 
	public GameObject _pauseMenu; 

	//PAUSE MENU
	public Slider _soundLevel;
	public Slider _musicLevel; 

	void Awake(){

		if (_message == null) {
			Debug.LogError ("GUIManager - _message missing"); 
		}

		if (_team1HealthText == null) {
			Debug.LogError ("GUIManager - _blueTeamMessage missing"); 
		}

		if (_team2HealthText == null) {
			Debug.LogError ("GUIManager - _redTeamMessage missing"); 
		}

		if (_pauseMenu == null) {
			Debug.LogError ("pausemenu missing "); 
		}

		if (_soundLevel == null) {
			Debug.LogError ("_soundlevel missing "); 
		}

		if (_musicLevel == null) {
			Debug.LogError ("_musicLevel missing "); 
		}

		SetMenuValues (); 
		_pauseMenu.SetActive(false); 


	}

	// Use this for initialization
	void Start () {
	
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
			ShowMessage ("Team 1 wins!!"); 
		} else if (winningTeamNumber == 2) {
			ShowMessage ("Team 2 wins!!"); 
		}

	}

	public void UpdateHealth(int team1health, int team2health){

		_team1HealthText.text = "Team 1 health: " + team1health; 
		_team2HealthText.text = "Team 2 health: " + team2health; 

		_team1HealthSlider.value = (float)team1health / 1000; 
		_team2HealthSlider.value = (float)team2health / 1000; 
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

		if(PlayerPrefs.HasKey("soundvolume")){
			_soundLevel.value = PlayerPrefs.GetFloat ("soundvolume"); 
		}

		if (PlayerPrefs.HasKey ("musicvolume")) {
			_musicLevel.value = PlayerPrefs.GetFloat ("musicvolume"); 
		}

	}
		
}
