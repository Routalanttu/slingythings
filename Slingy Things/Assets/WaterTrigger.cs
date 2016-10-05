using UnityEngine;
using System.Collections;

public class WaterTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter2D (Collider2D other){
		Destroy (other.gameObject); 
		SoundController.Instance.PlaySoundByIndex (2, other.transform.position); 
		Debug.Log ("hit water"); 
	}
}
