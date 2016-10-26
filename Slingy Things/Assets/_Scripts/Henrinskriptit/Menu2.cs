using UnityEngine;
using System.Collections;

public class Menu2 : MonoBehaviour {

    public GameObject menuObjects;
    //public GameObject numberOfTeamsObjects;
    public GameObject teamsAndAnimals;
    public GameObject selectLevel;
    public Transform centerScreen;
    public Transform offScreenRight;
    public Transform offScreenLeft;
    public Animator mainMenuObjects;
    public Animator teamsAndAnimalsObjects;



    // Use this for initialization
    void Start () {
        teamsAndAnimals.transform.position = offScreenRight.transform.position;
        menuObjects.transform.position = centerScreen.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnPlay() {
        mainMenuObjects.SetTrigger("MainMenuMove");
        teamsAndAnimalsObjects.SetTrigger("TeamsAndAnimalsMove");
    }

    public void OnCreditsReturn() {
        mainMenuObjects.SetTrigger("MainMenuMove");
        teamsAndAnimalsObjects.SetTrigger("TeamsAndAnimalsMove");
    }

}
