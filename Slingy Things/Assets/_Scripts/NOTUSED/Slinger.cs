using UnityEngine;
using System.Collections;

namespace SlingySlugs {
	public class Slinger : MonoBehaviour {

		[SerializeField] private float _forceMultiplier = 4f;

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

		private GameObject _scorcher;

		private void Awake () {
			_gcTransform = GetComponent<Transform> (); 
			_rigidBody = GetComponent<Rigidbody2D> (); 
			_slug = GetComponent<CharacterInfo> ();
			_charAnim = GetComponent<CharacterAnimator> ();
			_explosion = GetComponent<Explosion> (); 
			_exploCutter = _explosion.GetCutter ();

			if (_slug.GetSpecies () == 3) {
				_scorcher = (GameObject)Instantiate(GameObject.FindGameObjectWithTag ("Scorcher"),_gcTransform.position,Quaternion.identity);
			}
		}

		void Update () {
			if (_isSlung) {
				_charAnim.RotateFlight (_rigidBody.velocity);
			}

			if (_soundCooldown > 0f) {
				_soundCooldown -= Time.deltaTime;
			}

			_siikaCounter++;

			if (_scorcher != null && _isArmed && _slug.GetSpecies () == 3) {
				_scorcher.transform.position = _gcTransform.position;
			}
		}

		public void Sling (Vector2 stretchVector) {
            SoundController.Instance.PlaySoundByIndex((int)Random.Range(18, 20)); 
			_rigidBody.AddForce (stretchVector * _forceMultiplier, ForceMode2D.Impulse);
			_isSlung = true;
			_isArmed = true;
			_explosion.Arm ();
			_snailBlowCounter = 0;
			GameManager.Instance.DeactivateCircleColliders(); 
			GameManager.Instance.SlugSlunged (); 
			_slug.ShowName (false);
			_slug.ShowHealth (false); 
			if (_slug.GetSpecies () == 3) {
				_scorcher.SetActive (true);
				_scorcher.transform.position = _gcTransform.position;
			}
		}

		void OnCollisionEnter2D(Collision2D coll) {

           
			_isSlung = false;

			if (_isArmed && _slug.IsActive) {
				Invoke ("ShowNameAndHealth", 2); 
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
						_scorcher.SetActive (false);
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
				
			if (_isArmed && _slug.GetSpecies() == 3) {
				//Debug.Log ("meni läpi");
				Instantiate (_exploCutter, _gcTransform.position, Quaternion.identity);
				if (_siikaCounter > 9) {
					//_explosion.Fire ();
					_siikaCounter = 0;
				}
			}


			_lastPos = _gcTransform.position;
			_lastVelo = _rigidBody.velocity;
			_lastAngVelo = _rigidBody.angularVelocity;

		}

		void ShowNameAndHealth(){
			_slug.ShowName (true); 
			_slug.ShowHealth (true); 
		}

	}
}
