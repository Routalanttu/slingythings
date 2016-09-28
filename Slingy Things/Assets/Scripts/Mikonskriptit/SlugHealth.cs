using UnityEngine;
using System.Collections;

public class SlugHealth : MonoBehaviour {

	public GameObject healthBarContainer;
	public int _slugHealth = 100;
	private Vector3 slugPos;


	// Use this for initialization
	void Start () {

		slugPos = transform.position;
		slugPos.x -= 1.0f;
		slugPos.y += 1.3f;
		healthBarContainer.transform.position = slugPos;

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DecreaseHealth(int damageAmount) {

		_slugHealth -= damageAmount; 

		float hpQuotient = (float)damageAmount / 100; 
		healthBarContainer.transform.localScale -= new Vector3(hpQuotient/3, 0, 0);  //kolmonen jakajana purkkaa vielä!!

		if(_slugHealth <= 0) {
			Die (); 
		}
	}

	public void Die(){
		Destroy (gameObject); 
		GameManager.Instance.GameOver (); 
	}
}
