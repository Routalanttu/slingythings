using UnityEngine;
using System.Collections;

namespace SlingySlugs {
	public class SnailSlinger : MonoBehaviour {

		[SerializeField] private float _forceMultiplier = 4f;
		[SerializeField] private PhysicsMaterial2D _bouncy;
		[SerializeField] private PhysicsMaterial2D _normo;

		private Transform _gcTransform; 
		private Rigidbody2D _rigidBody;
		private BoxCollider2D _colli;
		private CharacterInfo _snail;
		private CharacterAnimator _charAnim;
		private Explosion _explosion; 

		private bool _isArmed;
		private bool _isSlung;

		private float _soundCooldown = 1f;
		private float _blowCooldown;

		private int _snailBlowCounter;

		private void Awake () {
			_gcTransform = GetComponent<Transform> (); 
			_rigidBody = GetComponent<Rigidbody2D> (); 
			_snail = GetComponent<CharacterInfo> ();
			_charAnim = GetComponent<CharacterAnimator> ();
			_explosion = GetComponent<Explosion> ();
			_colli = GetComponent<BoxCollider2D> ();
			_colli.sharedMaterial = _normo;
		}

		public void Sling (Vector2 stretchVector) {
			SoundController.Instance.PlaySoundByIndex((int)Random.Range(18, 20)); 
			_rigidBody.AddForce (stretchVector * _forceMultiplier, ForceMode2D.Impulse);
			_colli.sharedMaterial = _bouncy;
			_isArmed = true;
			_explosion.Arm ();
			_snailBlowCounter = 0;
			GameManager.Instance.DeactivateCircleColliders(); 
			GameManager.Instance.SlugSlunged ();
			_snail.ShowName (false);
			_snail.ShowHealth (false); 
		}

		void OnCollisionEnter2D(Collision2D coll) {
			if (_isArmed && _snail.IsActive) {
				if (_snailBlowCounter < 3 && _blowCooldown <= 0f) {
					_explosion.Fire ();
					_blowCooldown = 0.02f;
					_soundCooldown = 1f;
					_snailBlowCounter++;
					_isArmed = true;
				} else if (_snailBlowCounter >= 3) {
					_colli.sharedMaterial = _normo;
					Invoke ("ShowNameAndHealth", 2);
					_isArmed = false;
				}
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
		}

		void Update () {
			if (_isSlung) {
				_charAnim.RotateFlight (_rigidBody.velocity);
			}

			if (_soundCooldown > 0f) {
				_soundCooldown -= Time.deltaTime;
			}

			if (_blowCooldown > 0f) {
				_blowCooldown -= Time.deltaTime;
			}
		}

		void ShowNameAndHealth(){
			_snail.ShowName (true); 
			_snail.ShowHealth (true); 
		}
	}
}