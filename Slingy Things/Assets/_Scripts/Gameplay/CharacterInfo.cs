using UnityEngine;
using System.Collections;

namespace SlingySlugs {
	public class CharacterInfo : MonoBehaviour {

		public enum Team 
		{
			Team1 = 1, 
			Team2 = 2, 
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
			Ferret
		}

		[SerializeField] private Species _species;
		[SerializeField] private int _health = 100;
		[SerializeField] private bool _dead = false;
		[SerializeField] private GameObject _deathAnimation;
		private Transform _myTransform;

		private void Awake () {
			_myTransform = GetComponent<Transform> ();
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
		}

		public void IncreaseHealth(int healAmount) {
			_health += healAmount;

			// Take away the excess health gain:
			if (_health > 100) {
				healAmount -= (_health - 100);
				_health = 100;
			}
			GameManager.Instance.DecreaseTeamHealth ((int)_team, healAmount);
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
	}
}