using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Return : MonoBehaviour {

    public void OnReturn() {
        SceneManager.LoadScene("Menu");
    }
}
