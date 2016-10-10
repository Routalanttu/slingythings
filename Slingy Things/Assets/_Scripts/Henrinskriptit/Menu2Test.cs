using UnityEngine;
using System.Collections;

public class Menu2Test : MonoBehaviour {

    public GameObject menuObjects;
    public GameObject numberOfTeamsObjects;
    public Transform centerScreen;
    public Transform offScreenRight;
    public Transform offScreenLeft;
    public float speed = 5.0f;
    private float startTime;
    private float journeyLength;
    private bool playPressCheck = false;
    private bool creditsPressCheck = false;

    public void Start() {
        numberOfTeamsObjects.transform.position = offScreenLeft.transform.position;
        menuObjects.transform.position = centerScreen.transform.position;
        startTime = Time.time;
        journeyLength = Vector3.Distance(centerScreen.position, offScreenRight.position);
    }

    public void Update() {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;

        if (playPressCheck) {
            menuObjects.transform.position = Vector3.Lerp(centerScreen.position, offScreenRight.position, fracJourney);
            numberOfTeamsObjects.transform.position = Vector3.Lerp(offScreenLeft.position, centerScreen.position, fracJourney);

            if (menuObjects.transform.position == offScreenRight.transform.position) {
                playPressCheck = false;
            }
        }
        
        if(creditsPressCheck) {
            menuObjects.transform.position = Vector3.Lerp(offScreenRight.position, centerScreen.position, fracJourney);
            numberOfTeamsObjects.transform.position = Vector3.Lerp(centerScreen.position, offScreenLeft.position, fracJourney);

            if (numberOfTeamsObjects.transform.position == offScreenLeft.transform.position) {
                creditsPressCheck = false;
            }
        }
    }

    public void OnPlay() {
        playPressCheck = true;
    }

    public void OnCreditsReturn() {
        creditsPressCheck = true;
    }
    
}
