using UnityEngine;
using System.Collections;

public class DPlayAudio : MonoBehaviour {
    public AudioSource audioSource = null;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayAudio(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
