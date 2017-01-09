using UnityEngine;
using System.Collections;

namespace SlingySlugs {
	public class SnailSlinger : BaseSlinger {

		[SerializeField] private float _forceMultiplier = 3f;
		[SerializeField] private PhysicsMaterial2D _bouncy;
		[SerializeField] private PhysicsMaterial2D _normo;

		private BoxCollider2D _colli;
		private float _blowCooldown;
		private int _snailBlowCounter;

		private void Awake () {
			Init (_forceMultiplier); 
			_colli = GetComponent<BoxCollider2D> ();
			_colli.sharedMaterial = _normo;
		}

		public void Sling (Vector2 stretchVector) {
			base.Sling (stretchVector); 
			_colli.sharedMaterial = _bouncy; 
			_snailBlowCounter = 0;
		}

		// Activates explosion when snail hits terrain after being slung
		void OnCollisionEnter2D(Collision2D coll) {
			if (_isArmed && _charInfo.IsActive) {
				if (_snailBlowCounter < 3 && _blowCooldown <= 0f) {
					_explosion.Fire ();
					_blowCooldown = 0.02f;
					_soundCooldown = 1f;
					_snailBlowCounter++;
					_isArmed = true;
				// After all 3 bounces have happened, stop exploding and bouncing:
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

	}
}