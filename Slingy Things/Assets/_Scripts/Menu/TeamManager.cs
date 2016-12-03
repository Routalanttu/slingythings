using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

namespace SlingySlugs{

	public class TeamManager : MonoBehaviour {

		private int _numberOfTeams; 

		ArrayList teamsList = new ArrayList(); 

		//SLOT SPRITES
		public Sprite _emptySlotSprite; 
		public Sprite _redSlotSprite; 
		public Sprite _blueSlotSprite; 
		public Sprite _yellowSlotSprite; 
		public Sprite _greenSlotSprite; 
		public Sprite _violetSlotSprite;
		public Sprite _orangeSlotSprite;

		//SLOT IMAGES
		public Image[] _slotImages;
		public GameObject _nextButtonGO; 

		private bool[] _teamsSelected = new bool[6];

		//SLUG CLASS SPRITES
		public Sprite _slugSprite; 
		public Sprite _snailSprite;
		public Sprite _octopusSprite; 
		public Sprite _siikaSprite; 

		//SLUG MENU IMAGES
		public Image _classImage1; 
		public Image _classImage2;
		public Image _classImage3;
		public Image _classImage4; 
		public Image _classImage5; 
		public Image _classImage6; 

		private int _teamSelected = 1; 

		private bool _firstTimeLaunched; 

		//TEAM BG, INPUT AND NAME 
		public Image _teamNameBGImage; 
		public Sprite[] _teamNameBGSprites; 
		public InputField _teamInput; 
		private string _teamName; 

		public Image[] _slugBGImages; 
		public Sprite[] _slugBGSprites; 

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
		public Text _team5Text; 
		public Text _team6Text; 
		Button _teamsNextButton; 
		Animator _teamsNextButtonAnimator;
		Image _teamsNextButtonImage;
		Color _teamsNextButtonColor;


		// Use this for initialization
		void Start () {

			//CHECK IF FIRST TIME LAUNCHED AND SET DEFAULTS
			if (!PlayerPrefs.HasKey("firstTimeLaunched")) {
				PlayerPrefs.SetInt ("firstTimeLaunched", 1); 
				SetDefaultNamesAndClasses (); 
			}
				
			UpdateTeamMenu (); 

			SetNamesAndClasses (); 


			_teamsNextButton = _nextButtonGO.GetComponent<Button> (); 
			_teamsNextButton.enabled = false; 
			_teamsNextButtonAnimator = _nextButtonGO.GetComponent<Animator> ();
			_teamsNextButtonAnimator.enabled = false; 
			_teamsNextButtonImage = _nextButtonGO.GetComponent<Image> (); 
			_teamsNextButtonColor = _teamsNextButtonImage.color; 

		}
			
