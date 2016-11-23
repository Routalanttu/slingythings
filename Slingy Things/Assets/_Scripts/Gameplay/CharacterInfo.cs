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
			int mitavittua = (int)_team;
			return mitavittua;
		}

		private enum Species {
			Slug,
			Snail,
			Octopus,
			Siika,
		}

		[SerializeField] private Species _species;
		[SerializeField] private int _health = 100;
		[SerializeField] private GameObject _deathAnimation;
		[SerializeField] private GameObject _healthTextObject;
		[SerializeField] private GameObject _nameTextObject;
		private TextMesh _healthText;
		private TextMesh _nameText; 
		private Transform _myTransform;

		private string _characterName; 

		private bool _dead = false;

		private void Awake () {
			_myTransform = GetComponent<Transform> ();

			_healthTextObject = (GameObject)Instantiate (
				_healthTextObject, transform.position + new Vector3(0f,1f,0f), 
				Quaternion.identity, transform);

			_nameTextObject = (GameObject)Instantiate (
				_nameTextObject, transform.position + new Vector3(0f,1.9f,0f), 
				Quaternion.identity, transform);
			
			_healthText = _healthTextObject.GetComponent<TextMesh> ();
			_healthText.GetComponent<TextMesh> ().text = _health.ToString();

			_nameText = _nameTextObject.GetComponent<TextMesh> (); 
			_nameText.GetComponent<TextMesh> ().text = "defaultname"; 
		}

		public int Health {
			get { return _health; }
		}

		public bool IsActive {
			set; 
			get; 
		}

		public void SetName(string name){
			_characterName = name; 
			_nameText.GetComponent<TextMesh> ().text = _characterName; 
		}

		public void DecreaseHealth(int damageAmount) {
			_health -= damageAmount;

			// Take away the unnecessary (below-zero) damage from team counter:
			if (_health < 0) {
				damageAmount += _health;
			}
			GameManager.Instance.DecreaseTeamHealth ((int)_team, damageAmount); 

			if(!_dead && _health <= 0) {
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
			_dead = true;
			Instantiate(_deathAnimation, _myTransform.position, Quaternion.identity);
			GameManager.Instance.KillSlug ((int)_team, gameObject);
		}

		public int GetSpecies () {
			return (int)_species;
		}

		public void SetTeam (int team) {
			_team = (Team)team;
		}

		public void SetColor(Color unityColor){

			_healthText.color = unityColor;
			_nameText.color = unityColor; 

		}

		public void SetDeath () {
			_dead = true;
		}
	}
}