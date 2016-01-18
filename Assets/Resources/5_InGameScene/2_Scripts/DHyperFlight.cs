using UnityEngine;
using System.Collections;

public class DHyperFlight : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Enemy"))
        {
            //coll.gameObject.SendMessage("forceDead");
            coll.GetComponent<DEnemyObj>().HitWithPlayer();
        }
    }
}
