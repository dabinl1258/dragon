using UnityEngine;
using System.Collections;

public class KMagnetItem : MonoBehaviour {

    public static bool isActived = false;
    public DPlayers Hero = null;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        
	    
	}

   

    public void HitWithPlayer()
    {
        gameObject.SetActive(false);

        if(isActived == false)
            isActived = true;

        Hero.OnMagnet();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            HitWithPlayer();
        }
    }

}
