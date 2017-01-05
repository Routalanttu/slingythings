using UnityEngine;
using System.Collections;

public class DamageText : MonoBehaviour {

	private Transform _gcTransform; 
	private float _posY; 
	private float _posX; 
	private float _posZ; 
	private float _lifeTime; 
	private float _startDelay; 
	private float _liftVelocity; 

	// Use this for initialization
	void Start () {

		_gcTransform = GetComponent<Transform> (); 

		_posY = _gcTransform.position.y; 
		_posX = _gcTransform.position.x;  
		_posZ = 5f; 

		_startDelay = Random.Range (0f, 0.5f); 
		_liftVelocity = Random.Range (6f, 10f); 
	
	}
	
	// Update is called once per frame
	void Update () {

		_lifeTime += Time.deltaTime; 

		if (_lifeTime > _startDelay) {
			_posY += Time.deltaTime*_liftVelocity; 
			_gcTransform.position = new Vector3 (_posX, _posY, _posZ); 
		}

		if (_lifeTime >= 10) {
			Destroy (gameObject); 
		}
	}
}
