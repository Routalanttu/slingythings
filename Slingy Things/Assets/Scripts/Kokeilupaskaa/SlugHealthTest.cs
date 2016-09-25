using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SlugHealthTest : MonoBehaviour {

    public GameObject healthBarContainer;

    private Vector3 slugPos;

	// Use this for initialization
	void Start () {
        slugPos = transform.position;
        slugPos.x -= 1.0f;
        slugPos.y += 1.3f;
        healthBarContainer.transform.position = slugPos;
        // transform.localScale -= new Vector3(0.10f, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {

	}
}
