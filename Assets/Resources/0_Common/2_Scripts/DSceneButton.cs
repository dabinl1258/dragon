using UnityEngine;
using System.Collections;

public class DSceneButton : MonoBehaviour {

    public string sceneName;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnClick()
    {
        Application.LoadLevelAsync(sceneName);
        Debug.Log("GO_MAIN");
    }
}
