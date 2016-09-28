using UnityEngine;
using System.Collections;

public class camFollow : MonoBehaviour {

	public Transform target; 
	public float dampingSpeed; 
	Transform m_transform; 

	// Use this for initialization
	void Start () {

		m_transform = GetComponent<Transform> (); 
	
	}
	
	// Update is called once per frame
	void Update () {
	

		m_transform.position = Vector3.Lerp (m_transform.position, target.position, dampingSpeed) + new Vector3(0,0.8f,-10); 

	}
}
