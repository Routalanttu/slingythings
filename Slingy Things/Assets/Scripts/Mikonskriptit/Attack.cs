using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

	public float radius = 5.0F;
	public float _explosionForce = 10F;
	public ParticleSystem _explosionPrefab; 

	private Transform _gcTransform; 
	private bool _fire; 

	// Use this for initialization
	void Start () {

		_gcTransform = GetComponent<Transform>(); 
	
	}
	
	// Update is called once per frame
	void Update () {

		
		_fire = Input.GetKeyDown (KeyCode.Space); 

		if(_fire){
			Fire(); 
		}
	
	}

	void Fire(){

		Debug.Log("Nyt Rajaytellaan!!"); 

		Vector2 explosionPos = _gcTransform.position; 
		ParticleSystem explosion; 
		explosion = Instantiate(_explosionPrefab, _gcTransform.position, Quaternion.identity) as ParticleSystem; 
		explosion.Play(); 
		SoundController.instance.PlaySoundByIndex (0, _gcTransform.position); 
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, radius);
        foreach (Collider2D hit in colliders) {
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
			Vector2 hitPosition = hit.GetComponent<Transform>().position;
			Vector2 explosionDir = hitPosition- explosionPos;
			float explosionDamage = explosionDir.magnitude; 
			if (rb != null && hit.gameObject.tag == "Player"){
				rb.AddForce (explosionDir * _explosionForce, ForceMode2D.Impulse); 
				hit.GetComponent<SlugHealth> ().DecreaseHealth (explosionDamage); 
				Debug.Log("PUMM"); 

			}
                
		}

	}
}
