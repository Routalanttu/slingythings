using UnityEngine;
using System.Collections;

public class SoundObject {

	public AudioSource source; 
	public GameObject sourceGO; 
	public AudioClip clip; 
	public string name; 

	public SoundObject (AudioClip aClip, string aName, float aVolume){

		sourceGO = new GameObject ("AudioSource_" + aName); 
		source = sourceGO.AddComponent<AudioSource> (); 
		source.name = "AudioSource_" + aName; 
		source.playOnAwake = false; 
		source.clip = aClip; 
		source.volume = aVolume; 
		source.spatialBlend = 0f; 
		source.bypassEffects = true; 
		source.bypassListenerEffects = true; 
		source.bypassReverbZones = true; 
	
		clip = aClip; 
		name = aName; 

	}

	public void PlaySound(){
		source.PlayOneShot (clip); 
	}

	public void StopSound(){
		source.Stop (); //stops the audiosource 
	}

	public void SetVolume(float setVolume){
		source.volume = setVolume; 
	}
}



public class SoundController : MonoBehaviour {

	private static SoundController _instance; 

	public static SoundController Instance{
		get {
			return _instance; 
		}
	}

	public AudioClip[] GameSounds; 
	private int totalSounds; 
	private ArrayList soundObjectList; 
	private SoundObject tempSoundObj; 
	private float volume;  

	public void Awake(){

		if (_instance == null) {
			_instance = this; 
		}else if (_instance != this) {
			 Destroy (this); 
		}

		if(PlayerPrefs.HasKey("soundvolume")){
			volume = PlayerPrefs.GetFloat ("soundvolume"); 
		}else{
			volume = 1; 
		}

		soundObjectList = new ArrayList(); 

		foreach (AudioClip theSound in GameSounds) {
			tempSoundObj = new SoundObject (theSound, theSound.name, volume); 

			soundObjectList.Add (tempSoundObj); 
			totalSounds++; 
		}

	}

	public void PlaySoundByIndex(int anIndexNumber) {

		//CHECK ARRAY BOUNDS 
		if (anIndexNumber > soundObjectList.Count) {
			Debug.LogWarning ("trying to play a sound indexed outside sound array"); 
			anIndexNumber = soundObjectList.Count - 1; 
		}

		tempSoundObj = (SoundObject)soundObjectList [anIndexNumber]; 
		tempSoundObj.PlaySound (); 

	}

	public void StopSoundByIndex(int anIndexNumber) {

		//CHECK ARRAY BOUNDS 
		if (anIndexNumber > soundObjectList.Count) {
			Debug.LogWarning ("trying to stop a sound indexed outside sound array"); 
			anIndexNumber = soundObjectList.Count - 1; 
		}

		tempSoundObj = (SoundObject)soundObjectList [anIndexNumber]; 
		tempSoundObj.StopSound (); 

	}


	public void setVolume(float volumeControl){
		volume = volumeControl; 

		foreach (SoundObject soundobj in soundObjectList) {
			soundobj.source.volume = volume;  
		}
			
		PlayerPrefs.SetFloat ("soundvolume", volume); 
	}

	public void StopSounds(){

		foreach (SoundObject soundobj in soundObjectList) {
			soundobj.source.Stop (); 
		}
	}

	public void PlaySounds(){

		foreach (SoundObject soundobj in soundObjectList) {
			soundobj.source.Play (); 
		}
	}

	public bool isPlaying(int audioIndex){

		SoundObject tempSound = (SoundObject) soundObjectList [audioIndex]; 
		return tempSound.source.isPlaying;
	}

		
}
