using UnityEngine;
using System.Collections;

public class ExplosionTest : MonoBehaviour {

    public Transform explosion;
    float distance;
    float explosionRadius = 100f;
    float damage = 50f;

    GameObject[] objects;

    // Use this for initialization
    void Start () {
	    objects = GameObject.FindGameObjectsWithTag("slug");
    }
	
	// Update is called once per frame
	void Update () {
	    for (int i = 0; i < objects.Length; i++)
        {
            distance = Vector3.Distance(explosion.transform.position, objects[i].transform.position);
            if (distance < explosionRadius)
            {
                Debug.Log("Close enough");
            }
        }
	}
}
