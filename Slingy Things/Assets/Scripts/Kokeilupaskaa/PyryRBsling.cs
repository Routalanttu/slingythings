using UnityEngine;
using System.Collections;

namespace SlingySlugs {
	public class PyryRBsling : MonoBehaviour {

		public float _forceAmount; 
		public float _maxPower; 

		public PyryAttack _attack; 

		Vector2 _slingDir; 
		Vector2 _vectorToMouse;
		Vector2 _clampedVectorToMouse; 
		Vector2 _slugPosition;

		bool _fire;

		private Vector2 _mousePos;

		Rigidbody2D _rigidBody; 
		Transform _gcTransform; 

		private bool _flung;

		//Pyry
		[SerializeField] private Transform _flyTrans;
		//private SpriteRenderer _flyAnim;
		//[SerializeField] private SpriteRenderer _idleAnim;
		//[SerializeField] private SpriteRenderer _idleTail;

		// Yryp
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
			_charAnim.SetFlightVisibility (false);

		}

		// Update is called once per frame
		void Update () {


			_mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition); 


			//Pyry
			if (_flung) {
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

		void OnMouseDown(){

			//Do we need this?
		}

		void OnMouseUp(){

			_slugPosition = _gcTransform.position; //have to use the vector2 form for below
			_vectorToMouse = _mousePos - _slugPosition; 

			if (_vectorToMouse.magnitude > _maxPower) {
				_vectorToMouse = Vector2.ClampMagnitude (_vectorToMouse, _maxPower);
			} 

			_rigidBody.AddForce (-_vectorToMouse * _forceAmount, ForceMode2D.Impulse);

			_charAnim.SetFlightVisibility (true);
			_flung = true;

		}

		void OnCollisionEnter2D(Collision2D coll) {
			_flung = false;
			_charAnim.SetFlightVisibility (false);
			_attack.Fire ();
		}


	}
}
