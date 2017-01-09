using UnityEngine;
using System.Collections;

namespace SlingySlugs {
	public class OctoSlinger : BaseSlinger {

		[SerializeField] private float _forceMultiplier = 3f;
		private Pollenation _pollenation; 

		private void Awake () {
			Init (_forceMultiplier); 
			_pollenation = GetComponent<Pollenation> (); 
		}

		//When slug collides with terrain after being slung, activate pollenation
		void OnCollisionEnter2D(Collision2D coll) {
			if (_isArmed && _charInfo.IsActive) {
				Invoke ("ShowNameAndHealth", 2); 
				_pollenation.Fire ();
				_soundCooldown = 1f;
				_isArmed = false;
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
		}

	}
}