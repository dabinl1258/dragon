using UnityEngine;
using System.Collections;

public class Show_Number : MonoBehaviour {
    public UILabel Label = null;
    public Info_Manager mng = null;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        Label.text = "H  " + mng.Current_Num.ToString();   
    }
}
