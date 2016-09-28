using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class GUIManager : MonoBehaviour {

	public Text _message; 
	public Text _blueTeamMessage; 
	public Text _redTeamMessage; 

	void Awake(){

		if (_message == null) {
			Debug.LogError ("GUIManager - _message missing"); 
		}

		if (_message == null) {
			Debug.LogError ("GUIManager - _blueTeamMessage missing"); 
		}

		if (_message == null) {
			Debug.LogError ("GUIManager - _redTeamMessage missing"); 
		}

		HideMessage (); 

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowMessage(string message){
		_message.text = message; 
		_message.gameObject.SetActive (true); 
	}

	public void HideMessage(){
		_message.gameObject.SetActive (false); 
	}

	public void GameOver(){
		ShowMessage ("Game Over"); 
	}

	public void Paused(){
		ShowMessage ("Paused"); 
	}
}
