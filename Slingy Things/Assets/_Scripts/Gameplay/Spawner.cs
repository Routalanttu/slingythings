using UnityEngine;
using System.Collections;

namespace SlingySlugs {
	public class Spawner : MonoBehaviour {

		GameObject[] _spawnPoints;
		bool[] _indexesUsed;
		int _index;

		[SerializeField] private GameObject _slugPrefab;

		void Awake () {
			// The scene should have empty GameObjects tagged with "Respawn" (or a tag of your choice).
			_spawnPoints = GameObject.FindGameObjectsWithTag ("Respawn");
			_indexesUsed = new bool[_spawnPoints.Length];

			// Note: there needs to be more spawnpoints than characters spawned
			for (int i = 0; i < 12; i++) {
				do {
					_index = Random.Range (0, _spawnPoints.Length);
				} while (_indexesUsed [_index] == true);
				_indexesUsed [_index] = true;

				GameObject blerg = (GameObject)Instantiate (_slugPrefab, _spawnPoints [_index].transform.position, Quaternion.identity);
				if (i >= 6) {
					blerg.GetComponent<CharacterInfo> ().SetTeam (2);
				}
			}
		}
	}

}