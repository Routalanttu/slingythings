using UnityEngine;
using System.Collections;

namespace SlingySlugs{

	//container for team information

	public class Team {
		
		public string _teamName; 
		public string _teamColor; 
		public string[] _slugNames;
		public string[] _slugClasses;

		public Team(){

			_slugNames = new string[6];
			_slugClasses = new string[6];

			for (int i = 0; i < 6; i++) {
				_slugNames [i] = "defaultname";  //default setting, don't know if any use
			}

			for (int j = 0; j < 6; j++) {
				_slugClasses [j] = "Slug";  //default setting, not sure if any use
			}

		}

	}

}


