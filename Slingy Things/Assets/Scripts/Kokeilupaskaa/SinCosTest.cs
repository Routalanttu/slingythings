using UnityEngine;
using System.Collections;

public class SinCosTest : MonoBehaviour {

	private Transform _transform;

	[SerializeField]private Transform _otherObject;

	private float _angle;
	[SerializeField]private float _speed;
	[SerializeField]private float _distance;

	// Use this for initialization
	void Awake () {
		_transform = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 cubePos = new Vector3 (Mathf.Sin (_angle) * _distance, Mathf.Cos (_angle)*_distance, 0.0f);
		_transform.localPosition = cubePos;
		_otherObject.localPosition = cubePos * -1f;

		Vector3 wutNigga = _transform.position - _transform.parent.position;

		_angle += _speed * Time.deltaTime;

		Debug.Log (_distance + " = " + wutNigga.magnitude + "?");
		Debug.Log ("Sin: " + Mathf.Sin (_angle) + ", Cos: " + Mathf.Cos (_angle));
	}
}
