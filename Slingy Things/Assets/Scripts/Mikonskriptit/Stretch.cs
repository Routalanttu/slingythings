using UnityEngine;
using System.Collections;

public class Stretch : MonoBehaviour {

	public float _maxStretch;
	public Vector3 _slingVector; 
	public Sling _sling; 

	private Vector3 _vectorToTarget; 

	[SerializeField]private Transform _stretchPoint;

	private Camera _camera;
	private Ray2D _rayToMouse;
	private Vector2 _prevVelocity;
	private bool clickedOn;

	private GameObject _idleTail;
	private GameObject _stretchTail;
	private GameObject _upperBody;

	private Transform _counterPiece;

	[SerializeField]private Transform _blackEye1;
	[SerializeField]private Transform _blackEye2;
	[SerializeField]private Transform _eye1JumpPoint;
	[SerializeField]private Transform _eye2JumpPoint;

	private Vector3 _eye1OrigPos;
	private Vector3 _eye2OrigPos;


	void Awake(){

	}

	// Use this for initialization
	void Start () {

		_sling = _sling.GetComponent<Sling> (); 

		_rayToMouse = new Ray2D (_stretchPoint.position, Vector3.zero); 

		_idleTail = transform.parent.FindChild ("LieroLowerBody").gameObject;
		_stretchTail = transform.parent.FindChild ("LieroStretch4").gameObject;
		_upperBody = transform.parent.FindChild ("LieroUpperBody").gameObject;
		_counterPiece = transform.parent.FindChild ("LieroCounterPiece");
		//_blackEye1 = transform.parent.FindChild ("BlackEye1");
		//_blackEye2 = transform.parent.FindChild ("BlackEye2");

		_eye1OrigPos = _blackEye1.parent.position;
		_eye2OrigPos = _blackEye2.parent.position;
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
		SetSling ();
	}

	void Dragging(){
		//calculate mouse position in the world 
		Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition); 

		//mobile: 
		//Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);﻿

		//calculate the difference between mousepoint and stretchpoint 
		Vector2 stretchPointToMouse = mouseWorldPoint - _stretchPoint.position;

		Debug.Log ("stretchpointtomouse " + stretchPointToMouse.magnitude); 

		Debug.Log ("mouseworldpointbefore " + mouseWorldPoint); 


		if (stretchPointToMouse.sqrMagnitude > (_maxStretch*_maxStretch)) {
			_rayToMouse.direction = stretchPointToMouse;
			mouseWorldPoint = _rayToMouse.GetPoint (_maxStretch);
		}

		Debug.Log ("mouseworldpointafter " + mouseWorldPoint); 


		mouseWorldPoint.z = 0f; 
		transform.position = mouseWorldPoint; 

		//_vectorToTarget = transform.parent.position - transform.position;
		_vectorToTarget = _stretchPoint.position - transform.position;

		_slingVector = _vectorToTarget; //for sling script to use 

		Stretching (); 

	}

	void SetSling(){
		_sling.SetSling (_slingVector, true); 
	}

	void Stretching(){

		float angle = Mathf.Atan2(_vectorToTarget.y, _vectorToTarget.x) * Mathf.Rad2Deg;
		float angleInRad = Mathf.Atan2 (_vectorToTarget.y, _vectorToTarget.x);
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		_stretchTail.transform.rotation = q;
		_counterPiece.transform.rotation = q;

		_blackEye1.localPosition = new Vector3 (Mathf.Sin (angleInRad+1.5708f) * 0.02f, Mathf.Cos (angleInRad+1.5708f)*-0.02f, 0.0f);
		_blackEye2.localPosition = new Vector3 (Mathf.Sin (angleInRad+1.5708f) * 0.02f, Mathf.Cos (angleInRad+1.5708f)*-0.02f, 0.0f);

		if (_vectorToTarget.magnitude > 0.15f) {
			_stretchTail.transform.localScale = new Vector3 (_vectorToTarget.magnitude / 1.25f, 1f, 1f);
		} else {
			_stretchTail.transform.localScale = new Vector3 (0.15f / 1.25f, 1f, 1f);
		}



		if (transform.localPosition.x > 0f) {
			_upperBody.GetComponent<SpriteRenderer> ().flipX = true;
			_idleTail.GetComponent<SpriteRenderer> ().flipX = true;

		//	_blackEye1.parent.position = _eye1JumpPoint.position;
		//	_blackEye2.parent.position = _eye2JumpPoint.position;
		} else if (transform.localPosition.x < 0f) {
			_upperBody.GetComponent<SpriteRenderer> ().flipX = false;
			_idleTail.GetComponent<SpriteRenderer> ().flipX = false;

			_blackEye1.parent.position = _eye1OrigPos;
			_blackEye2.parent.position = _eye2OrigPos;
		}
			

	}


}
