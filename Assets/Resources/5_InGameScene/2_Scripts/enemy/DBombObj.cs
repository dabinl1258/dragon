using UnityEngine;
using System.Collections;

public class DBombObj : DEnemyObj {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        CheckMiss();
	}

    public override void HitWithPlayer()
    {
        forceDead();
    }

    protected override void Miss()
    {
        gameObject.SetActive(false);
    }

    virtual public void forceDead()
    {
        DPlayers.instance.RestEnegy(restEnergy);
        gameObject.SetActive(false);
    }

    
}
