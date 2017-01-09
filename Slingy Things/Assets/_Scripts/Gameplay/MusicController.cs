using UnityEngine;
using System.Collections;

namespace SlingySlugs{

public class MusicController : MonoBehaviour {

	private float volume; 

	public AudioClip music; 
	public float _defaultVolume = 0.2f; 

	public bool loopMusic; 

	private AudioSource source; 
	private GameObject sourceGO; 

	void Awake(){

	}

	//Create music sound source and loop music audio clip. 
	void Start()
	{
		volume = _defaultVolume; 

		sourceGO = new GameObject("Music_Audiosource"); 
		source = sourceGO.AddComponent<AudioSource>();
		source.name = "MusicAudioSource"; 
		source.playOnAwake = true; 
		source.clip = music; 
		source.volume = volume; 


		if(PlayerPrefs.HasKey("mutesounds") && PlayerPrefs.GetInt("mutesounds") == 1){
			source.mute = true; 
		}else{
			volume = _defaultVolume; 
		}
			

	}


	void Update()
	{

		if(source != null && !source.isPlaying && loopMusic){
			source.Play();    
		}
			

	}

	public void ToggleMute(){

		if (source.mute) {
			source.mute = false; 
		}else{
			source.mute = true; 
		}

		PlayerPrefs.Save (); 
	}











}
}
