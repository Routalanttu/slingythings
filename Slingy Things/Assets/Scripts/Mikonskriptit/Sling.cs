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

		_flyDir = Vector3.zero; 
		_gravity = new Vector3 (0, -0.5f); 
		_drag = new Vector3 (-1, 0); 
		_flySpeed = 0.4f; 

	
	}
	
	// Update is called once per frame
	void Update () {

		if (!_isInAir && _sling) {
			Launch (); 
		}


		if (Input.GetKey (KeyCode.Space)) {
			//_rigidBody.AddForce (Vector2.up * 10); 
		}
	
	}

	void FixedUpdate(){
		RaycastHit2D hit=Physics2D.Raycast(transform.position,-Vector3.up,Mathf.Infinity);

		if(hit.collider.name=="ground"){
			_isInAir = false; 
			_sling = false; 
			_flyDir = Vector3.zero;  
			Debug.Log ("ray osu maahan"); 
		}
	}


	private void Launch() {

		_slingVector.y -= Time.deltaTime*2; 
		_gcTransform.position += _slingVector * _flySpeed; 
	}

//	void OnCollisionEnter2D(Collision2D col){
//
//		Debug.Log ("joku collision"); 
//
//		if (col.gameObject.tag == "Ground") {
//			_isInAir = false; 
//			_sling = false; 
//			_flyDir = Vector3.zero; 
//
//			Debug.Log ("osu maahan"); 
//
//		}
//
//	}
//
//	void OnTriggerEnter2D(Collider2D other){
//		Debug.Log ("tuleeko triggeri"); 
//
//		_isInAir = false; 
//		_sling = false; 
//		_flyDir = Vector3.zero; 
//
//		Debug.Log ("osu maahan"); 
//	}

	public void SetSling(Vector3 slingVector, bool sling){

		_slingVector = slingVector; 
		_sling = sling; 
	}


}
