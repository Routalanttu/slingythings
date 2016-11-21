using UnityEngine;
using System.Collections;

namespace SlingySlugs{

public class Water : MonoBehaviour {

	private Transform _gcTransform; 
	private float _startPosX; 
	private float _posX; 
	private float _posY; 

	// Use this for initialization
	void Start () {

		_gcTransform = GetComponent<Transform> (); 
		_startPosX = _gcTransform.position.x; 

		_posX = _startPosX; 
		_posY = _gcTransform.position.y; 
	
	}
	
	// Update is called once per frame
	void Update () {

		_posX += Time.deltaTime; 

		_gcTransform.position = new Vector2 (_posX, _posY); 

		if (_posX > 80) {
			_posX = _startPosX; 
		}
	
	}
}
}
