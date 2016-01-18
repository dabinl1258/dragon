using UnityEngine;
using System.Collections;

public class DInTestFlash : MonoBehaviour {
    DFlashingEffect flahs;
	// Use this for initialization
	void Start () {
        flahs = GetComponent<DFlashingEffect>();
        flahs.EnableEffect();
        flahs.timerMax = 0.1f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
