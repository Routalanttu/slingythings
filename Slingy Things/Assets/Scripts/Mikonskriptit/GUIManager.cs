using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class GUIManager : MonoBehaviour {

	public Text _message; 
	public Text _team1HealthText; 
	public Text _team2HealthText; 

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
	}

	public void Paused(){
		ShowMessage ("Paused"); 
	}
}
