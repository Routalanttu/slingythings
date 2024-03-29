﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SlingySlugs
{

	public class Menu2 : MonoBehaviour
	{
    
		public Animator _mainMenuObjectsAnim;
		public Animator _teamsAndAnimalsObjectsAnim;
		public Animator _selectLevelObjectsAnim;
		public Animator _titleAnim;
		public Animator _optionsMenuAnim;
		public Animator _teamManagementAnim;

        public Slider loadingBar;
        public GameObject loadingImage;

        private AsyncOperation async;

        //public GameObject numberOfTeamsObjects;

        // Use this for initialization
        void Start ()
		{

		}
	
		// Update is called once per frame
		void Update ()
		{

			if (Input.GetKeyDown(KeyCode.Escape)) { OnQuit(); }
	
		}

		public void OnPlay ()
		{
			_mainMenuObjectsAnim.SetTrigger ("MainMenuMove");
			_teamsAndAnimalsObjectsAnim.SetTrigger ("TeamsAndAnimalsMove");
			_titleAnim.SetTrigger ("TitleMove"); 

		}

		public void OnTeamsAndAnimalsReturn ()
		{
			_mainMenuObjectsAnim.SetTrigger ("MainMenuMove");
			_teamsAndAnimalsObjectsAnim.SetTrigger ("TeamsAndAnimalsMoveBack");
			_titleAnim.SetTrigger ("TitleMove"); 
		}

		public void OnToLevelSelect ()
		{
			_teamsAndAnimalsObjectsAnim.SetTrigger ("TeamsAndAnimalsMove"); 
			_selectLevelObjectsAnim.SetTrigger ("SelectLevelMove"); 

		}

		public void OnLevelSelectReturn ()
		{
			_teamsAndAnimalsObjectsAnim.SetTrigger ("TeamsAndAnimalsMove"); 
			_selectLevelObjectsAnim.SetTrigger ("SelectLevelMove"); 
		}

		public void OnOptions ()
		{
			_mainMenuObjectsAnim.SetTrigger ("MainMenuMove");
			_titleAnim.SetTrigger ("TitleMove"); 
			_optionsMenuAnim.SetTrigger ("OptionsMove"); 

		}

		public void OnOptionsReturn ()
		{
			_mainMenuObjectsAnim.SetTrigger ("MainMenuMove");
			_titleAnim.SetTrigger ("TitleMove"); 
			_optionsMenuAnim.SetTrigger ("OptionsMove"); 
		}

		public void OnTeamManagement ()
		{
			_mainMenuObjectsAnim.SetTrigger ("MainMenuMove");
			_teamManagementAnim.SetTrigger ("TeamManagementMove"); 
			_titleAnim.SetTrigger ("TitleMove"); 

		}

		public void OnTeamManagementReturn ()
		{
			_mainMenuObjectsAnim.SetTrigger ("MainMenuMove");
			_teamManagementAnim.SetTrigger ("TeamManagementMove"); 
			_titleAnim.SetTrigger ("TitleMove"); 

		}

		public void PlayButtonSound ()
		{

			SoundController.Instance.PlaySoundByIndex (0); 

		}

        public void PlayButtonSoundByIndex(int index)
        {

            SoundController.Instance.PlaySoundByIndex(index);

        }

        public void ClickAsync(int level) 
        {
            loadingImage.SetActive(true);
            StartCoroutine(LoadLevelWithBar(level));
        }

        IEnumerator LoadLevelWithBar(int level) 
        {
            async = SceneManager.LoadSceneAsync(level);

            while (!async.isDone) 
            {
                loadingBar.value = async.progress;
                yield return null;
            }
        }

        public void OnQuit ()
		{
			Application.Quit ();
		}


	}
}
