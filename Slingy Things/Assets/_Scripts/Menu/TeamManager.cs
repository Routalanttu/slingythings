using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class TeamManager : MonoBehaviour {

	//SLUG CLASS SPRITES
	public Sprite _class1; 
	public Sprite _class2;
	public Sprite _class3; 

	//SLUG MENU IMAGES
	public Image _classImage1; 
	public Image _classImage2;
	public Image _classImage3;
	public Image _classImage4; 
	public Image _classImage5; 
	public Image _classImage6; 

	private int _teamSelected = 1; 

	private bool _firstTimeLaunched; 

	//TEAM INPUT AND NAME 
	public InputField _teamInput; 
	private string _teamName; 

	//NAME INPUT FIELDS FOR SLUGS
	public InputField _slug1Input; 
	public InputField _slug2Input; 
	public InputField _slug3Input; 
	public InputField _slug4Input; 
	public InputField _slug5Input; 
	public InputField _slug6Input; 

	//SLUG NAMES VISIBLE 
	private string _slug1Name; 
	private string _slug2Name; 
	private string _slug3Name; 
	private string _slug4Name; 
	private string _slug5Name; 
	private string _slug6Name; 

	//SLUG CLASSES
	private string _slug1Class;
	private string _slug2Class;
	private string _slug3Class;
	private string _slug4Class;
	private string _slug5Class;
	private string _slug6Class;


	//TEAMS AND ANIMALS MENU WHEN GOING TO GAME (CHOOSING TEAMS) 
	public Text _team1Text;
	public Text _team2Text; 
	public Text _team3Text; 
	public Text _team4Text; 


	// Use this for initialization
	void Start () {

		//CHECK IF FIRST TIME LAUNCHED AND SET DEFAULTS
		if (!PlayerPrefs.HasKey("firstTimeLaunched")) {
			PlayerPrefs.SetInt ("firstTimeLaunched", 1); 
			SetDefaultNamesAndClasses (); 
		}
			
		UpdateTeamMenu (); 

		SetNamesAndClasses (); 

	}
		
	private void UpdateTeamMenu(){

		_team1Text.text = PlayerPrefs.GetString("team1Name"); 
		_team2Text.text = PlayerPrefs.GetString("team2Name"); 
		_team3Text.text = PlayerPrefs.GetString("team3Name"); 
		_team4Text.text = PlayerPrefs.GetString("team4Name"); 

	}

	private void SetNamesAndClasses(){

		//GET TEAM AND SLUG NAMES BY STORED TEAM NUMBER
		switch (_teamSelected) {
		case 1: 
			_teamName = PlayerPrefs.GetString ("team1Name"); 

			_slug1Name = PlayerPrefs.GetString ("t1s1Name");
			_slug2Name = PlayerPrefs.GetString ("t1s2Name"); 
			_slug3Name = PlayerPrefs.GetString ("t1s3Name"); 
			_slug4Name = PlayerPrefs.GetString ("t1s4Name"); 
			_slug5Name = PlayerPrefs.GetString ("t1s5Name"); 
			_slug6Name = PlayerPrefs.GetString ("t1s6Name"); 

			_slug1Class = PlayerPrefs.GetString ("t1s1Class"); 
			_slug2Class = PlayerPrefs.GetString ("t1s2Class"); 
			_slug3Class = PlayerPrefs.GetString ("t1s2Class"); 
			_slug4Class = PlayerPrefs.GetString ("t1s4Class"); 
			_slug5Class = PlayerPrefs.GetString ("t1s5Class"); 
			_slug6Class = PlayerPrefs.GetString ("t1s6Class"); 

			break; 
		case 2:
			_teamName = PlayerPrefs.GetString ("team2Name"); 

			_slug1Name = PlayerPrefs.GetString ("t2s1Name");
			_slug2Name = PlayerPrefs.GetString ("t2s2Name"); 
			_slug3Name = PlayerPrefs.GetString ("t2s3Name"); 
			_slug4Name = PlayerPrefs.GetString ("t2s4Name"); 
			_slug5Name = PlayerPrefs.GetString ("t2s5Name"); 
			_slug6Name = PlayerPrefs.GetString ("t2s6Name"); 

			_slug1Class = PlayerPrefs.GetString ("t2s1Class"); 
			_slug2Class = PlayerPrefs.GetString ("t2s2Class"); 
			_slug3Class = PlayerPrefs.GetString ("t2s2Class"); 
			_slug4Class = PlayerPrefs.GetString ("t2s4Class"); 
			_slug5Class = PlayerPrefs.GetString ("t2s5Class"); 
			_slug6Class = PlayerPrefs.GetString ("t2s6Class"); 

			break;

		case 3:
			_teamName = PlayerPrefs.GetString ("team3Name"); 

			_slug1Name = PlayerPrefs.GetString ("t3s1Name");
			_slug2Name = PlayerPrefs.GetString ("t3s2Name"); 
			_slug3Name = PlayerPrefs.GetString ("t3s3Name"); 
			_slug4Name = PlayerPrefs.GetString ("t3s4Name"); 
			_slug5Name = PlayerPrefs.GetString ("t3s5Name"); 
			_slug6Name = PlayerPrefs.GetString ("t3s6Name"); 

			_slug1Class = PlayerPrefs.GetString ("t3s1Class"); 
			_slug2Class = PlayerPrefs.GetString ("t3s2Class"); 
			_slug3Class = PlayerPrefs.GetString ("t3s2Class"); 
			_slug4Class = PlayerPrefs.GetString ("t3s4Class"); 
			_slug5Class = PlayerPrefs.GetString ("t3s5Class"); 
			_slug6Class = PlayerPrefs.GetString ("t3s6Class"); 

			break;

		case 4:
			_teamName = PlayerPrefs.GetString ("team4Name"); 

			_slug1Name = PlayerPrefs.GetString ("t4s1Name");
			_slug2Name = PlayerPrefs.GetString ("t4s2Name"); 
			_slug3Name = PlayerPrefs.GetString ("t4s3Name"); 
			_slug4Name = PlayerPrefs.GetString ("t4s4Name"); 
			_slug5Name = PlayerPrefs.GetString ("t4s5Name"); 
			_slug6Name = PlayerPrefs.GetString ("t4s6Name"); 

			_slug1Class = PlayerPrefs.GetString ("t4s1Class"); 
			_slug2Class = PlayerPrefs.GetString ("t4s2Class"); 
			_slug3Class = PlayerPrefs.GetString ("t4s2Class"); 
			_slug4Class = PlayerPrefs.GetString ("t4s4Class"); 
			_slug5Class = PlayerPrefs.GetString ("t4s5Class"); 
			_slug6Class = PlayerPrefs.GetString ("t4s6Class"); 

			break;

		}

		//SET TEXT FIELDS ACCORDINGLY 
		_teamInput.text = _teamName; 

		_slug1Input.text = _slug1Name; 
		_slug2Input.text = _slug2Name; 
		_slug3Input.text = _slug3Name; 
		_slug4Input.text = _slug4Name; 
		_slug5Input.text = _slug5Name; 
		_slug6Input.text = _slug6Name; 


		//SET SLUG CLASS IMAGES
		if (_slug1Class == "Slug") {
			_classImage1.sprite = _class1; 
		} else if (_slug1Class == "Snail") {
			_classImage1.sprite = _class2; 
		} else if (_slug1Class == "Octopus") {
			_classImage1.sprite = _class3; 
		}

		if (_slug2Class == "Slug") {
			_classImage2.sprite = _class1; 
		} else if (_slug2Class == "Snail") {
			_classImage2.sprite = _class2; 
		} else if (_slug2Class == "Octopus") {
			_classImage2.sprite = _class3; 
		}

		if (_slug3Class == "Slug") {
			_classImage3.sprite = _class1; 
		} else if (_slug3Class == "Snail") {
			_classImage3.sprite = _class2; 
		} else if (_slug3Class == "Octopus") {
			_classImage3.sprite = _class3; 
		}

		if (_slug4Class == "Slug") {
			_classImage4.sprite = _class1; 
		} else if (_slug4Class == "Snail") {
			_classImage4.sprite = _class2; 
		} else if (_slug4Class == "Octopus") {
			_classImage4.sprite = _class3; 
		}

		if (_slug5Class == "Slug") {
			_classImage5.sprite = _class1; 
		} else if (_slug5Class == "Snail") {
			_classImage5.sprite = _class2; 
		} else if (_slug4Class == "Octopus") {
			_classImage5.sprite = _class3; 
		}

		if (_slug6Class == "Slug") {
			_classImage6.sprite = _class1; 
		} else if (_slug6Class == "Snail") {
			_classImage6.sprite = _class2; 
		} else if (_slug6Class == "Octopus") {
			_classImage6.sprite = _class3; 
		}
			
	}

	public void NextTeam(){

		_teamSelected++; 

		if (_teamSelected > 4) {
			_teamSelected = 1; 
		}

		SetNamesAndClasses (); 

	}

	public void PreviousTeam(){

		_teamSelected--; 

		if (_teamSelected < 1) {
			_teamSelected = 4; 
		}

		SetNamesAndClasses (); 

	}

	public void SetTeamName(){
		_teamName = _teamInput.text; 
		PlayerPrefs.SetString ("team" + _teamSelected + "Name", _teamName); 
		PlayerPrefs.Save (); 
		UpdateTeamMenu (); 
	}

	public void SetSlugName(int slugNumber){

		switch (slugNumber) {
		case 1:
			_slug1Name = _slug1Input.text; 
			PlayerPrefs.SetString ("t" + _teamSelected + "s1Name", _slug1Name); 
			break;

		case 2:
			_slug2Name = _slug2Input.text; 
			PlayerPrefs.SetString ("t" + _teamSelected + "s2Name", _slug2Name); 
			break;

		case 3:
			_slug3Name = _slug3Input.text; 
			PlayerPrefs.SetString ("t" + _teamSelected + "s3Name", _slug3Name); 
			break;

		case 4:
			_slug3Name = _slug4Input.text; 
			PlayerPrefs.SetString ("t" + _teamSelected + "s4Name", _slug4Name); 
			break;

		case 5:
			_slug3Name = _slug5Input.text; 
			PlayerPrefs.SetString ("t" + _teamSelected + "s5Name", _slug5Name); 
			break; 

		case 6:
			_slug3Name = _slug6Input.text; 
			PlayerPrefs.SetString ("t" + _teamSelected + "s6Name", _slug6Name); 
			break; 

		default: 
			break; 

		}

		PlayerPrefs.Save (); 
	}

	public void ChangeClass(int slugNumber){

		switch (slugNumber) {
		case 1:
			

			break;

		case 2:
			
			break;

		case 3:
			
			break;

		case 4:

			break;

		case 5:

			break; 

		case 6:

			break; 

		default: 
			break; 

		}

	}
		

	private void SetDefaultNamesAndClasses(){

		PlayerPrefs.SetString ("team1Name", "Team1"); 
		PlayerPrefs.SetString ("team2Name", "Team2"); 
		PlayerPrefs.SetString ("team3Name", "Team3"); 
		PlayerPrefs.SetString ("team4Name", "Team4"); 

		for(int i = 1; i < 5; i++){

			for (int j = 1; i < 6; i++) {

				switch (j) {

				case 1:
					PlayerPrefs.SetString ("t" + i + "s" + j + "Class", "Slug"); 
					break;
				case 2:
					PlayerPrefs.SetString ("t" + i + "s" + j + "Class", "Octopus"); 
					break;
				case 3:
					PlayerPrefs.SetString ("t" + i + "s" + j + "Class", "Snail"); 
					break;
				case 4:
					PlayerPrefs.SetString ("t" + i + "s" + j + "Class", "Slug"); 
					break; 
				case 5: 
					PlayerPrefs.SetString ("t" + i + "s" + j + "Class", "Octopus"); 
					break;
				case 6:
					PlayerPrefs.SetString ("t" + i + "s" + j + "Class", "Snail"); 
					break; 

				}
			}
		}
			

	}
}
