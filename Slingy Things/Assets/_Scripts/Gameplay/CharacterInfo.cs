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

		public void Die(){
			Instantiate(_deathAnimation, _myTransform.position, Quaternion.identity);
			GameManager.Instance.KillSlug ((int)_team, gameObject); 
			Destroy (gameObject); 
		}

	}
}