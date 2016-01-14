using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MHomiLibrary;


/// <summary>
/// 위치 :  0_Mngs
/// 게임에쓰이는 데이타를 DB에서 읽어와 저장해두는 곳입니다
/// 이곳은 참조만 하는곳입니다. 
/// </summary>
public class HMng : HSingleton<HMng>
{
    protected HMng() { }

    void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    void Update()
    {
       
    }

    void OnDestroy()
    {
        Debug.Log("OnDestroy()/HMng.cs");
    }
}