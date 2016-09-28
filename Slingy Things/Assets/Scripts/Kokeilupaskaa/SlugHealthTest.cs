using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SlugHealthTest : MonoBehaviour {

    public GameObject healthBarContainer;
    public float slugHealth = 100f;

    private Vector3 slugPos;

    //public GameObject hpBar; 

	// Use this for initialization
	void Start () {
        slugPos = transform.position;
        slugPos.x -= 1.0f;
        slugPos.y += 1.3f;
        healthBarContainer.transform.position = slugPos;

    }
	
	// Update is called once per frame
	void Update () {

	}

    public void DecreaseHealth(float damageAmount) {
        //decrease hp
        healthBarContainer.transform.localScale -= new Vector3(damageAmount, 0, 0);
        Debug.Log("Nyt vähennetään" + damageAmount + "hp pistettä");
        slugHealth -= damageAmount;
        if(slugHealth <= 0) {
            Destroy(gameObject);
        }
    }
}
