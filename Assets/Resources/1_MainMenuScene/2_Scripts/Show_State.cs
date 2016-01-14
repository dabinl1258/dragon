using UnityEngine;
using System.Collections;

public class Show_State : MonoBehaviour {
    int nPrefabCount = 1;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnClick()
    {
        Debug.Log("Showing State Pop-Up");
        //GameObject obj = HPrefabMng.I.CreatePrefab("UI Root", E_H_RESOURCELOAD.E_1_MainMenuScene,
        //    "State_PopUp", 0, "State_PopUp", + nPrefabCount.ToString());
    }
}
