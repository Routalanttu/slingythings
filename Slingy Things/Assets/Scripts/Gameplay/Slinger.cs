using UnityEngine;
using System.Collections;

namespace SlingySlugs {
	public class Slinger : MonoBehaviour {

		[SerializeField] private float _forceMultiplier; 
		[SerializeField] private float _maxPower; 
		[SerializeField] private float _minPower;

		[SerializeField] private PyryAttack _attack; 

		private Vector2 _slingDir; 
		private Vector2 _vectorToMouse;
		private Vector2 _clampedVectorToMouse; 
		private Vector2 _slugPosition;

		private bool _fire;

		private Vector2 _mousePos;

		private Rigidbody2D _rigidBody; 
		private Transform _gcTransform; 

		private bool _isSlung;

		//Pyry
		[SerializeField] private Transform _flyTrans;
		private CharacterAnimator _charAnim;

		// Use this for initialization
		private void Awake () {

			_rigidBody = GetComponent<Rigidbody2D> (); 
			_gcTransform = GetComponent<Transform> (); 

			if (_maxPower == 0) {
				_maxPower = 2.5f; 
			}

			// Pyry
			_charAnim = GetComponent<CharacterAnimator> ();
			_charAnim.SetToIdle ();

		}

		// Update is called once per frame
		void Update () {


			_mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition); 


			//Pyry
			if (_isSlung) {
				Vector2 curVelo = _rigidBody.velocity;
				float angle = Mathf.Atan2 (curVelo.y, curVelo.x) * Mathf.Rad2Deg;
				_flyTrans.localRotation = Quaternion.AngleAxis (angle, Vector3.forward);
			}

			if (_rigidBody.velocity.x < 0f) {
				_charAnim.FlightFlip (true);
			} else if (_rigidBody.velocity.x > 0f) {
				_charAnim.FlightFlip (false);
			}


		}

		public void Sling (Vector2 stretchVector) {
			_rigidBody.AddForce (stretchVector * _forceMultiplier, ForceMode2D.Impulse);

			_charAnim.SetToFlight ();
			_isSlung = true;
		}

		void OnCollisionEnter2D(Collision2D coll) {
			_isSlung = false;
			_charAnim.SetToIdle ();
			_attack.Fire ();
		}


	}
}
