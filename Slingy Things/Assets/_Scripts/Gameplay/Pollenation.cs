﻿using UnityEngine;
using System.Collections;

namespace SlingySlugs {
	public class Pollenation : MonoBehaviour {

		[SerializeField] private float radius = 10.0f;
		[SerializeField] private int _healAmountMultiplier = 2; 
		[SerializeField] private ParticleSystem _cutterPrefab; 
		[SerializeField] private GameObject _pollenAnimation; 
		[SerializeField] private GameObject _healPoof;
		private Transform _gcTransform;
		private int _team;

		void Start(){
			_team = GetComponent<CharacterInfo> ().Team;
			_gcTransform = GetComponent<Transform>(); 
		}

		public bool Fire(){
			Vector2 xploPos = _gcTransform.position; 
			Instantiate(_pollenAnimation, _gcTransform.position, Quaternion.identity);
			Instantiate(_cutterPrefab, _gcTransform.position, Quaternion.identity);
			SoundController.Instance.PlaySoundByIndex (20); 

			Collider2D[] colliders = Physics2D.OverlapCircleAll(xploPos, radius);
			foreach (Collider2D hit in colliders) {
				Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();

				Vector2 hitPosition = hit.GetComponent<Transform>().position;
				Vector2 healDelta = hitPosition- xploPos; //Get vector between explosion and hit 
				float deltaDistance = radius - healDelta.magnitude; //get the effective blast magnitude
				int healAmount = (int)deltaDistance * _healAmountMultiplier; 

				if (rb != null && 
					hit.gameObject.tag == "Slug" && 
					hit.transform.GetComponent<CharacterInfo>().Team == _team &&
					rb != this.gameObject.GetComponent<Rigidbody2D>())
				{  
					hit.GetComponent<CharacterInfo> ().IncreaseHealth (healAmount);
					Instantiate (_healPoof, hit.transform.position, Quaternion.identity);
					Debug.Log ("Heal Heal Heal"); 
				}
			}

			// Joku Invoke sen kadotettavan objektin tappamiselle?

			return false;
		}
	}
}