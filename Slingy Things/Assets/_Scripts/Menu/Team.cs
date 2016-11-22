using UnityEngine;
using System.Collections;

namespace SlingySlugs{

	//container for team information

	public class Team {
		
		public string _teamName; 
		public string _teamColor; 
		public Color _teamUnityColor; 
		public string[] _slugNames;
		public string[] _slugClasses;

		public Team(){

			_slugNames = new string[6];
			_slugClasses = new string[6];

		}


		public void SetUnityColor(){

			_teamUnityColor = Color.black; 
			
			if (_teamColor == "Red") {
				_teamUnityColor.r = 1f;
				_teamUnityColor.g = 0f;
				_teamUnityColor.b = 0f;
			} else if (_teamColor == "Blue") {
				_teamUnityColor.r = 0f;
				_teamUnityColor.g = 0f;
				_teamUnityColor.b = 1f;
			} else if (_teamColor == "Yellow") {
				_teamUnityColor.r = 1f;
				_teamUnityColor.g = 1f;
				_teamUnityColor.b = 0f;
			} else if (_teamColor == "Green") {
				_teamUnityColor.r = 0;
				_teamUnityColor.g = 1f;
				_teamUnityColor.b = 0f;
			} else if (_teamColor == "Violet") {
				_teamUnityColor.r = 1f;
				_teamUnityColor.g = 0f;
				_teamUnityColor.b = 1f;
			} else if (_teamColor == "Orange") {
				_teamUnityColor.r = 1f;
				_teamUnityColor.g = 0.5f;
				_teamUnityColor.b = 0f;
			} else {
				_teamUnityColor.r = 0f;
				_teamUnityColor.g = 0f;
				_teamUnityColor.b = 0f;
			}
		}

	}

}


