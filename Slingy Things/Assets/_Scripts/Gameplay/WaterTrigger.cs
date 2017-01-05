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
			
			if (other.gameObject.GetComponent<CharacterInfo> ().GetSpecies () == 0) {
				if (other.gameObject.GetComponent<SlugSlinger>().GetArmedState()) {
					GameManager.Instance.Drowned ();
				}
			}

			if (other.gameObject.GetComponent<CharacterInfo> ().GetSpecies () == 1) {
				if (other.gameObject.GetComponent<SnailSlinger>().GetArmedState()) {
					GameManager.Instance.Drowned ();
				}
			}

			if (other.gameObject.GetComponent<CharacterInfo> ().GetSpecies () == 2) {
				if (other.gameObject.GetComponent<OctoSlinger>().GetArmedState()) {
					GameManager.Instance.Drowned ();
				}
			}

			if (other.gameObject.GetComponent<CharacterInfo> ().GetSpecies () == 3) {
				if (other.gameObject.GetComponent<SiikaSlinger>().GetArmedState()) {
					GameManager.Instance.Drowned ();
				}
			}
			
					
				other.gameObject.GetComponent<CharacterInfo> ().DecreaseHealth (100); 
		}
		
	}
}
}