using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DPlayerAfterimage : MonoBehaviour {
    
    bool able = false;
    public SkinnedMeshRenderer meshRenderer = null;
    public Color color;

    public void Enable()
    {
        able = true;
    }

    public void Disable()
    {
        able = false;
    }



	// Use this for initialization
	void Start () {

        
	}
	
	// Update is called once per frame
	void Update () {
        print(meshRenderer.material.color);
        meshRenderer.material.color = color;
	}

    private void EffectUpdate()
    {
		GameObject temp;
    }
}
