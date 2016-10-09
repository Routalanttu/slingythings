using UnityEngine;
using System.Collections;

namespace SlingySlugs {
	public class Slinger : MonoBehaviour {

		[SerializeField] private float _forceMultiplier;

		// The Attack script should be renamed "Explosion":
		private Explosion _explosion; 

		private Rigidbody2D _rigidBody; 
		private Transform _gcTransform; 

		private bool _isSlung;

		//[SerializeField] private Transform _flyTrans;
		private CharacterAnimator _charAnim;

		private void Awake () {
			_rigidBody = GetComponent<Rigidbody2D> (); 
			_gcTransform = GetComponent<Transform> (); 
			_charAnim = GetComponent<CharacterAnimator> ();
			_charAnim.SetToIdle ();
			_explosion = GetComponent<Explosion> ();
		}

		void Update () {
			if (_isSlung) {
				_charAnim.RotateFlight (_rigidBody.velocity);
			}
		}

		public void Sling (Vector2 stretchVector) {
			_rigidBody.AddForce (stretchVector * _forceMultiplier, ForceMode2D.Impulse);
			_charAnim.SetToFlight ();
			_isSlung = true;
		}

		void OnCollisionEnter2D(Collision2D coll) {
			_isSlung = false;
			// Placeholder functionality; should be "SetToLimp"
			_charAnim.SetToIdle ();
			_explosion.Fire ();
		}


	}
}
