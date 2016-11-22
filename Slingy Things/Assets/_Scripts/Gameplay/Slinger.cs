using UnityEngine;
using System.Collections;

namespace SlingySlugs {
	public class Slinger : MonoBehaviour {

		[SerializeField] private float _forceMultiplier;

		private Explosion _explosion; 

		private Rigidbody2D _rigidBody; 
		private Transform _gcTransform; 

		private bool _isArmed;
		private bool _isSlung;

		private float _soundCooldown = 1f;

		private CharacterAnimator _charAnim;

		private CharacterInfo _slug;

		private int _snailBlowCounter = 2;

		private void Awake () {
			_gcTransform = GetComponent<Transform> (); 
			_rigidBody = GetComponent<Rigidbody2D> (); 
			_slug = GetComponent<CharacterInfo> ();
			_charAnim = GetComponent<CharacterAnimator> ();
			_explosion = GetComponent<Explosion> (); 
		}

		void Update () {
			if (_isSlung) {
				_charAnim.RotateFlight (_rigidBody.velocity);
			}

			if (_soundCooldown > 0f) {
				_soundCooldown -= Time.deltaTime;
			}
		}

		public void Sling (Vector2 stretchVector) {
			_rigidBody.AddForce (stretchVector * _forceMultiplier, ForceMode2D.Impulse);
			_isSlung = true;
			_isArmed = true;
			_explosion.Arm ();
			_snailBlowCounter = 0;
			GameManager.Instance.DeactiveCircleColliders(); 
		}

		void OnCollisionEnter2D(Collision2D coll) {
			_isSlung = false;
			if (_isArmed && _slug.IsActive) {
				_explosion.Fire ();
				_soundCooldown = 1f;
				if (_slug.GetSpecies () == 2) {
					GetComponent<Pollenation> ().Fire ();
				}
				_isArmed = false;
			} else {
				if (_soundCooldown <= 0f) {
					SoundController.Instance.PlaySoundByIndex (1);
					_soundCooldown = 1f;
				}
			}

			if (_snailBlowCounter < 2 && _slug.GetSpecies() == 1) {
				_isArmed = true;
				_isSlung = true;
				_snailBlowCounter++;
				_explosion.Fire ();
			}
			//Debug.Log (_snailBlowCounter + " " + _isArmed);
		}
			
		public void SetToSlung () {
			_isSlung = true;
			_charAnim.SetToFlight ();
		}
			
		public bool GetArmedState() {
			return _isArmed;
		}

		private void FixedUpdate () {
			if (Mathf.Abs (_rigidBody.velocity.x) > 0.4f || Mathf.Abs (_rigidBody.velocity.y) > 0.4f) {
				SetToSlung ();
			} else {
				_charAnim.SetToIdle ();
			}
		}

	}
}
