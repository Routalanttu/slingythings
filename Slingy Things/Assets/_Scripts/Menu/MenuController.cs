using UnityEngine;
using System.Collections;

namespace SlingySlugs{

public class MenuController : MonoBehaviour {

    public GameObject menuObjects;
    public GameObject numberOfTeamsObjects;

    public void Start()
    {
        //numberOfTeamsObjects.transform.Translate(new Vector2(5, 0));
        numberOfTeamsObjects.transform.position = new Vector3(15, 0, 10);
    }

    public void OnPlay()
    {
        //menuObjects.transform.Translate(new Vector2(7, 0));
        //numberOfTeamsObjects.transform.Translate(new Vector2(-5, 0));


        menuObjects.transform.position = new Vector3(15, 0, 10);
        numberOfTeamsObjects.transform.position = new Vector3(0, 0, 10);
    }

    public void OnBack()
    {
        //menuObjects.transform.Translate(new Vector2(0, 0));
        //numberOfTeamsObjects.transform.Translate(new Vector2(5, 0));

        menuObjects.transform.position = new Vector3(0, 0, 10);
        numberOfTeamsObjects.transform.position = new Vector3(15, 0, 10);

    }

}
}
