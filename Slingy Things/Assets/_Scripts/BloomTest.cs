using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class BloomTest : MonoBehaviour {

	public GameObject MainCam;
	public float _minimum = 0F;
	public float _maximum =  1.0F;
	float t = 0.0f;
	bool _bloom; 

	// Use this for initialization
	void Start () {

		MainCam.GetComponent<BloomOptimized> ().intensity = 0;
	
	}
	
	// Update is called once per frame
	void Update () {

		MainCam.GetComponent<BloomOptimized> ().intensity = Mathf.Lerp (_minimum, _maximum, t); 
		t += 0.5f * Time.deltaTime;

		if (t > 1.0f){
			float temp = _maximum;
			_maximum = _minimum;
			_minimum = temp;
			t = 0.0f;
		}
	
	}
}
