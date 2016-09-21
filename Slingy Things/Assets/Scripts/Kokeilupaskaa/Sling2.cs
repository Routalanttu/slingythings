﻿using UnityEngine;
using System.Collections;

public class Sling2 : MonoBehaviour {

	public float _maxStretch = 2.0f; 

	//private SpringJoint2D _spring;
	[SerializeField]private Transform _stretchPoint;
	//private Rigidbody2D _rigidBody;
	private Camera _camera;
	private Ray _rayToMouse;
	private Vector2 _prevVelocity;
	private float _maxStretchSqr;
	private bool clickedOn;

	private bool justLetGo = false;

	private GameObject _idleTail;
	private GameObject _stretchTail;
	private GameObject _upperBody;

	private Transform _counterPiece;

	[SerializeField]private AudioClip squish;

	private AudioSource audio;

	private bool _thrown;


	[SerializeField]private SpriteRenderer _arrowOne;
	[SerializeField]private SpriteRenderer _arrowTwo;
	[SerializeField]private SpriteRenderer _arrowThree;
	[SerializeField]private SpriteRenderer _arrowFour;
	[SerializeField]private SpriteRenderer _arrowFive;

	//[SerializeField]private Transform _blackEye1;
	//[SerializeField]private Transform _blackEye2;
	//[SerializeField]private Transform _eye1JumpPoint;
	//[SerializeField]private Transform _eye2JumpPoint;

	//private Vector3 _eye1OrigPos;
	//private Vector3 _eye2OrigPos;


	void Awake(){
		audio = GetComponent<AudioSource> ();
	}

	// Use this for initialization
	void Start () {
		_rayToMouse = new Ray (_stretchPoint.position, Vector3.zero); 
		_maxStretchSqr = _maxStretch * _maxStretch;  //comparing squared magnitudes is faster 

		_idleTail = transform.parent.FindChild ("slugTailIdle").gameObject;
		_stretchTail = transform.parent.FindChild ("slugTailStretch2").gameObject;
		_upperBody = transform.parent.FindChild ("slugHead").gameObject;
		_counterPiece = transform.parent.FindChild ("slugCounterPiece2");
		//_blackEye1 = transform.parent.FindChild ("BlackEye1");
		//_blackEye2 = transform.parent.FindChild ("BlackEye2");

		//_eye1OrigPos = _blackEye1.parent.position;
		//_eye2OrigPos = _blackEye2.parent.position;
	}

	// Update is called once per frame
	void Update () {

		if (clickedOn) {
			Dragging (); 
			_idleTail.GetComponent<SpriteRenderer> ().enabled = false;
			_stretchTail.GetComponent<SpriteRenderer> ().enabled = true;
			_counterPiece.GetComponent<SpriteRenderer> ().enabled = true;
		} else {
			transform.localPosition = new Vector3 (0f, 0f, 0f);
			_idleTail.GetComponent<SpriteRenderer> ().enabled = true;
			_stretchTail.GetComponent<SpriteRenderer> ().enabled = false;
			_counterPiece.GetComponent<SpriteRenderer> ().enabled = false;
			if (_thrown) {
				
			}
		}


	}

	void OnMouseDown(){
		clickedOn = true; 
		justLetGo = true;
	}

	void OnMouseUp(){
		clickedOn = false; 
		//audio.Play ();
		if (justLetGo) {
			audio.PlayOneShot (squish, 0.5f);
			Debug.Log ("Fuck you");
			ThrowTheFucker ();
		}
		justLetGo = false;
		HideAllArrows ();
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
		float angleInRad = Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x);
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		_stretchTail.transform.rotation = q;
		_counterPiece.transform.rotation = q;

		//_blackEye1.localPosition = new Vector3 (Mathf.Sin (angleInRad+1.5708f) * 0.02f, Mathf.Cos (angleInRad+1.5708f)*-0.02f, 0.0f);
		//_blackEye2.localPosition = new Vector3 (Mathf.Sin (angleInRad+1.5708f) * 0.02f, Mathf.Cos (angleInRad+1.5708f)*-0.02f, 0.0f);

		if (vectorToTarget.magnitude > 0.8f) {
			_stretchTail.transform.localScale = new Vector3 (vectorToTarget.magnitude / 1.25f, 1f, 1f);
			if (vectorToTarget.magnitude > 1f) {
				_arrowOne.enabled = true;
				Color tmp = _arrowOne.color;
				tmp.a = (vectorToTarget.magnitude - 1f)*5f;
				_arrowOne.color = tmp;
			} else {
				_arrowOne.enabled = false;
			}
			if (vectorToTarget.magnitude > 1.2f) {
				_arrowTwo.enabled = true;
				Color tmp = _arrowTwo.color;
				tmp.a = (vectorToTarget.magnitude - 1.2f)*5f;
				_arrowTwo.color = tmp;
			} else {
				_arrowTwo.enabled = false;
			}
			if (vectorToTarget.magnitude > 1.4f) {
				_arrowThree.enabled = true;
				Color tmp = _arrowThree.color;
				tmp.a = (vectorToTarget.magnitude - 1.4f)*5f;
				_arrowThree.color = tmp;
			} else {
				_arrowThree.enabled = false;
			}
			if (vectorToTarget.magnitude > 1.6f) {
				_arrowFour.enabled = true;
				Color tmp = _arrowFour.color;
				tmp.a = (vectorToTarget.magnitude - 1.6f)*5f;
				_arrowFour.color = tmp;
			} else {
				_arrowFour.enabled = false;
			}
			if (vectorToTarget.magnitude > 1.8f) {
				_arrowFive.enabled = true;
				Color tmp = _arrowFive.color;
				tmp.a = (vectorToTarget.magnitude - 1.8f)*5f;
				_arrowFive.color = tmp;
			} else {
				_arrowFive.enabled = false;
			}
		} else {
			_stretchTail.transform.localScale = new Vector3 (0.8f / 1.25f, 1f, 1f);
			HideAllArrows ();
		}

		Debug.Log (vectorToTarget.magnitude);


		if (transform.localPosition.x > 0f) {
			_upperBody.GetComponent<SpriteRenderer> ().flipX = true;
			_idleTail.GetComponent<SpriteRenderer> ().flipX = true;

			//_blackEye1.parent.position = _eye1JumpPoint.position;
			//_blackEye2.parent.position = _eye2JumpPoint.position;
		} else if (transform.localPosition.x < 0f) {
			_upperBody.GetComponent<SpriteRenderer> ().flipX = false;
			_idleTail.GetComponent<SpriteRenderer> ().flipX = false;

			//_blackEye1.parent.position = _eye1OrigPos;
			//_blackEye2.parent.position = _eye2OrigPos;
		}

	}

	private void HideAllArrows() {
		_arrowOne.enabled = false;
		_arrowTwo.enabled = false;
		_arrowThree.enabled = false;
		_arrowFour.enabled = false;
		_arrowFive.enabled = false;
	}

	private void ThrowTheFucker(float xPower, float yPower) {
		Debug.Log ("Throw");
		_thrown = true;
	}

	private void beMidAir() {

	}


}
