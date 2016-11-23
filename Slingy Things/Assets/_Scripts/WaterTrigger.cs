using UnityEngine;
using System.Collections;

namespace SlingySlugs {
public class WaterTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter2D (Collider2D other){
		SoundController.Instance.PlaySoundByIndex (2); 

		if (other.gameObject.CompareTag ("Slug")) {
				// TOTAL PLACEHOLDER PLS REMOVE
				if (other.gameObject.GetComponent<Slinger>().GetArmedState()) {
					GameManager.Instance.NextPlayerMove ();
				}

				other.gameObject.GetComponent<CharacterInfo> ().Die ();
				GameManager.Instance.Drowned (); 

		}
		
	}
}
}