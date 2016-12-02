using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashScreenController : MonoBehaviour {

    public Animator _splashScreenAnim;

    private AsyncOperation async;


    void Start() {
        ClickAsync();
        _splashScreenAnim.SetTrigger("Animate");
    }

    void animationEnded() {
        ActivateMenu();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0))
            ActivateMenu();
    }

    public void ClickAsync() {
        StartCoroutine(LoadMenuWithAnimation());
    }

    IEnumerator LoadMenuWithAnimation() {
        async = SceneManager.LoadSceneAsync("Menu");
        async.allowSceneActivation = false;
        yield return async;
    }

    public void ActivateMenu() {
        async.allowSceneActivation = true;
    }

}

