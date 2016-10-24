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
    public Animator mainMenuCredits;



    // Use this for initialization
    void Start () {
        teamsAndAnimals.transform.position = offScreenRight.transform.position;
        menuObjects.transform.position = centerScreen.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnPlay() {
        mainMenuObjects.SetTrigger("Menu2MainMenuObjectsMoveAwayFromScreen");
        mainMenuCredits.SetTrigger("Menu2CreditsMoveToScreen");
    }

    public void OnCreditsReturn() {
        mainMenuObjects.SetTrigger("Menu2MainMenuObjectsMoveAwayFromScreen");
        mainMenuCredits.SetTrigger("Menu2CreditsMoveToScreen");
    }

}
