using UnityEngine;
using System.Collections;

namespace SlingySlugs {
public class WaterTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter2D (Collider2D other){
		Destroy (other.gameObject); 
		SoundController.Instance.PlaySoundByIndex (2); 

		if (other.gameObject.CompareTag ("Slug")) {
				other.gameObject.GetComponent<CharacterInfo> ().DecreaseHealth (100);

				// TOTAL PLACEHOLDER PLS REMOVE
				if (other.gameObject.GetComponent<Slinger>().GetArmedState()) {
					GameManager.Instance.NextPlayerMove ();
				}
		}
		
	}
}
}