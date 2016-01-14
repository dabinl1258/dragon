using UnityEngine;
using System.Collections;

public class M_Sound : MonoBehaviour {
    public bool SoundValue=true;
    AudioSource audio;
	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnClick()
    {
        if (SoundValue == true)
        {
            audio.mute = true;
            SoundValue = false;
            Debug.Log("Sound OFF");
        }

        else if (SoundValue == false)
        {
            audio.mute = false;
            SoundValue = true;
            Debug.Log("Sound ON");
        }
    }
}
