using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashScreenController : MonoBehaviour {

    public Animator _splashScreenAnim;

	// Use this for initialization
	void Start () {
        _splashScreenAnim.SetTrigger("Animate");

    }

    void animationEnded () {
        SceneManager.LoadScene("Menu");
    }
	
	// Update is called once per frame
	void Update () {
        //animationEnded();
	}
}
