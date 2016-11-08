using UnityEngine;
using System.Collections;

public class Menu2 : MonoBehaviour {
    
    public Animator _mainMenuObjectsAnim;
    public Animator _teamsAndAnimalsObjectsAnim;
	public Animator _selectLevelObjectsAnim; 
	public Animator _titleAnim; 
	public Animator _optionsMenuAnim; 

	//public GameObject numberOfTeamsObjects;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnPlay() {
        _mainMenuObjectsAnim.SetTrigger("MainMenuMove");
        _teamsAndAnimalsObjectsAnim.SetTrigger("TeamsAndAnimalsMove");
		_titleAnim.SetTrigger ("TitleMove"); 

    }

    public void OnTeamsAndAnimalsReturn() {
        _mainMenuObjectsAnim.SetTrigger("MainMenuMove");
        _teamsAndAnimalsObjectsAnim.SetTrigger("TeamsAndAnimalsMoveBack");
		_titleAnim.SetTrigger ("TitleMove"); 
    }

	public void OnToLevelSelect(){
		_teamsAndAnimalsObjectsAnim.SetTrigger ("TeamsAndAnimalsMove"); 
		_selectLevelObjectsAnim.SetTrigger ("SelectLevelMove"); 

	}

	public void OnLevelSelectReturn(){
		_teamsAndAnimalsObjectsAnim.SetTrigger ("TeamsAndAnimalsMove"); 
		_selectLevelObjectsAnim.SetTrigger ("SelectLevelMove"); 
	}

    public void OnOptions() {
		_mainMenuObjectsAnim.SetTrigger("MainMenuMove");
		_titleAnim.SetTrigger ("TitleMove"); 
		_optionsMenuAnim.SetTrigger ("OptionsMove"); 

    }

    public void OnOptionsReturn() {
		_mainMenuObjectsAnim.SetTrigger("MainMenuMove");
		_titleAnim.SetTrigger ("TitleMove"); 
		_optionsMenuAnim.SetTrigger ("OptionsMove"); 
    }

	public void PlayButtonSound(){

		SoundController.Instance.PlaySoundByIndex (0); 

	}

}
