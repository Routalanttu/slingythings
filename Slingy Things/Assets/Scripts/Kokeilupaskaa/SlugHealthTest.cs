using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SlugHealthTest : MonoBehaviour {

    public Vector3 hpBar;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        transform.localScale = hpBar;
	}
}
