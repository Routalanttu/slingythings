using UnityEngine;
using System.Collections;

namespace SlingySlugs {
	public class SiikaSlinger : MonoBehaviour {

		// Pyry
		// Make timed
		// Collisioskippi ei tapahdu

		[SerializeField] private float _forceMultiplier = 4f;
		[SerializeField] private GameObject _virtualShovel;
		[SerializeField] private GameObject _auraFlame;
		private GameObject _auraInstance;

		private Transform _gcTransform; 
		private Rigidbody2D _rigidBody;
		private CharacterInfo _slug;
		private CharacterAnimator _charAnim;

		private bool _isArmed;
		private bool _isSlung;

		private float _soundCooldown = 1f;

		private Vector3 _lastPos;
		private Vector3 _lastVelo;
		private float _lastAngVelo;

		private float _shovelCooldown;
		private SpriteRenderer _flame;

		private void Awake () {
			_gcTransform = GetComponent<Transform> (); 
			_rigidBody = GetComponent<Rigidbody2D> (); 
			_slug = GetComponent<CharacterInfo> ();
			_charAnim = GetComponent<CharacterAnimator> ();
			_auraInstance = (GameObject)Instantiate (_auraFlame, transform.position, Quaternion.identity, transform);
			_flame = _auraFlame.GetComponent<SpriteRenderer> ();
			_flame.enabled = false;
		}

		public void Sling (Vector2 stretchVector) {
			SoundController.Instance.PlaySoundByIndex((int)Random.Range(18, 20)); 
			_rigidBody.AddForce (stretchVector * _forceMultiplier, ForceMode2D.Impulse);
			_shovelCooldown = 5f;
			_isArmed = true;
			GameManager.Instance.DeactivateCircleColliders(); 
			GameManager.Instance.SlugSlunged ();
			_slug.ShowName (false);
			_slug.ShowHealth (false); 
			_flame.enabled = true;
		}

		void OnCollisionEnter2D(Collision2D coll) {
			if (_isArmed && _slug.IsActive) {
				Invoke ("ShowNameAndHealth", 2); 
				_soundCooldown = 1f;
				_isArmed = false;
			} else {
				if (_soundCooldown <= 0f) {
					SoundController.Instance.PlaySoundByIndex (1);
					_soundCooldown = 1f;
				}
			}
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

			if (_isArmed) {
				Instantiate (_virtualShovel, _gcTransform.position, Quaternion.identity);
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
				_isArmed = false;
				_flame.enabled = false;
			}

			RotateAuraFlame (_rigidBody.velocity);
		}

		void ShowNameAndHealth(){
			_slug.ShowName (true); 
			_slug.ShowHealth (true); 
		}

		public void RotateAuraFlame (Vector2 curVelo) {
			float flightAngle = Mathf.Atan2 (curVelo.y, curVelo.x) * Mathf.Rad2Deg;
			_auraInstance.transform.localRotation = Quaternion.AngleAxis (flightAngle, Vector3.forward);
		}
	}
}