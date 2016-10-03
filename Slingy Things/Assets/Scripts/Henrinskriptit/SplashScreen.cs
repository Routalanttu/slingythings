using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour {

    [SerializeField] private float delayTime = 5.0f;

    private float vittu;

    private SpriteRenderer _sr;

    void Awake() {
        _sr = GetComponent<SpriteRenderer>();
        vittu = 0f;
    }

    private void Update() {
        float häh = Time.deltaTime;

        delayTime -= häh;

        if (delayTime < 4.5f && delayTime > 3.5f) {
            vittu += häh;
            Color tmp = new Color(_sr.color.r, _sr.color.g, _sr.color.b,vittu);

            _sr.color = tmp;
        }

        if (delayTime < 1.5f && delayTime > 0.5f) {
            vittu -= häh;
            Color tmp = new Color(_sr.color.r, _sr.color.g, _sr.color.b,vittu);

            _sr.color = tmp;
        }

        Debug.Log(vittu);





        if (delayTime < 0f) {
            SceneManager.LoadScene("Menu");
        }

        if (Input.GetButtonDown("Jump")) {
            SceneManager.LoadScene("Menu");
        }
    }
}
