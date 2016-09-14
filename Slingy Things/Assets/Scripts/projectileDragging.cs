using UnityEngine;
using System.Collections;

public class projectileDragging : MonoBehaviour {

	public float _maxStretch = 3.0f; 

	private SpringJoint2D _spring; 
	private Transform _stretchPoint; 
	private Rigidbody2D _rigidBody; 
	private Camera _camera; 
	private Ray _rayToMouse; 
	private Vector2 _prevVelocity; 
	private float _maxStretchSqr;
	private bool clickedOn; 


	void Awake(){

		_spring = GetComponent<SpringJoint2D> (); 
		_stretchPoint = _spring.connectedBody.transform;

	}

	// Use this for initialization
	void Start () {

		_rigidBody = GetComponent<Rigidbody2D> (); 
		_rayToMouse = new Ray (_stretchPoint.position, Vector3.zero); 
		_maxStretchSqr = _maxStretch * _maxStretch;  //comparing squared magnitudes is faster 
	}
	
	// Update is called once per frame
	void Update () {

		if (clickedOn) {
			Dragging (); 
		}

		if (_spring != null) {
			if (!_rigidBody.isKinematic && _prevVelocity.sqrMagnitude > _rigidBody.velocity.sqrMagnitude) {
				Destroy (_spring);
				_rigidBody.velocity = _prevVelocity;
			}

			if (!clickedOn) {
				_prevVelocity = _rigidBody.velocity;
			}
		}
			
	
	}
		
	void OnMouseDown(){
		_spring.enabled = false; 
		clickedOn = true; 
	}

	void OnMouseUp(){
		_spring.enabled = true; 
		_rigidBody.isKinematic = false; 
		clickedOn = false; 
	}

	void Dragging(){
		Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition); 

		//When used for mobile: 
		//Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);﻿


		Vector2 stretchPointToMouse = mouseWorldPoint - _stretchPoint.position;

		if (stretchPointToMouse.sqrMagnitude > _maxStretchSqr) {
			_rayToMouse.direction = stretchPointToMouse;
			mouseWorldPoint = _rayToMouse.GetPoint (_maxStretch);
		}

		mouseWorldPoint.z = 0f; 
		transform.position = mouseWorldPoint; 
	}


}
