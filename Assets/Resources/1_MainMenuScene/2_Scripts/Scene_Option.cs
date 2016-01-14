using UnityEngine;
using System.Collections;

public class Scene_Option : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void OnClick()
    {
        Application.LoadLevelAsync(5);
    }
}
