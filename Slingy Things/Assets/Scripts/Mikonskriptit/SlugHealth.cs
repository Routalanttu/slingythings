using UnityEngine;
using System.Collections;

public class SlugHealth : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DecreaseHealth(float damageAmount){
		//decrease hp
		Debug.Log("Nyt vähennetään" + damageAmount + "hp pistettä"); 
	}
}
