using UnityEngine;
using System.Collections;

public class PyryCamFollow : MonoBehaviour {

	[SerializeField] private Transform _nigger;
	private Transform _myTransform;

	// Use this for initialization
	void Awake () {
		_myTransform = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		_myTransform.position = new Vector3 (_nigger.position.x, _myTransform.position.y, _myTransform.position.z);
	}
}
