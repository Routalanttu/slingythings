using UnityEngine;
using System.Collections;

namespace SlingySlugs {
	public class TempTurnVisualizer : MonoBehaviour {

		[SerializeField] private SpriteRenderer _redTurn;
		[SerializeField] private SpriteRenderer _blueTurn;
		[SerializeField] private SpriteRenderer _yellowTurn;
		[SerializeField] private SpriteRenderer _greenTurn;
		[SerializeField] private SpriteRenderer _violetTurn;
		[SerializeField] private SpriteRenderer _orangeTurn;

		/*
		[SerializeField] private GameObject _redIndicator;
		[SerializeField] private GameObject _blueIndicator;
		[SerializeField] private GameObject _yellowIndicator;
		[SerializeField] private GameObject _greenIndicator;
		[SerializeField] private GameObject _violetIndicator;
		[SerializeField] private GameObject _orangeIndicator;
		*/
		private GameObject[] _slugs;
	
		private string color; 

		// Use this for initialization
		void Start () {
			_slugs = GameObject.FindGameObjectsWithTag ("Slug");

			// RÄJÄYTIN POIS KOSKA HELATEKSTIEN KANSSA TARPEETON
			/*
			foreach (var slug in _slugs) {
				if (slug.GetComponent<CharacterInfo> ().GetTeam () == 1) {
					Instantiate (_blueIndicator, slug.transform.position + new Vector3(0f,1f,0f), Quaternion.identity, slug.transform);
					slug.GetComponent<CircleCollider2D> ().enabled = true;
				} else if (slug.GetComponent<CharacterInfo> ().GetTeam () == 2) {
					Instantiate (_redIndicator, slug.transform.position + new Vector3(0f,1f,0f), Quaternion.identity, slug.transform);
					slug.GetComponent<CircleCollider2D> ().enabled = false;
				}
			}
			*/
		}
		
		// Update is called once per frame
		void Update () {

			int activeTeam = GameManager.Instance.GetCurrentActiveTeam (); 
			if (activeTeam == 1) {
				color = GameSessionController._instance._teams [0]._teamColor; 
			} else if (activeTeam == 2) {
				color = GameSessionController._instance._teams [1]._teamColor; 
			}else if (activeTeam == 3) {
				color = GameSessionController._instance._teams [2]._teamColor; 
			}else{
				color = GameSessionController._instance._teams [3]._teamColor; 
			}

			Debug.Log (color); 


			if (color == "Red") {
				_redTurn.enabled = true;
				_blueTurn.enabled = false;
				_yellowTurn.enabled = false;
				_greenTurn.enabled = false; 
				_violetTurn.enabled = false;
				_orangeTurn.enabled = false; 
			} else if (color == "Blue") {
				_redTurn.enabled = false;
				_blueTurn.enabled = true;
				_yellowTurn.enabled = false;
				_greenTurn.enabled = false; 
				_violetTurn.enabled = false;
				_orangeTurn.enabled = false; 
			}else if (color == "Yellow") {
				_redTurn.enabled = false;
				_blueTurn.enabled = false;
				_yellowTurn.enabled = true;
				_greenTurn.enabled = false; 
				_violetTurn.enabled = false;
				_orangeTurn.enabled = false; 
			}else if (color == "Green") {
				_redTurn.enabled = false;
				_blueTurn.enabled = false;
				_yellowTurn.enabled = false;
				_greenTurn.enabled = true; 
				_violetTurn.enabled = false;
				_orangeTurn.enabled = false; 
			}else if (color == "Violet") {
				_redTurn.enabled = false;
				_blueTurn.enabled = false;
				_yellowTurn.enabled = false;
				_greenTurn.enabled = false; 
				_violetTurn.enabled = true;
				_orangeTurn.enabled = false; 
			}else if (color == "Orange") {
				_redTurn.enabled = false;
				_blueTurn.enabled = false;
				_yellowTurn.enabled = false;
				_greenTurn.enabled = false; 
				_violetTurn.enabled = false;
				_orangeTurn.enabled = true; 
			}
		}
	}
}