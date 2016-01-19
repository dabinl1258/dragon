using UnityEngine;
using System.Collections;

public class LogoScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
    void OnClick()
    {
        Application.LoadLevelAsync(1);
        Debug.Log("GO_MAIN");
    }
}
