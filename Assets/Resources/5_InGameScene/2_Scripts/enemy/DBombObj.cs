using UnityEngine;
using System.Collections;

public class DBombObj : DEnemyObj {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (DMainCamera.instance.GetCollision() > transform.position.x)
            gameObject.SetActive(false);
	}

    public override void HitWithPlayer()
    {
        DPlayers.instance.RestEnegy(restEnergy);
        gameObject.SetActive(false);
    }

    protected override void Miss()
    {
        DPlayers.instance.RestEnegy( restEnergy);
        gameObject.SetActive(false);
    }

    
}
