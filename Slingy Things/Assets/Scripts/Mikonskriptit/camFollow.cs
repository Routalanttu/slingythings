using UnityEngine;
using System.Collections;

public class camFollow : MonoBehaviour {

	public float dampingSpeed; 
	public Transform _target; 
	Transform _transform; 

	private float _posX; 
	private float _posY; 
	private float _posZ; 

	// Use this for initialization
	void Start () {

		_transform = GetComponent<Transform> (); 

		_posX = _transform.position.x; 
		_posY = _transform.position.y; 
		_posZ = _transform.position.z; 
	}

	public void SetCameraTarget(Transform target){

		if (target != null) {
			_target = target; 
		}

	}
	
	// Update is called once per frame
	void Update () {
		
		if (_target != null) {
			_transform.position = Vector3.Lerp (_transform.position, _target.position, dampingSpeed) + new Vector3(0,0.0f,-10); 
		}
			
		_posX = _transform.position.x; 
		_posY = _transform.position.y; 
		_posZ = _transform.position.z; 


		if (_posY < -2) {
			_posY = -2; 
		}

		if (_posY > 6 ) {
			_posY = 6; 
		}

		if (_posX < -30) {
			_posX = -30; 
		}

		if (_posX > 30) {
			_posX = 30; 
		}

		_transform.position = new Vector3 (_posX, _posY, _posZ); 
		
	

	}
}
