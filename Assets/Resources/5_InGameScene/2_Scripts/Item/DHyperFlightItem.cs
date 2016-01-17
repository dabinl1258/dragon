using UnityEngine;
using System.Collections;

public class DHyperFlightItem : DItem {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        CheckMiss();
	}

    public virtual void HitWithPlayer()
    {
        DPlayers.instance.EnableHyper(3.0f);
        gameObject.SetActive(false);
    }
}
