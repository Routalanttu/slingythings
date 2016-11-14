using UnityEngine;
using System.Collections;

namespace SlingySlugs{

	public class GameSessionController : MonoBehaviour {

		private int numberOfPlayers; 

		private bool team1Selected;
		private bool team2Selected;
		private bool team3Selected;
		private bool team4Selected;

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


	}
}

