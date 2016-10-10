﻿using UnityEngine;
using System.Collections;

namespace SlingySlugs {
public class Attack : MonoBehaviour {

	public float radius = 5.0F;
	public float _explosionForce = 10F;
	public int _explosionDamageMultiplier = 2; 
	public ParticleSystem _explosionPrefab; 
	private Transform _gcTransform; 
	private bool _fire; 
	private bool _armed; 
	private Slug _slug; 

	// Use this for initialization
	void Start () {

		_gcTransform = GetComponent<Transform>(); 
		_slug = GetComponent<Slug> (); 

	}
	
	// Update is called once per frame
	void Update () {

	
	}

	void OnCollisionEnter2D(Collision2D coll){

		if (_armed && _slug.IsActive) {
			Fire(); 
		}

	}

	void Fire(){

		Vector2 explosionPos = _gcTransform.position; 
		ParticleSystem explosion; 
		explosion = Instantiate(_explosionPrefab, _gcTransform.position, Quaternion.identity) as ParticleSystem; 
		explosion.Play(); 
		SoundController.Instance.PlaySoundByIndex (0, _gcTransform.position); 
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, radius);
        foreach (Collider2D hit in colliders) {
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
			Vector2 hitPosition = hit.GetComponent<Transform>().position;
			Vector2 explosionDelta = hitPosition- explosionPos; //Get vector between explosion and hit 
			Vector2 explosionDir = Vector3.Normalize (explosionDelta); //normalize the vector to get direction only 
			float deltaDistance = radius - explosionDelta.magnitude; //get the effective blast magnitude
			int explosionDamage = (int)deltaDistance * _explosionDamageMultiplier; 

			if (rb != null && hit.gameObject.tag == "Slug"){
				rb.AddForce (explosionDir * _explosionForce, ForceMode2D.Impulse);  
				hit.GetComponent<SlugHealth> ().DecreaseHealth (explosionDamage); 

			}
		}
			
		_armed = false; 
		_fire = false; 

		Invoke ("NextPlayerMove", 1); 


	}

	public void ArmSlug(){
		_armed = true; 
	}

	public void NextPlayerMove(){
		GameManager.Instance.NextPlayerMove (); 
	}


}

}