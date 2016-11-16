using UnityEngine;
using System.Collections;

namespace SlingySlugs {
	public class TempTurnVisualizer : MonoBehaviour {

		[SerializeField] private SpriteRenderer _blueTurn;
		[SerializeField] private SpriteRenderer _redTurn;
		[SerializeField] private GameObject _blueIndicator;
		[SerializeField] private GameObject _redIndicator;
		private GameObject[] _slugs;

		// Use this for initialization
		void Start () {
			_slugs = GameObject.FindGameObjectsWithTag ("Slug");

			foreach (var slug in _slugs) {
				if (slug.GetComponent<CharacterInfo> ().GetTeam () == 1) {
					Instantiate (_blueIndicator, slug.transform.position + new Vector3(0f,1f,0f), Quaternion.identity, slug.transform);
					slug.GetComponent<CircleCollider2D> ().enabled = true;
				} else if (slug.GetComponent<CharacterInfo> ().GetTeam () == 2) {
					Instantiate (_redIndicator, slug.transform.position + new Vector3(0f,1f,0f), Quaternion.identity, slug.transform);
					slug.GetComponent<CircleCollider2D> ().enabled = false;
				}
			}
		}
		
		// Update is called once per frame
		void Update () {
			if (GameManager.Instance.GetCurrentActiveTeam () == 1) {
				_blueTurn.enabled = true;
				_redTurn.enabled = false;
			} else {
				_blueTurn.enabled = false;
				_redTurn.enabled = true;
			}
		}
	}
}