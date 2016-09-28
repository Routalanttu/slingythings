using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private static GameManager _instance; 
	private static bool _isQuitting = false;

	public static GameManager Instance {
		get {
			if (_instance == null && !_isQuitting) {
				_instance = new GameManager ();  //This is probably not necessary as we set the GameManager in Awake
			}
			return _instance; 
		}
	}

	private GUIManager _guiManager; 

	public GUIManager GUIManager
	{
		get
		{
			if(_guiManager == null)
			{
				_guiManager = FindObjectOfType<GUIManager>(); 
			}

			return _guiManager; 
		}
	}

	private bool _paused = false; 

	void Awake() {

		if (_instance == null) {
			_instance = this; 
		} else if (_instance != this) {
			Destroy (this); 
		}
			
	}
		
	void Start () {
	
	}

	void Update () {

		UserInput (); 
	
	}

	void UserInput(){

		if (Input.GetKey (KeyCode.G)) {
			GUIManager.GameOver (); 
		}

		if (Input.GetKeyDown (KeyCode.P)) {
			Pause (); 
		} 
	}

	public void Pause(){

		if (!_paused) {
			Time.timeScale = 0; 
			_paused = true; 
			GUIManager.Paused (); 
		} else {
			Time.timeScale = 1; 
			_paused = false; 
			GUIManager.HideMessage (); 
		}
	}

	private void OnApplicationQuit(){
		_isQuitting = true; 
	}

	public void GameOver(){
		GUIManager.GameOver (); 
		Pause (); 
	}
}
