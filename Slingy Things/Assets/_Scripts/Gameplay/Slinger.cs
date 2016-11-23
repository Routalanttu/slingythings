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

		private int _siikaCounter = 0;

		private ParticleSystem _exploCutter;


		private Vector3 _lastPos;
		private Vector3 _lastVelo;
		private float _lastAngVelo;

		private void Awake () {
			_gcTransform = GetComponent<Transform> (); 
			_rigidBody = GetComponent<Rigidbody2D> (); 
			_slug = GetComponent<CharacterInfo> ();
			_charAnim = GetComponent<CharacterAnimator> ();
			_explosion = GetComponent<Explosion> (); 
			_exploCutter = _explosion.GetCutter ();
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
				_snailBlowCounter++;
				if (_slug.GetSpecies () == 2) {
					GetComponent<Pollenation> ().Fire ();
				}
				// HYI VITTU SIIKA HURGS
				if (_slug.GetSpecies () != 3) {
					_isArmed = false;
				} else {
					if (_snailBlowCounter < 9) {
						_gcTransform.position = _lastPos;
						_rigidBody.velocity = _lastVelo;
						_rigidBody.angularVelocity = _lastAngVelo;
						Physics2D.IgnoreCollision (GetComponent<BoxCollider2D> (), coll.collider);
					} else {
						_isSlung = false;
						_isArmed = false;
					}
				}
			} else {
				if (_soundCooldown <= 0f) {
					SoundController.Instance.PlaySoundByIndex (1);
					_soundCooldown = 1f;
				}
			}

			if (_snailBlowCounter < 2 && _slug.GetSpecies() == 1) {
				_isArmed = true;
				_isSlung = true;
				//_snailBlowCounter++;
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

			_siikaCounter++;
			if (_isArmed && _slug.GetSpecies() == 3) {
				//Debug.Log ("meni läpi");
				//Instantiate (_exploCutter, _gcTransform.position, Quaternion.identity);
				_explosion.Fire();
				_siikaCounter = 0;
			}


			_lastPos = _gcTransform.position;
			_lastVelo = _rigidBody.velocity;
			_lastAngVelo = _rigidBody.angularVelocity;

		}

	}
}
