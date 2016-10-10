using UnityEngine;
using System.Collections;

namespace SlingySlugs {
public class SlugHealth : MonoBehaviour {

	public int _slugHealth = 100;
	private Vector3 slugPos;
	private Slug _slug; 

	// Use this for initialization
	void Start () {

		_slug = GetComponent<Slug> (); 

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DecreaseHealth(int damageAmount) {

		_slugHealth -= damageAmount; 
		GameManager.Instance.DecreaseTeamHealth ((int)_slug.teamSelect, damageAmount); 

		float hpQuotient = (float)damageAmount / 100; 

		if(_slugHealth <= 0) {
			Die (); 
		}
	}

	public void Die(){
		int teamNumber = (int) GetComponent<Slug> ().teamSelect; //get the enum as int 
		GameManager.Instance.KillSlug (teamNumber, gameObject); 
		Destroy (gameObject); 
	}

	public int GetHealth () {
		return _slugHealth;
	}
}
}