		private void UpdateTeamMenu(){

			_team1Text.text = PlayerPrefs.GetString("team1Name"); 
			_team2Text.text = PlayerPrefs.GetString("team2Name"); 
			_team3Text.text = PlayerPrefs.GetString("team3Name"); 
			_team4Text.text = PlayerPrefs.GetString("team4Name"); 
			_team5Text.text = PlayerPrefs.GetString("team5Name"); 
			_team6Text.text = PlayerPrefs.GetString("team6Name"); 


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
			case 5:
				_teamName = PlayerPrefs.GetString ("team5Name"); 

				_slug1Name = PlayerPrefs.GetString ("t5s1Name");
				_slug2Name = PlayerPrefs.GetString ("t5s2Name"); 
				_slug3Name = PlayerPrefs.GetString ("t5s3Name"); 
				_slug4Name = PlayerPrefs.GetString ("t5s4Name"); 
				_slug5Name = PlayerPrefs.GetString ("t5s5Name"); 
				_slug6Name = PlayerPrefs.GetString ("t5s6Name"); 

				_slug1Class = PlayerPrefs.GetString ("t5s1Class"); 
				_slug2Class = PlayerPrefs.GetString ("t5s2Class"); 
				_slug3Class = PlayerPrefs.GetString ("t5s2Class"); 
				_slug4Class = PlayerPrefs.GetString ("t5s4Class"); 
				_slug5Class = PlayerPrefs.GetString ("t5s5Class"); 
				_slug6Class = PlayerPrefs.GetString ("t5s6Class"); 

				break;

			case 6:
				_teamName = PlayerPrefs.GetString ("team6Name"); 

				_slug1Name = PlayerPrefs.GetString ("t6s1Name");
				_slug2Name = PlayerPrefs.GetString ("t6s2Name"); 
				_slug3Name = PlayerPrefs.GetString ("t6s3Name"); 
				_slug4Name = PlayerPrefs.GetString ("t6s4Name"); 
				_slug5Name = PlayerPrefs.GetString ("t6s5Name"); 
				_slug6Name = PlayerPrefs.GetString ("t6s6Name"); 

				_slug1Class = PlayerPrefs.GetString ("t6s1Class"); 
				_slug2Class = PlayerPrefs.GetString ("t6s2Class"); 
				_slug3Class = PlayerPrefs.GetString ("t6s2Class"); 
				_slug4Class = PlayerPrefs.GetString ("t6s4Class"); 
				_slug5Class = PlayerPrefs.GetString ("t6s5Class"); 
				_slug6Class = PlayerPrefs.GetString ("t6s6Class"); 

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
				_classImage1.sprite = _slugSprite; 
			} else if (_slug1Class == "Snail") {
				_classImage1.sprite = _snailSprite; 
			} else if (_slug1Class == "Octopus") {
				_classImage1.sprite = _octopusSprite; 
			} else if (_slug1Class == "Siika") {
				_classImage1.sprite = _siikaSprite; 
			}

			if (_slug2Class == "Slug") {
				_classImage2.sprite = _slugSprite; 
			} else if (_slug2Class == "Snail") {
				_classImage2.sprite = _snailSprite; 
			} else if (_slug2Class == "Octopus") {
				_classImage2.sprite = _octopusSprite; 
			} else if (_slug2Class == "Siika") {
				_classImage2.sprite = _siikaSprite; 
			}

			if (_slug3Class == "Slug") {
				_classImage3.sprite = _slugSprite; 
			} else if (_slug3Class == "Snail") {
				_classImage3.sprite = _snailSprite; 
			} else if (_slug3Class == "Octopus") {
				_classImage3.sprite = _octopusSprite; 
			}else if (_slug3Class == "Siika") {
				_classImage3.sprite = _siikaSprite; 
			}

			if (_slug4Class == "Slug") {
				_classImage4.sprite = _slugSprite; 
			} else if (_slug4Class == "Snail") {
				_classImage4.sprite = _snailSprite; 
			} else if (_slug4Class == "Octopus") {
				_classImage4.sprite = _octopusSprite; 
			}else if (_slug4Class == "Siika") {
				_classImage4.sprite = _siikaSprite; 
			}

			if (_slug5Class == "Slug") {
				_classImage5.sprite = _slugSprite; 
			} else if (_slug5Class == "Snail") {
				_classImage5.sprite = _snailSprite; 
			} else if (_slug4Class == "Octopus") {
				_classImage5.sprite = _octopusSprite; 
			} else if (_slug4Class == "Siika") {
				_classImage5.sprite = _siikaSprite; 
			}

			if (_slug6Class == "Slug") {
				_classImage6.sprite = _slugSprite; 
			} else if (_slug6Class == "Snail") {
				_classImage6.sprite = _snailSprite; 
			} else if (_slug6Class == "Octopus") {
				_classImage6.sprite = _octopusSprite; 
			}else if (_slug6Class == "Siika") {
				_classImage6.sprite = _siikaSprite; 
			}
				
		}

		public void NextTeam(){

			_teamSelected++; 

			if (_teamSelected > 6) {
				_teamSelected = 1; 
			}

			_teamNameBGImage.sprite = _teamNameBGSprites [_teamSelected - 1]; 

			for (int i = 0; i < _slugBGImages.Length; i++) {
				_slugBGImages [i].sprite = _slugBGSprites [_teamSelected - 1]; 
			}

			SetNamesAndClasses (); 

		}

