using UnityEngine;
using System.Collections;

namespace SlingySlugs {
	public class Slinger : MonoBehaviour {

		[SerializeField] private float _forceMultiplier;

		// The Attack script should be renamed "Explosion":
		private Explosion _explosion; 

		private Rigidbody2D _rigidBody; 
		private Transform _gcTransform; 

		// Käytetäänkö tätä armia mihinkään todella?
		// Siis mihinkään, mikä ei toimisi tuolla slungillakin?
		private bool _isArmed;
		private bool _isSlung;

		private float _soundCooldown = 1f;

		//[SerializeField] private Transform _flyTrans;
		private CharacterAnimator _charAnim;

		private CharacterInfo _slug;

		private void Awake () {
			_rigidBody = GetComponent<Rigidbody2D> (); 
			_gcTransform = GetComponent<Transform> (); 
			_charAnim = GetComponent<CharacterAnimator> ();
			_charAnim.SetToIdle ();
			_explosion = GetComponent<Explosion> (); 
			_slug = GetComponent<CharacterInfo> ();
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
			_charAnim.SetToFlight ();
			_isSlung = true;
			_isArmed = true;
			_explosion.Arm ();
		}

		void OnCollisionEnter2D(Collision2D coll) {
			_isSlung = false;
			_charAnim.SetToIdle ();
			// Placeholder functionality; should be "SetToLimp"
			if (_isArmed && _slug.IsActive) {
				//_charAnim.SetToIdle ();
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
		}

		void OnCollisionStay2D(Collision2D coll) {
			_charAnim.SetToIdle ();
		}

		void OnCollisionExit2D(Collision2D coll) {
			SetToSlung ();
		}

		public void SetToSlung () {
			_isSlung = true;
			_charAnim.SetToFlight ();
		}




	}
}
