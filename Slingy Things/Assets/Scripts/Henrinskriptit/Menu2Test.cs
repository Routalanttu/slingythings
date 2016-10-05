using UnityEngine;
using System.Collections;

public class Menu2Test : MonoBehaviour {

    public GameObject menuObjects;
    public GameObject numberOfTeamsObjects;

    public void Start() {
        numberOfTeamsObjects.transform.Translate(new Vector2 (200, 0));
    }

    public void OnPlay() {
        menuObjects.transform.Translate(new Vector2 (200, 0));
        numberOfTeamsObjects.transform.Translate(new Vector2 (0, 0));
    }


}
