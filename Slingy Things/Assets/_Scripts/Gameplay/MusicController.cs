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

	public GameObject _musicoff; 

	void Awake(){

		if (_musicoff == null) {
			Debug.LogError ("_musicoff missing"); 
		}

	}

	void Start()
	{
		volume = _defaultVolume; 

		sourceGO = new GameObject("Music_Audiosource"); 
		source = sourceGO.AddComponent<AudioSource>();
		source.name = "MusicAudioSource"; 
		source.playOnAwake = true; 
		source.clip = music; 
		source.volume = volume; 


		if(PlayerPrefs.HasKey("mutemusic") && PlayerPrefs.GetInt("mutemusic") == 1){
			source.mute = true; 
			_musicoff.SetActive (true); 
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
			PlayerPrefs.SetInt ("mutemusic", 0); 
			_musicoff.SetActive (false); 
		}else{
			source.mute = true; 
			PlayerPrefs.SetInt ("mutemusic", 1); 
			_musicoff.SetActive (true); 
		}

		PlayerPrefs.Save (); 
	}











}
}
