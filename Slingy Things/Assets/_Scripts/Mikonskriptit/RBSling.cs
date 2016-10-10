using UnityEngine;
using System.Collections;

namespace SlingySlugs {
public class RBSling : MonoBehaviour {

	public float _forceAmount; 
	public float _maxPower; 

	public Attack _attack; 

	Vector2 _slingDir; 
	Vector2 _vectorToMouse;
	Vector2 _clampedVectorToMouse; 
	Vector2 _slugPosition;

	bool _fire; 

	private Vector2 _mousePos;
	private Slug _slug; 

	Rigidbody2D _rigidBody; 
	Transform _gcTransform; 



	// Use this for initialization
	void Start () {
	
		_rigidBody = GetComponent<Rigidbody2D> (); 
		_gcTransform = GetComponent<Transform> (); 
		_attack = GetComponent<Attack> (); 
		_slug = GetComponent<Slug> (); 

		if (_maxPower == 0) {
			_maxPower = 2.5f; 
		}
	
	}
	
	// Update is called once per frame
	void Update () {

		_mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition); 
	
	}

	void OnMouseDown(){

		//Set camera target
		GameManager.Instance.SetCameraTarget(gameObject.transform); 
	}

	void OnMouseUp(){

		if (_slug.IsActive) {

			_slugPosition = _gcTransform.position; //have to use the vector2 form for below
			_vectorToMouse = _mousePos - _slugPosition; 

			if (_vectorToMouse.magnitude > _maxPower) {
				_vectorToMouse = Vector2.ClampMagnitude (_vectorToMouse, _maxPower);
			} 

			_rigidBody.AddForce (-_vectorToMouse * _forceAmount, ForceMode2D.Impulse);

			_attack.ArmSlug (); 

		}
			
	}


}
}