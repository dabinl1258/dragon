using UnityEngine;
using System.Collections;

public class Scene_Quit : MonoBehaviour {
    //public GameObject Quit;
	// Use this for initialization
	void Start () {
        //Invoke("OnPressed", 2.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnClick()
    {
        Application.CancelQuit();
        Debug.Log("Exit");
    }
}
