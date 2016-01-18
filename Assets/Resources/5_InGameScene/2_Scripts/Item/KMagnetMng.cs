using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KMagnetMng : MonoBehaviour {
    
    public GameObject[] m_MagnetList = new GameObject[10];
    int Headpointer = 0;
    int Tailpointer = 0;
    DPlayers m_Player;

	// Use this for initialization
	void Start () {
        m_Player = transform.root.GetComponent<DPlayers>();

        for(int i = 0 ; i < 10 ; i++ )
        {
            m_MagnetList[i] = null;
        }
        Debug.Log(m_MagnetList.Length);
        
	}
	
	// Update is called once per frame
	void Update () {
	    if(m_Player.getOnMagnet() == true)
        {
            if (Headpointer > 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (m_MagnetList[i] != null)
                    {
                        //m_MagnetList[i].transform.position = Vector2.Lerp(m_MagnetList[i].transform.position, transform.position, 1f);
                        Debug.Log("magnet");
                    }
                }
            }
            
        }
        else
        {
             
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Item")== true)
        {
            Debug.Log("Item added");
            AddObjectToArray(col.gameObject);
        }
    }

    public void AddObjectToArray(GameObject p_gameobject)
    {
        m_MagnetList[Headpointer] = p_gameobject;
        Headpointer++;
    }

    public void DelObjectInArray()
    {
        m_MagnetList[Tailpointer] = null;
        Tailpointer++;
    }
}


