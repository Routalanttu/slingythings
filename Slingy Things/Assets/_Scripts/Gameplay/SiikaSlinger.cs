using UnityEngine;
using System.Collections;

namespace SlingySlugs {
	public class SiikaSlinger : BaseSlinger {

		[SerializeField] private float _forceMultiplier = 3;
		[SerializeField] private GameObject _virtualShovel;
		[SerializeField] private GameObject _auraFlame;

		private GameObject _auraInstance;

		private Vector3 _lastPos;
		private Vector3 _lastVelo;
		private float _lastAngVelo;
		private float _shovelCooldown;
		private SpriteRenderer _flame;

		private void Awake () {
			//Initialize base slinger class
			Init (_forceMultiplier); 
			_auraInstance = (GameObject)Instantiate (_auraFlame, transform.position, Quaternion.identity, transform);
			_flame = _auraInstance.GetComponent<SpriteRenderer> ();
			_flame.enabled = false;
		}

		public void Sling (Vector2 stretchVector) {
			base.Sling (stretchVector); 
			_shovelCooldown = 2f;
			_flame.enabled = true;
			Instantiate (_virtualShovel, _gcTransform.position, Quaternion.identity, _gcTransform);
		}

		void OnCollisionEnter2D(Collision2D coll) {
			if (_isArmed && _charInfo.IsActive) {
				_gcTransform.position = _lastPos;
				_rigidBody.velocity = _lastVelo;
				_rigidBody.angularVelocity = _lastAngVelo;
				if (coll.gameObject.CompareTag ("Slug")) {
					coll.gameObject.GetComponent<CharacterInfo> ().DecreaseHealth (20);
				}
				Physics2D.IgnoreCollision (GetComponent<BoxCollider2D> (), coll.collider);
			} else {
				if (_soundCooldown <= 0f) {
					SoundController.Instance.PlaySoundByIndex (1);
					_soundCooldown = 1f;
				}
			}
		}

		// Allows tbe Siika to be thrown through the ground it's standing on without any first-frame bumps
		void OnCollisionStay2D(Collision2D coll) {
			if (_isArmed && _charInfo.IsActive) {
				_gcTransform.position = _lastPos;
				_rigidBody.velocity = _lastVelo;
				_rigidBody.angularVelocity = _lastAngVelo;
				Physics2D.IgnoreCollision (GetComponent<BoxCollider2D> (), coll.collider);
			}


		}

		private void FixedUpdate () {
			if (Mathf.Abs (_rigidBody.velocity.x) > 0.4f || Mathf.Abs (_rigidBody.velocity.y) > 0.4f) {
				SetToSlung ();
			} else {
				_charAnim.SetToIdle ();
			}

			_lastPos = _gcTransform.position;
			_lastVelo = _rigidBody.velocity;
			_lastAngVelo = _rigidBody.angularVelocity;
		}

		void Update () {
			if (_isSlung) {
				_charAnim.RotateFlight (_rigidBody.velocity);
			}

			if (_soundCooldown > 0f) {
				_soundCooldown -= Time.deltaTime;
			}

			if (_shovelCooldown > 0f) {
				_shovelCooldown -= Time.deltaTime;
			}

			if (_isArmed && _shovelCooldown < 0f) {
				Invoke ("ShowNameAndHealth", 2); 
				_isArmed = false;
				_flame.enabled = false;
			}

			RotateAuraFlame (_rigidBody.velocity);
		}

		public void RotateAuraFlame (Vector2 curVelo) {
			float flightAngle = Mathf.Atan2 (curVelo.y, curVelo.x) * Mathf.Rad2Deg;
			_auraInstance.transform.localRotation = Quaternion.AngleAxis (flightAngle, Vector3.forward);
		}
	}
}