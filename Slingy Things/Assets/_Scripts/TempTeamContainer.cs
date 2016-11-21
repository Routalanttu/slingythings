using UnityEngine;
using System.Collections;

namespace SlingySlugs{

public class TempTeamContainer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt ("HasBeenPlayed") == null) {

			for (int i = 0; i < 8; i++) {
				PlayerPrefs.SetString ("Team" + i + "Name", "Team " + (i+1));
			}

			PlayerPrefs.SetInt ("RedUsed", 1);
			PlayerPrefs.SetInt ("GreenUsed", 1);
			PlayerPrefs.SetInt ("BlueUsed", 1);
			PlayerPrefs.SetInt ("YellowUsed", 1);
			PlayerPrefs.SetInt ("OrangeUsed", 0);
			PlayerPrefs.SetInt ("PurpleUsed", 0);
			PlayerPrefs.SetInt ("BlackUsed", 0);
			PlayerPrefs.SetInt ("WhiteUsed", 0);

			PlayerPrefs.SetInt ("SelectedTeam1", 0);
			PlayerPrefs.SetInt ("SelectedTeam2", 1);
			PlayerPrefs.SetInt ("SelectedTeam3", 2);
			PlayerPrefs.SetInt ("SelectedTeam4", 3);

			PlayerPrefs.SetInt ("HasBeenPlayed", 1);

			PlayerPrefs.SetString ("Team3Name", "YO MOMMA!");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
}
