using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DLoad : MonoBehaviour {
    private Transform playerPosition = null;
   
    [SerializeField]
    private float compensate;

    [SerializeField]
    private GameObject[] monster;

    private float[] savePos = new float[]{
        15 , 30 ,40,41,42,43,44,45,46,47,48,49,50,51 , 80 , 120 
        ,122 , 124 , 126 ,128, 130
    };

    private int[] saveMonster = new int[] 
    {
        0,0,0,0,0,
        0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0
        ,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
    };

    private DObjectPool[] monsterPool;
    

    int index = -1;
    float index_data =0;
    bool create_able; // 생성 가능한지 ?
	// Use this for initialization
	void Start () {
        monsterPool =  new DObjectPool[monster.Length];
        for (int i = 0; i < monster.Length; i++ )
        {
            monsterPool[i] = new DObjectPool(monster[i]);
        }
        
            create_able = NextIndex();
	}
	
	// Update is called once per frame
	void Update () {
        if(playerPosition == null)
        {
             playerPosition = GameObject.Find("players").transform;
        }
        CheckCreate();
	}

    bool NextIndex() // 다음 인덱스를 준비 다음 인덱스가 없으면 false
    {
        
        index++;
        if (savePos.Length <= index)
        {
            Debug.Log("end");
            return false;
        }
        index_data = savePos[index];
        return true;
    }

    void Create()
    {
        GameObject temp = monsterPool[saveMonster[index]].GetAble();
        temp.SendMessage("Create_Object", new Vector3(index_data, 0));
    }

    void CheckCreate()// 생성 가능 한지 확인 
    {
        if (playerPosition.position.x + compensate > index_data && create_able)
        {
            Create();
            create_able = NextIndex();
            
            
        }
        
    }
}
