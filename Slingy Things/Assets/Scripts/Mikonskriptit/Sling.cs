using UnityEngine;
using System.Collections;

public class Sling : MonoBehaviour {

	public float _flySpeed; 
	private Vector3 _slingVector;

	private Rigidbody2D _rigidBody; 
	private Transform _gcTransform; 

	private Vector3 _flyVector; 
	private Vector3 _flyDir; 
	private Vector3 _drag; 
	private Vector3 _gravity; 

	private bool _isInAir; 
	private bool _sling; 


	// Use this for initialization
	void Start () {

		_isInAir = false; 

		_rigidBody = GetComponent<Rigidbody2D> ();
		_gcTransform = GetComponent<Transform> (); 

		_flyDir = new Vector3 (1, 1); 
		_gravity = new Vector3 (0, -0.5f); 
		_drag = new Vector3 (-1, 0); 
		_flySpeed = 0.4f; 

	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.H)) {
			_sling = true; 
		}

		if (!_isInAir && _sling) {
			Launch (); 
		}
	
	}


	private void Launch() {

		_slingVector.y -= Time.deltaTime*2; 
		_gcTransform.position += _slingVector * _flySpeed; 
	}

	void OnCollisionEnter2D(Collision2D col){

		if (col.gameObject.tag == "Ground") {
			_isInAir = false; 
			_sling = false; 
			_flyDir = new Vector3 (1, 1); 

		}
	}

	public void SetSling(Vector3 slingVector, bool sling){

		_slingVector = slingVector; 
		_sling = sling; 
	}


}
