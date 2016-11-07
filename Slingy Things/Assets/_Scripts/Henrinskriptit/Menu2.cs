using UnityEngine;
using System.Collections;

public class Menu2 : MonoBehaviour {

    public GameObject menuObjects;
	public GameObject teamsAndAnimals;
	public GameObject selectLevel;
    
    public Animator mainMenuObjects;
    public Animator teamsAndAnimalsObjects;
	public Animator selectLevelObjects; 

	//tarviiko?
	//public Transform centerScreen;
	//public Transform offScreenRight;
	//public Transform offScreenLeft;

	//public GameObject numberOfTeamsObjects;



    // Use this for initialization
    void Start () {

		//tarviiko?
       // teamsAndAnimals.transform.position = offScreenRight.transform.position;
       // menuObjects.transform.position = centerScreen.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnPlay() {
        mainMenuObjects.SetTrigger("MainMenuMove");
        teamsAndAnimalsObjects.SetTrigger("TeamsAndAnimalsMove");
    }

    public void OnTeamsAndAnimalsReturn() {
        mainMenuObjects.SetTrigger("MainMenuMove");
        teamsAndAnimalsObjects.SetTrigger("TeamsAndAnimalsMoveBack");
    }

	public void OnToLevelSelect(){
		teamsAndAnimalsObjects.SetTrigger ("TeamsAndAnimalsMove"); 
		selectLevelObjects.SetTrigger ("SelectLevelMove"); 

	}

	public void OnLevelSelectReturn(){
		teamsAndAnimalsObjects.SetTrigger ("TeamsAndAnimalsMove"); 
		selectLevelObjects.SetTrigger ("SelectLevelMove"); 
	}

    public void OnCredits() {

    }

    public void OnCreditsReturn() {

    }

}