		public void PreviousTeam(){

			_teamSelected--; 

			if (_teamSelected < 1) {
				_teamSelected = 6; 
			}

			_teamNameBGImage.sprite = _teamNameBGSprites [_teamSelected - 1]; 

			for (int i = 0; i < _slugBGImages.Length; i++) {
				_slugBGImages [i].sprite = _slugBGSprites [_teamSelected - 1]; 
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
				if (_slug1Class == "Slug") {
					_classImage1.sprite = _snailSprite; 
					_slug1Class = "Snail"; 
				} else if (_slug1Class == "Snail") {
					_classImage1.sprite = _octopusSprite; 
					_slug1Class = "Octopus"; 
				} else if (_slug1Class == "Octopus") {
					_classImage1.sprite = _siikaSprite; 
					_slug1Class = "Siika"; 
				} else if (_slug1Class == "Siika") {
					_classImage1.sprite = _slugSprite; 
					_slug1Class = "Slug"; 
				}

				PlayerPrefs.SetString ("t" + _teamSelected + "s" + slugNumber + "Class", _slug1Class); 

				break;

			case 2:
				if (_slug2Class == "Slug") {
					_classImage2.sprite = _snailSprite; 
					_slug2Class = "Snail"; 
				} else if (_slug2Class == "Snail") {
					_classImage2.sprite = _octopusSprite; 
					_slug2Class = "Octopus"; 
				} else if (_slug2Class == "Octopus") {
					_classImage2.sprite = _siikaSprite; 
					_slug2Class = "Siika"; 
				}else if (_slug2Class == "Siika") {
					_classImage2.sprite = _slugSprite; 
					_slug2Class = "Slug"; 
				}

				PlayerPrefs.SetString ("t" + _teamSelected + "s" + slugNumber + "Class", _slug2Class); 
				
				break;

			case 3:
				if (_slug3Class == "Slug") {
					_classImage3.sprite = _snailSprite; 
					_slug3Class = "Snail"; 
				} else if (_slug3Class == "Snail") {
					_classImage3.sprite = _octopusSprite; 
					_slug3Class = "Octopus"; 
				} else if (_slug3Class == "Octopus") {
					_classImage3.sprite = _siikaSprite; 
					_slug3Class = "Siika"; 
				} else if (_slug3Class == "Siika") {
					_classImage3.sprite = _slugSprite; 
					_slug3Class = "Slug"; 
				}

				PlayerPrefs.SetString ("t" + _teamSelected + "s" + slugNumber + "Class", _slug3Class); 
				
				break;

			case 4:
				if (_slug4Class == "Slug") {
					_classImage4.sprite = _snailSprite; 
					_slug4Class = "Snail"; 
				} else if (_slug4Class == "Snail") {
					_classImage4.sprite = _octopusSprite; 
					_slug4Class = "Octopus"; 
				} else if (_slug4Class == "Octopus") {
					_classImage4.sprite = _siikaSprite; 
					_slug4Class = "Siika"; 
				} else if (_slug4Class == "Siika") {
					_classImage4.sprite = _slugSprite; 
					_slug4Class = "Slug"; 
				}

				PlayerPrefs.SetString ("t" + _teamSelected + "s" + slugNumber + "Class", _slug4Class); 

				break;

			case 5:
				if (_slug5Class == "Slug") {
					_classImage5.sprite = _snailSprite; 
					_slug5Class = "Snail"; 
				} else if (_slug5Class == "Snail") {
					_classImage5.sprite = _octopusSprite; 
					_slug5Class = "Octopus"; 
				} else if (_slug5Class == "Octopus") {
					_classImage5.sprite = _siikaSprite; 
					_slug5Class = "Siika"; 
				}else if (_slug5Class == "Siika") {
					_classImage5.sprite = _slugSprite; 
					_slug5Class = "Slug"; 
				}

				PlayerPrefs.SetString ("t" + _teamSelected + "s" + slugNumber + "Class", _slug5Class); 

				break; 

			case 6:
				if (_slug6Class == "Slug") {
					_classImage6.sprite = _snailSprite; 
					_slug6Class = "Snail"; 
				} else if (_slug6Class == "Snail") {
					_classImage6.sprite = _octopusSprite; 
					_slug6Class = "Octopus"; 
				} else if (_slug6Class == "Octopus") {
					_classImage6.sprite = _siikaSprite; 
					_slug6Class = "Siika";
				}else if (_slug6Class == "Siika") {
					_classImage6.sprite = _slugSprite; 
					_slug6Class = "Slug";
				}

				PlayerPrefs.SetString ("t" + _teamSelected + "s" + slugNumber + "Class", _slug6Class);

				break; 

			default: 
				break; 

			}

			PlayerPrefs.Save (); 

		}


