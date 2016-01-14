using UnityEngine;
using System.Collections;

public class Show_Dia : MonoBehaviour {
    //public UILabel Diamond;
    // Use this for initialization
    public UILabel Label = null;
    public Info_Manager Mng = null;

    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Label.text = "D  " + Mng.Current_Dia.ToString();
    }
}
