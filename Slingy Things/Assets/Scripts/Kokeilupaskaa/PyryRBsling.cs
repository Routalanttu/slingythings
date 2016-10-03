using UnityEngine;
using System.Collections;

public class PyryRBsling : MonoBehaviour {

	public float _forceAmount; 
	public float _maxPower; 

	public Attack _attack; 

	Vector2 _slingDir; 
	Vector2 _vectorToMouse;
	Vector2 _clampedVectorToMouse; 
	Vector2 _slugPosition;

	bool _fire;

	private Vector2 _mousePos;

	Rigidbody2D _rigidBody; 
	Transform _gcTransform; 

	private bool _flung;

	//Pyry
	[SerializeField] private Transform _flyTrans;
	private SpriteRenderer _flyAnim;
	[SerializeField] private SpriteRenderer _idleAnim;
	[SerializeField] private SpriteRenderer _idleTail;

	// Use this for initialization
	void Start () {

		_rigidBody = GetComponent<Rigidbody2D> (); 
		_gcTransform = GetComponent<Transform> (); 

		if (_maxPower == 0) {
			_maxPower = 2.5f; 
		}

		_flyAnim = _flyTrans.GetComponent<SpriteRenderer> ();
		_flyAnim.enabled = false;

	}

	// Update is called once per frame
	void Update () {


		_mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition); 


		//Pyry
		if (_flung) {
			Vector2 v = _rigidBody.velocity;
			float angle = Mathf.Atan2 (v.y, v.x) * Mathf.Rad2Deg;
			_flyTrans.localRotation = Quaternion.AngleAxis (angle, Vector3.forward);
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

		_flyAnim.enabled = true;
		_idleAnim.enabled = false;
		_idleTail.enabled = false;
		_flung = true;

	}


}
