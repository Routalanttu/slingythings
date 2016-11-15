using UnityEngine;
using System.Collections;

namespace SlingySlugs{

	public class GameSessionController : MonoBehaviour {

		private int numberOfPlayers; 

		private bool team1Selected;
		private bool team2Selected;
		private bool team3Selected;
		private bool team4Selected;
		private bool team5Selected;
		private bool team6Selected;

		private string team1; 
		private string team2;
		private string team3;
		private string team4;

		public static GameSessionController _instance; 

		void Awake(){
			if (_instance == null) {
				_instance = this; 
			} else if (_instance != this) {
				Destroy (this); 
			}
		}

		// Use this for initialization
		void Start () {

			DontDestroyOnLoad (gameObject); 

		}

		// Update is called once per frame
		void Update () {

		}

		public void setTeams(){
		

		}

		public void ChooseTeam(int teamNumber){

			switch (teamNumber) {
			case 1:
				team1Selected = true; 
				break;
			case 2:
				team2Selected = true; 
				break;
			case 3:
				team3Selected = true; 
				break;
			case 4:
				team4Selected = true; 
				break;
			case 5:
				team5Selected = true; 
				break;
			case 6:
				team6Selected = true; 
				break;
			default:
				break;
			}

			numberOfPlayers++; 


			if (numberOfPlayers >= 2) {

				//activate go to game button
			}


		}


	}


	/*
	 * 
	 * klikkaa tiimin nappulaa: 

—>   tiimi1 selected = true 

if(team1)



numberofteams ++ 

if (number teams)>= 2  voi jatkaa peliin 
*/
}