		public void ProceedToGame(){

			GameSessionController._instance._numberOfTeams = _numberOfTeams; 
		}

		public void ChooseTeams(int teamNumber){

			if (!_teamsSelected[teamNumber-1] && _numberOfTeams < 4) {
				switch (teamNumber) {
				case 1:
					_slotImages [_numberOfTeams].sprite = _redSlotSprite; 
					break;
				case 2:
					_slotImages[_numberOfTeams].sprite = _blueSlotSprite; 
					break;
				case 3:
					_slotImages[_numberOfTeams].sprite = _yellowSlotSprite; 
					break;
				case 4:
					_slotImages[_numberOfTeams].sprite= _greenSlotSprite; 
					break;
				case 5:
					_slotImages[_numberOfTeams].sprite = _violetSlotSprite; 
					break;
				case 6:
					_slotImages[_numberOfTeams].sprite = _orangeSlotSprite; 
					break;
				default:
					break;
				}

				_teamsSelected [teamNumber - 1] = true; 	
				_numberOfTeams++; 

				//set team name
				GameSessionController._instance._teams [_numberOfTeams -1 ]._teamName = PlayerPrefs.GetString ("team" + teamNumber + "Name");

				//set team color
				GameSessionController._instance._teams[_numberOfTeams - 1]._teamColor = PlayerPrefs.GetString("team" + teamNumber + "Color"); 
				GameSessionController._instance._teams[_numberOfTeams - 1].SetUnityColor (); 

				//set slug names
				for(int i = 1; i < 7; i++){
					GameSessionController._instance._teams [_numberOfTeams-1]._slugNames [i-1] = PlayerPrefs.GetString ("t"+teamNumber + "s" + i + "Name"); 
				}

				//set slug class
				for(int i = 1; i < 7; i++){
					GameSessionController._instance._teams [_numberOfTeams-1]._slugClasses [i-1] = PlayerPrefs.GetString ("t"+teamNumber + "s" + i + "Class"); 
				}


					
			}

			if (_numberOfTeams >= 2) {

				Color c = _teamsNextButtonColor;
				c.a = 1;
				_teamsNextButtonImage.color = c; 
				_teamsNextButton.enabled = true; 
				_teamsNextButtonAnimator.enabled = true; 

			}
		

		}

		public void ResetTeams(){

			_numberOfTeams = 0; 

			for (int i = 0; i < _teamsSelected.Length; i++) {
				_teamsSelected [i] = false; 
			}

			for (int i = 0; i < _slotImages.Length; i++) {
				_slotImages [i].sprite = _emptySlotSprite; 
			}
				
			Color c = _teamsNextButtonColor;
			c.a = 0.5f;
			_teamsNextButtonImage.color = c; 
			_teamsNextButton.enabled = false; 
			_teamsNextButtonAnimator.enabled = false; 


		}
			

