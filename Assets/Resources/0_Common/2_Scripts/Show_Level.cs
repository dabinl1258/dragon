using UnityEngine;
using System.Collections;

public class Show_Level : MonoBehaviour {
    public UILabel Label = null;
    public Info_Manager mng = null;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Label.text = "L  " + mng.Now_Level.ToString();
	}
}
