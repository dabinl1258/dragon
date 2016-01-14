using UnityEngine;
using System.Collections;

public class M_Vibration : MonoBehaviour {
    public bool VibValue = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnClick()
    {
        if (VibValue == true)
        {
            Debug.Log("Vibration OFF");
            VibValue = false;
        }

        else if (VibValue == false)
        {
            Debug.Log("Vibration ON");
            VibValue = true;
        }
    }
}
