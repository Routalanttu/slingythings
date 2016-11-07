using UnityEngine;
using System.Collections;

public class Menu2 : MonoBehaviour {

    public GameObject menuObjects;
	public GameObject teamsAndAnimals;
	public GameObject selectLevel;
    
    public Animator mainMenuObjects;
    public Animator teamsAndAnimalsObjects;
	public Animator selectLevelObjects; 

	//public GameObject numberOfTeamsObjects;

    // Use this for initialization
    void Start () {

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
