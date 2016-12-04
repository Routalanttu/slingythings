﻿using UnityEngine;
using System.Collections;

namespace SlingySlugs {
	public class SlugSlinger : MonoBehaviour {

		[SerializeField] private float _forceMultiplier = 4f;

		private Transform _gcTransform; 
		private Rigidbody2D _rigidBody;
		private CharacterInfo _slug;
		private CharacterAnimator _charAnim;
		private Explosion _explosion; 

		private bool _isArmed;
		private bool _isSlung;

		private float _soundCooldown = 1f;

		private void Awake () {
			_gcTransform = GetComponent<Transform> (); 
			_rigidBody = GetComponent<Rigidbody2D> (); 
			_slug = GetComponent<CharacterInfo> ();
			_charAnim = GetComponent<CharacterAnimator> ();
			_explosion = GetComponent<Explosion> (); 
		}

		public void Sling (Vector2 stretchVector) {
			SoundController.Instance.PlaySoundByIndex((int)Random.Range(18, 20)); 
			_rigidBody.AddForce (stretchVector * _forceMultiplier, ForceMode2D.Impulse);
			_isArmed = true;
			_explosion.Arm ();
			GameManager.Instance.DeactivateCircleColliders(); 
			GameManager.Instance.SlugSlunged ();
			_slug.ShowName (false);
			_slug.ShowHealth (false); 
		}

		void OnCollisionEnter2D(Collision2D coll) {
			if (_isArmed && _slug.IsActive) {
				Invoke ("ShowNameAndHealth", 2); 
				_explosion.Fire ();
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
		}

		void Update () {
			if (_isSlung) {
				_charAnim.RotateFlight (_rigidBody.velocity);
			}

			if (_soundCooldown > 0f) {
				_soundCooldown -= Time.deltaTime;
			}
		}

		void ShowNameAndHealth(){
			_slug.ShowName (true); 
			_slug.ShowHealth (true); 
		}
	}
}