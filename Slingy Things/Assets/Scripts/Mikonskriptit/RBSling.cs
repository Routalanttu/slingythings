using UnityEngine;
using System.Collections;

public class RBSling : MonoBehaviour {

	public float _forceAmount; 
	public float _maxPower; 

	Vector2 _slingDir; 
	Vector2 _vectorToMouse;
	Vector2 _clampedVectorToMouse; 
	Vector2 _slugPosition;

	bool _fire; 

	private Vector2 _mousePos;

	Rigidbody2D _rigidBody; 
	Transform _gcTransform; 

	// Use this for initialization
	void Start () {
	
		_rigidBody = GetComponent<Rigidbody2D> (); 
		_gcTransform = GetComponent<Transform> (); 

		if (_maxPower == 0) {
			_maxPower = 2.5f; 
		}
	
	}
	
	// Update is called once per frame
	void Update () {

		_fire = Input.GetKeyDown (KeyCode.Mouse0); 
		_mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition); 


		if (_fire) {
			_slingDir = _mousePos - _slugPosition; 
		}
			
	
	}

	void OnMouseDown(){

		//Do we need this?
	}

	void OnMouseUp(){

		_slugPosition = _gcTransform.position; //have to use the vector2 form for below
		_vectorToMouse = _mousePos - _slugPosition; 

		if (_vectorToMouse.magnitude > _maxPower) {
			_vectorToMouse = Vector2.ClampMagnitude (_vectorToMouse, _maxPower);
		} 

		_rigidBody.AddForce (-_vectorToMouse * _forceAmount, ForceMode2D.Impulse);

	}


}
