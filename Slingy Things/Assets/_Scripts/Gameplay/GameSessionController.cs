using UnityEngine;
using System.Collections;

namespace SlingySlugs{

	public class GameSessionController : MonoBehaviour {

		public int _numberOfTeams;
		public Team[] _teams;   //setted in teammanaager / choose team

		public static GameSessionController _instance; 

		void Awake(){
			if (_instance == null) {
				_instance = this; 
			} else if (_instance != this) {
				Destroy (this); 
			}

			_teams = new Team[4]; 

			for (int i = 0; i < _teams.Length; i++) {
				_teams [i] = new Team (); 
			}


		}

		// Use this for initialization
		void Start () {

			DontDestroyOnLoad (gameObject); 

		}

	

		public void TestLog(){

			Debug.Log ("GameSessionController on siirtynyt menusta peliin"); 

		}


	}
}