		private void SetDefaultNamesAndClasses(){

			PlayerPrefs.SetString ("team1Name", "Team1"); 
			PlayerPrefs.SetString ("team2Name", "Team2"); 
			PlayerPrefs.SetString ("team3Name", "Team3"); 
			PlayerPrefs.SetString ("team4Name", "Team4"); 
			PlayerPrefs.SetString ("team5Name", "Team5"); 
			PlayerPrefs.SetString ("team6Name", "Team6"); 

			PlayerPrefs.SetString ("team1Color", "Red"); 
			PlayerPrefs.SetString ("team2Color", "Blue"); 
			PlayerPrefs.SetString ("team3Color", "Yellow"); 
			PlayerPrefs.SetString ("team4Color", "Green"); 
			PlayerPrefs.SetString ("team5Color", "Violet"); 
			PlayerPrefs.SetString ("team6Color", "Orange"); 


			for(int i = 1; i < 7; i++){   //cycle through the teams

				for (int j = 1; j < 7; j++) {  //cycle through the slugs 

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
						PlayerPrefs.SetString ("t" + i + "s" + j + "Class", "Siika"); 
						break; 
					case 5: 
						PlayerPrefs.SetString ("t" + i + "s" + j + "Class", "Octopus"); 
						break;
					case 6:
						PlayerPrefs.SetString ("t" + i + "s" + j + "Class", "Snail"); 
						break; 
					default:
						break; 

					}
				}
			}

			PlayerPrefs.SetString ("t1s1Name", "Shellboy"); 
			PlayerPrefs.SetString ("t1s2Name", "Wartslug"); 
			PlayerPrefs.SetString ("t1s3Name", "8-Feeler"); 
			PlayerPrefs.SetString ("t1s4Name", "Crisu"); 
			PlayerPrefs.SetString ("t1s5Name", "Kauno"); 
			PlayerPrefs.SetString ("t1s6Name", "Leija"); 

			PlayerPrefs.SetString ("t2s1Name", "Mopsi"); 
			PlayerPrefs.SetString ("t2s2Name", "TheMan"); 
			PlayerPrefs.SetString ("t2s3Name", "Niska"); 
			PlayerPrefs.SetString ("t2s4Name", "Kynis"); 
			PlayerPrefs.SetString ("t2s5Name", "Rock"); 
			PlayerPrefs.SetString ("t2s6Name", "Heidi"); 

			PlayerPrefs.SetString ("t3s1Name", "Jesse"); 
			PlayerPrefs.SetString ("t3s2Name", "Sister"); 
			PlayerPrefs.SetString ("t3s3Name", "Jape"); 
			PlayerPrefs.SetString ("t3s4Name", "Milli"); 
			PlayerPrefs.SetString ("t3s5Name", "Tube"); 
			PlayerPrefs.SetString ("t3s6Name", "Katri"); 

			PlayerPrefs.SetString ("t4s1Name", "Charlie"); 
			PlayerPrefs.SetString ("t4s2Name", "Juha"); 
			PlayerPrefs.SetString ("t4s3Name", "Carl"); 
			PlayerPrefs.SetString ("t4s4Name", "Mike"); 
			PlayerPrefs.SetString ("t4s5Name", "Kevin"); 
			PlayerPrefs.SetString ("t4s6Name", "Bum"); 

			PlayerPrefs.SetString ("t5s1Name", "Hardy"); 
			PlayerPrefs.SetString ("t5s2Name", "Tank"); 
			PlayerPrefs.SetString ("t5s3Name", "Snipah"); 
			PlayerPrefs.SetString ("t5s4Name", "FatBoy"); 
			PlayerPrefs.SetString ("t5s5Name", "Juba"); 
			PlayerPrefs.SetString ("t5s6Name", "Teemu"); 

			PlayerPrefs.SetString ("t6s1Name", "Nätti"); 
			PlayerPrefs.SetString ("t6s2Name", "Nico"); 
			PlayerPrefs.SetString ("t6s3Name", "Eero"); 
			PlayerPrefs.SetString ("t6s4Name", "Kelli"); 
			PlayerPrefs.SetString ("t6s5Name", "Lee"); 
			PlayerPrefs.SetString ("t6s6Name", "Rotten"); 




				

		}
	}
}
