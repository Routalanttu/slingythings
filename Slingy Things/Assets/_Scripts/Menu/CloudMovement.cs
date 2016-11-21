using UnityEngine;
using System.Collections;

namespace SlingySlugs{

public class CloudMovement : MonoBehaviour {

	private float _posX;
	private float _posY;
	private Transform _gcTransform; 

	// Use this for initialization
	void Start () {

		_gcTransform = GetComponent<Transform> (); 
		_posX = _gcTransform.position.x; 
		_posY = _gcTransform.position.y; 

	}
	
	// Update is called once per frame
	void Update () {

		_posX -= Time.deltaTime; 

		if (_posX < -17f) {
			_posX = 23f; 
		}

		_gcTransform.position = new Vector2(_posX, _posY); 

	
	}
}
}
