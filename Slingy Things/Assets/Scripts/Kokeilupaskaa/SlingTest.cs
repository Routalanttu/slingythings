using UnityEngine;
using System.Collections;

public class SlingTest : MonoBehaviour {

	public float _maxStretch = 2.0f; 

	//private SpringJoint2D _spring;
	[SerializeField]private Transform _stretchPoint;
	//private Rigidbody2D _rigidBody;
	private Camera _camera;
	private Ray _rayToMouse;
	private Vector2 _prevVelocity;
	private float _maxStretchSqr;
	private bool clickedOn;

	private GameObject _idleTail;
	private GameObject _stretchTail;

	void Awake(){

	}

	// Use this for initialization
	void Start () {
		_rayToMouse = new Ray (_stretchPoint.position, Vector3.zero); 
		_maxStretchSqr = _maxStretch * _maxStretch;  //comparing squared magnitudes is faster 

		_idleTail = transform.parent.FindChild ("LieroLowerBody").gameObject;
		_stretchTail = transform.parent.FindChild ("LieroStretch4").gameObject;
	}

	// Update is called once per frame
	void Update () {

		if (clickedOn) {
			Dragging (); 
			_idleTail.GetComponent<SpriteRenderer> ().enabled = false;
			_stretchTail.GetComponent<SpriteRenderer> ().enabled = true;
		} else {
			transform.localPosition = new Vector3 (0f, 0f, 0f);
			_idleTail.GetComponent<SpriteRenderer> ().enabled = true;
			_stretchTail.GetComponent<SpriteRenderer> ().enabled = false;
		}


	}

	void OnMouseDown(){
		clickedOn = true; 
	}

	void OnMouseUp(){
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


		Vector3 vectorToTarget = transform.parent.position - transform.position;
		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		_stretchTail.transform.rotation = q;

		_stretchTail.transform.localScale = new Vector3 (vectorToTarget.magnitude / 1.25f, 1f, 1f);

	}


}
