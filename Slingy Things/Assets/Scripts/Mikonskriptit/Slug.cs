using UnityEngine;
using System.Collections;

public class Slug : MonoBehaviour {

	public enum Team 
	{
		Team1 = 1, 
		Team2 = 2, 
	};

	public Team teamSelect = Team.Team1; 

	public bool IsActive {
		set; 
		get; 
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
