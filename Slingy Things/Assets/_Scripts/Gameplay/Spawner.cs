using UnityEngine;
using System.Collections;

namespace SlingySlugs {
	public class Spawner : MonoBehaviour {

		GameObject[] _spawnPoints;
		bool[] _indexesUsed;
		int _index;

		private string[] _teamNames; 
		private Team[] _teams; 
		private int _numberOfTeams; 
		private GameSessionController _gameSessionController; 


		[SerializeField] private GameObject _slugPrefab;
		[SerializeField] private GameObject _octopusPrefab;
		[SerializeField] private GameObject _snailPrefab;

		void Awake () {
			// The scene should have empty GameObjects tagged with "Respawn" (or a tag of your choice).
			_spawnPoints = GameObject.FindGameObjectsWithTag ("Respawn");
			_indexesUsed = new bool[_spawnPoints.Length];
	 
			_numberOfTeams = GameSessionController._instance._numberOfTeams;
			_teams = GameSessionController._instance._teams; 

			SpawnCharacters (); 

			//PyrynSpawneri (); DELETE THIS
		}

		void Start(){

			//test game session controller at the start of game to ensure it has persisted from menu
			GameSessionController._instance.TestLog (); 

		}

		void SpawnCharacters (){

			// Note: there needs to be more spawnpoints than characters spawned

			GameObject tmpPrefab;

			//iterate teams
			for (int i = 0; i < _numberOfTeams; i++) {

				for (int j = 0; j < 6; j++) {

					if (_teams [i]._slugClasses [j] == "Slug") {
						tmpPrefab = _slugPrefab; 
					} else if (_teams [i]._slugClasses [j] == "Octopus") {
						tmpPrefab = _octopusPrefab; 
					}else{
						tmpPrefab = _snailPrefab; 
					}
	
					//loop spawnpoints to find a free random spawnpoint 
					do {
						_index = Random.Range (0, _spawnPoints.Length);
					} while (_indexesUsed [_index]);
					_indexesUsed [_index] = true;

					GameObject spawnedSlug = (GameObject)Instantiate (tmpPrefab, _spawnPoints [_index].transform.position, Quaternion.identity);
					spawnedSlug.GetComponent<CharacterInfo> ().SetTeam (i+1);
					spawnedSlug.GetComponent<CharacterInfo> ().SetColor (_teams[i]._teamColor);
							
				} //end inner for loop



			} //end outer for loop

		} //end spawncharacters

		void PyrynSpawneri(){   //DELETE THIS METHOD
			
			for (int i = 0; i < 12; i++) {
				do {
					_index = Random.Range (0, _spawnPoints.Length);
				} while (_indexesUsed [_index] == true);
				_indexesUsed [_index] = true;

				GameObject blerg = (GameObject)Instantiate (_slugPrefab, _spawnPoints [_index].transform.position, Quaternion.identity);
				if (i < 6) {
					blerg.GetComponent<CharacterInfo> ().SetTeam (1);
				} else if (i >= 6) {
					blerg.GetComponent<CharacterInfo> ().SetTeam (2);
				}
			}

		}
	}

}