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
            distance = (explosion.transform.position - objects[i].transform.position).magnitude;
            if (distance < explosionRadius) {
                Debug.Log("Close enough");
            }
            if (distance < explosionRadius - 10f) {
                Debug.Log("Close enough - 10f");
            }
            if (distance < explosionRadius - 20f) {
                Debug.Log("Close enough - 20f");
            }
            if (distance < explosionRadius - 30f) {
                Debug.Log("Close enough - 30f");
            }
            if (distance < explosionRadius - 40f) {
                Debug.Log("Close enough - 40f");
            }
            if (distance < explosionRadius - 50f) {
                Debug.Log("Close enough - 50f");
            }
        }
	}
}
