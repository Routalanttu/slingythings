using UnityEngine;
using System.Collections;

namespace SlingySlugs {
	public class CharacterInfo : MonoBehaviour {

		public enum Team 
		{
			Team1 = 1, 
			Team2 = 2, 
			Team3 = 3,
			Team4 = 4,
		};

		[SerializeField] private Team _team = Team.Team1;

		public int GetTeam () {
			int teamNumber = (int)_team;
			return teamNumber;
		}

		private enum Species {
			Slug,
			Snail,
			Octopus,
			Ferret
		}

		[SerializeField] private Species _species;
		[SerializeField] private int _health = 100;
		[SerializeField] private bool _dead = false;
		[SerializeField] private GameObject _deathAnimation;
		[SerializeField] private GameObject _healthTextObject;

		private TextMesh _healthText;
		private Transform _myTransform;

		private void Awake () {
			_myTransform = GetComponent<Transform> ();

			_healthTextObject = (GameObject)Instantiate (
				_healthTextObject, transform.position + new Vector3(0f,1f,0f), 
				Quaternion.identity, transform);
			_healthText = _healthTextObject.GetComponent<TextMesh> ();
			_healthText.GetComponent<TextMesh> ().text = _health.ToString();
		}

		public int Health {
			get { return _health; }
		}

		public bool IsActive {
			set; 
			get; 
		}

		public void DecreaseHealth(int damageAmount) {
			_health -= damageAmount;

			// Take away the unnecessary (below-zero) damage from team counter:
			if (_health < 0) {
				damageAmount += _health;
			}
			GameManager.Instance.DecreaseTeamHealth ((int)_team, damageAmount); 

			if(_health <= 0) {
				Die (); 
			}

			_healthText.GetComponent<TextMesh> ().text = _health.ToString();
		}

		public void IncreaseHealth(int healAmount) {
			_health += healAmount;

			// Take away the excess health gain:
			if (_health > 100) {
				healAmount -= (_health - 100);
				_health = 100;
			}
			GameManager.Instance.DecreaseTeamHealth ((int)_team, healAmount);

			_healthText.GetComponent<TextMesh> ().text = _health.ToString();
		}

		public void Die(){
			Instantiate(_deathAnimation, _myTransform.position, Quaternion.identity);
			GameManager.Instance.KillSlug ((int)_team, gameObject); 
			Destroy (gameObject); 
		}

		public int GetSpecies () {
			return (int)_species;
		}

		public void SetTeam (int team) {
			_team = (Team)team;
		}

		public void SetColor(string colorName){

			Debug.Log (colorName); 

			if (colorName == "Red") {
				Color tmp = _healthText.color;
				tmp.r = 255f;
				tmp.g = 0f;
				tmp.b = 0f;
				_healthText.color = tmp;
			} else if (colorName == "Blue") {
				Color tmp = _healthText.color;
				tmp.r = 0f;
				tmp.g = 0f;
				tmp.b = 255f;
				_healthText.color = tmp;
			} else if (colorName == "Yellow") {
				Color tmp = _healthText.color;
				tmp.r = 255f;
				tmp.g = 255f;
				tmp.b = 0f;
				_healthText.color = tmp;
			} else if (colorName == "Green") {
				Color tmp = _healthText.color;
				tmp.r = 0;
				tmp.g = 255f;
				tmp.b = 50f;
				_healthText.color = tmp;
			} else if (colorName == "Violet") {
				Color tmp = _healthText.color;
				tmp.r = 255f;
				tmp.g = 0f;
				tmp.b = 255f;
				_healthText.color = tmp;
			} else if (colorName == "Orange") {
				Color tmp = _healthText.color;
				tmp.r = 255f;
				tmp.g = 200f;
				tmp.b = 0f;
				_healthText.color = tmp;
			} else if (colorName == "Coral") {
				Color tmp = _healthText.color;
				tmp.r = 40f;
				tmp.g = 200f;
				tmp.b = 200f;
				_healthText.color = tmp;
			}else {
				Color tmp = _healthText.color;
				tmp.r = 0f;
				tmp.g = 0f;
				tmp.b = 0f;
				_healthText.color = tmp;
			}

		}

	}
}