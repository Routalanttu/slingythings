using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class TeamManager : MonoBehaviour {

	public InputField _team1Input; 

	//TEAMS AND ANIMALS MENU
	public Text _team1Text;
	public Text _team2Text; 
	public Text _team3Text; 
	public Text _team4Text; 

	private string _team1Name; 
	private string _team2Name; 
	private string _team3Name; 
	private string _team4Name; 


	// Use this for initialization
	void Start () {

		if (PlayerPrefs.HasKey ("team1Name")) {
			_team1Name = PlayerPrefs.GetString ("team1Name"); 
		} else {
			Debug.Log ("meneekö tänne"); 
			_team1Name = "Team 1"; 
		}

		_team1Input.text = _team1Name; 


		//MAIN MENU 
		if (_team1Name != null) {
			_team1Text.text = _team1Name; 
		}

		if (_team2Name != null) {
			_team2Text.text = _team2Name; 
		}

		if (_team3Name != null) {
			_team3Text.text = _team3Name; 
		}

		if (_team4Name != null) {
			_team4Text.text = _team4Name; 
		}

	
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setTeamName(){

		_team1Name = _team1Input.text; 
		PlayerPrefs.SetString ("team1Name", _team1Name); 
		PlayerPrefs.Save (); 
	}
}
