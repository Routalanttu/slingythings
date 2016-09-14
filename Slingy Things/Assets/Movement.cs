using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	Transform m_transform; 
	public float speed; 


	// Use this for initialization
	void Start () {
	
		m_transform = GetComponent<Transform> (); 
	}
	
	// Update is called once per frame
	void Update () {

		float horz = Input.GetAxis("Horizontal"); 
		float verz = Input.GetAxis("Vertical"); 

		m_transform.position += new Vector3 (horz*speed*Time.deltaTime, verz*speed*Time.deltaTime, 0); 
	
	}


}
