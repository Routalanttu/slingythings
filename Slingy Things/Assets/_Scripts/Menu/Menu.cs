using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    public void OnPlay() {
        SceneManager.LoadScene("TeamsAndAnimals");
    }

    public void OnLevelSelect() {
        SceneManager.LoadScene("SelectLevel");
    }

    public void OnCredits() {
        SceneManager.LoadScene("Credits");
    }

    public void OnReturn() {
        SceneManager.LoadScene("Menu");
    }

    public void OnLevelSelectReturn() {
        SceneManager.LoadScene("TeamsAndAnimals");
    }

    public void OnLevelSelection() {
        SceneManager.LoadScene(3);
    }

    public void OnQuit() {
        Application.Quit();
    }

    public void OnOptions() {
        SceneManager.LoadScene("Options");
    }

}
