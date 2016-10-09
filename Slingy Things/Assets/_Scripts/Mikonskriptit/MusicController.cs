using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

	private float volume; 

	public AudioClip music; 

	public bool loopMusic; 

	private AudioSource source; 
	private GameObject sourceGO; 

	void Start()
	{

		if(PlayerPrefs.HasKey("musicvolume")){
			volume = PlayerPrefs.GetFloat ("musicvolume"); 
		}else{
			volume = 0.2f; 
		}

		sourceGO = new GameObject("Music_Audiosource"); 
		source = sourceGO.AddComponent<AudioSource>();
		source.name = "MusicAudioSource"; 
		source.playOnAwake = true; 
		source.clip = music; 
		source.volume = volume; 

	}


	void Update()
	{

		if(source != null && !source.isPlaying && loopMusic){
			source.Play();    
		}
			

	}

	public void setVolume(float volumeControl){

		volume = volumeControl; 
		source.volume = volume; 

		PlayerPrefs.SetFloat ("musicvolume", volume); 
	}










}
